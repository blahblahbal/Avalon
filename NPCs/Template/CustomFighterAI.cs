using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.NPCs.Template;

public abstract class CustomFighterAI : ModNPC
{
	public virtual float MaxMoveSpeed => 2.5f;
	public virtual float MaxAirSpeed => 3.5f;
	public virtual float Acceleration => 0.1f;
	public virtual float AirAcceleration => 0.1f;
	public virtual float MaxJumpHeight => 8f;
	public virtual float JumpRadius => 150;
	public virtual bool JumpOverDrop => true;
	public virtual bool CanOpenDoors => false;
	public virtual bool CanBreakDoors => false;
	/// <summary>
	/// set to -1 to allow knocking but no opening.
	/// </summary>
	public virtual int MaxKnockCount => 1;
	public virtual int MaxNumberOfJumpsAgainstWall => 4;
	public virtual int KnockInterval => 60;
	public virtual int TimeBeforeTurningAround => 180;

	public float PreviousDirection;
	public float TimeSpentAtDoor = 0;
	public ref float NumJumps => ref NPC.ai[0];
	/// <summary>
	/// RunningMode 0 is Targeting player
	/// RunningMode 1 is Running away
	/// </summary>
	public ref float RunningMode => ref NPC.ai[1];
	public ref float RunningModeTimer => ref NPC.ai[2];
	public ref float SavePrevDir => ref NPC.ai[3];

	public override bool? CanFallThroughPlatforms()
	{
		Player player = Main.player[NPC.FindClosestPlayer()];
		float upOrDown = NPC.Center.Y - player.Center.Y;

		//if the player is under the npc then fall through the platform, should maybe check for canhitline but vanilla doesn't do that so idk
		return NPC.collideY && upOrDown < -15;
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(PreviousDirection);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		PreviousDirection = reader.ReadSingle();
	}
	public virtual void CustomBehavior()
	{
	}
	//private bool isHit;
	private bool isInJump;
	private float jumpdelay = 3;
	public override void AI()
	{
		Player player = Main.player[NPC.FindClosestPlayer()];
		float distanceBetweenPlayer = Vector2.Distance(player.Center, NPC.Center);
		float dir;

		// if confused and the buff has more than 1 tick left on it, set RunningMode to 1 (running away)
		// if confused and buff has exactly 1 tick left, set RunningMode to 0 (targeting the closest player)
		if (NPC.confused)
		{
			int bindex = NPC.FindBuffIndex(BuffID.Confused);
			if (bindex > -1)
			{
				if (NPC.buffTime[bindex] > 1)
				{
					RunningMode = 1;
					NumJumps = 0;
				}
				else if (NPC.buffTime[bindex] == 1)
				{
					RunningMode = 0;
					NumJumps = 0;
				}
			}
		}
		// set running mode to 0 if just hit
		if (!NPC.confused && NPC.justHit)
		{
			RunningMode = 0;
			NumJumps = 0;
		}
		// some annoying logic to allow the npc to save the direction it's going during running away mode
		// (so it doesn't always face away from the player if you happen to run to the other side of it while it's in this mode)
		if (RunningMode == 0)
		{
			dir = NPC.Center.X - player.Center.X;
		}
		else
		{
			if (SavePrevDir == 0)
			{
				dir = NPC.Center.X - player.Center.X;
				PreviousDirection = dir;
				SavePrevDir = 1;
			}
			else
			{
				dir = PreviousDirection;
			}
		}
		float upOrDown = NPC.Center.Y - player.Center.Y;

		dir = Math.Sign(dir);

		//movement stuff you don't need to worry about... unless you do then just figure it out lol
		float moveSpeedMulti;
		float airSpeedMulti;

		// set multipliers based on running mode
		if (RunningMode == 0)
		{
			moveSpeedMulti = NPC.velocity.X + (Acceleration * -dir);
			airSpeedMulti = NPC.velocity.X + (AirAcceleration * -dir);
		}
		else
		{
			moveSpeedMulti = -NPC.velocity.X + (Acceleration * -dir);
			airSpeedMulti = -NPC.velocity.X + (AirAcceleration * -dir);
		}
		moveSpeedMulti = Math.Clamp(moveSpeedMulti, -MaxMoveSpeed, MaxMoveSpeed);
		airSpeedMulti = Math.Clamp(airSpeedMulti, -MaxAirSpeed, MaxAirSpeed);

		// set X velocity based on running mode
		if (RunningMode == 0)
		{
			NPC.spriteDirection = -(int)dir;
			if (NPC.velocity.Y == 0)
			{
				NPC.velocity.X = moveSpeedMulti;
			}
			else
			{
				NPC.velocity.X = airSpeedMulti;
			}
		}
		else if (RunningMode == 1)
		{
			NPC.spriteDirection = (int)dir;
			if (NPC.velocity.Y == 0)
			{
				NPC.velocity.X = -moveSpeedMulti;
			}
			else
			{
				NPC.velocity.X = -airSpeedMulti;
			}
		}
		// increment running mode timer
		if (RunningMode == 1 && !NPC.confused)
		{
			RunningModeTimer++;
			if (RunningModeTimer > TimeBeforeTurningAround && NPC.collideY)
			{
				RunningMode = 0;
				RunningModeTimer = 0;
				SavePrevDir = 0;
				NumJumps = 0;
				return;
			}
		}
		//if the player is above the npc and in range (JumpRadius) and there is also a line between the player and the npc
		if (distanceBetweenPlayer < JumpRadius && NPC.collideY && upOrDown > 1 && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
		{
			Jump(MaxJumpHeight);
			NumJumps = 0;
		}
		Point a = NPC.Bottom.ToTileCoordinates();
		float height = 0;
		// if its on the ground and touching a wall
		if ((NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile && !Main.tileSolidTop[Main.tile[a.X, a.Y].TileType]) && NPC.collideX && NumJumps <= MaxNumberOfJumpsAgainstWall)
		{
			bool door = false;
			bool tallGate = false;
			int modifier = 1;
			if (RunningMode == 1) modifier = -1;
			//check for the height of the wall infront and if it's a door
			for (int i = 0; i < 10; i++)
			{
				Tile t = Main.tile[a.X + 1 * -(int)dir * modifier, a.Y - i];
				if ((CanOpenDoors || CanBreakDoors) && i == 1 && (t.TileType == TileID.TallGateClosed || i < 3 && TileLoader.IsClosedDoor(t.TileType)))
				{
					if (t.TileType == TileID.TallGateClosed)
						tallGate = true;
					door = true;
					break;
				}
				if (t.HasTile && Main.tileSolid[t.TileType])
				{
					height = i + 1;
				}
			}
			if (!door)
			{
				//jumps with the right height
				if (NumJumps < MaxNumberOfJumpsAgainstWall)
					Jump(height);
				else
				{
					// swap the running mode and set num jumps to 0
					if (RunningMode == 0) RunningMode = 1;
					else if (RunningMode == 1) RunningMode = 0;
					NumJumps = 0;
				}
			}
			else
			{
				TimeSpentAtDoor++;
				if (MaxKnockCount != -1 && TimeSpentAtDoor > KnockInterval * MaxKnockCount)
				{
					if (CanOpenDoors)
					{
						if ((!tallGate && WorldGen.OpenDoor(a.X + 1 * -(int)dir * modifier, a.Y - 1, NPC.direction) || (tallGate && WorldGen.ShiftTallGate(a.X + 1 * -(int)dir * modifier, a.Y - 1, false))) && Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, tallGate ? 4 : 0, a.X + 1 * -(int)dir * modifier, a.Y - 1, NPC.direction);
						}
					}
					else
					{
						WorldGen.KillTile(a.X + 1 * -(int)dir * modifier, a.Y - 1);
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, a.X + 1 * -(int)dir * modifier, a.Y - 1);
					}
				}
				if(TimeSpentAtDoor % KnockInterval == 0)
				{
					WorldGen.KillTile(a.X + 1 * -(int)dir * modifier, a.Y - 1, true);
				}
			}
		}
		else
			TimeSpentAtDoor = 0;

		//if its on the ground
		if (NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile)
		{
			//enable stepup when on the ground
			Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
			isInJump = false;
			//isHit = false;
			if (jumpdelay != 0)
			{
				jumpdelay--;
			}
			//if the tile under and infront is air then jump
			if ((!Main.tileSolid[Main.tile[a.X + 1 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 1 * -(int)dir, a.Y].HasTile) && (!Main.tileSolid[Main.tile[a.X + 2 * -(int)dir, a.Y].TileType] || !Main.tile[a.X + 2 * -(int)dir, a.Y].HasTile) && upOrDown > -20 && JumpOverDrop)
			{
				Jump(MaxJumpHeight);
				NumJumps = 0;
			}
		}
		else
		{
			if (NPC.velocity.Y < 0)
			{
				isInJump = true;
			}
			//enable step up if at peak of the jump
			if (NPC.velocity.Y > 0)
			{
				Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
			}
		}
		CustomBehavior();
	}
	public void Jump(float height)
	{
		
		//do the jump, if the height is higher than the maxjump then just set it to maxjumpheight
		if(jumpdelay == 0)
		{
			height = Math.Clamp(height + 2.5f, 0f, MaxJumpHeight);
			NPC.velocity.Y = -height;
			jumpdelay = 3;
			NumJumps++;
		}
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (!NPC.confused)
			RunningMode = 0;
	}
	/*public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
	{
		isHit = true;
	}
	public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
	{
		isHit = true;
	}*/
}
