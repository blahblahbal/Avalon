using Avalon.Common;
using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode;

internal class WallofSteelLaserEye : ModNPC
{
	private byte ModeChangeCounter = 0;
	private byte Phase = 0;
	private byte Phase1FireballCounter = 0;
	private byte Cycle = 0;
	private float PhaseRotation = 0f;
	private int PreviousProj = -1;
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 1;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Ichor] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
		var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Hide = true
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
	}

	public override void SetDefaults()
	{
		NPC.width = 96;
		NPC.height = 96;
		NPC.noTileCollide = NPC.noGravity = NPC.behindTiles = true;
		NPC.npcSlots = 100f;
		NPC.damage = 150;
		NPC.lifeMax = 47000;
		NPC.timeLeft = 750000;
		NPC.defense = 55;
		NPC.aiStyle = -1;
		NPC.knockBackResist = 0;
		NPC.scale = 1f;
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		//NPC.gfxOffY = 30;
		DrawOffsetY = (int)((154 / 2) - (NPC.Size.Y / 2));
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("WallofSteelGore4").Type, NPC.scale);
			int s1 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, GoreID.Smoke1, NPC.scale);
			int s2 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, GoreID.Smoke2, NPC.scale);
			int s3 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.Center, NPC.velocity, GoreID.Smoke3, NPC.scale);
		}
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(ModeChangeCounter);
		writer.Write(Phase);
		writer.Write(Phase1FireballCounter);
		writer.Write(Cycle);
		writer.Write(PhaseRotation);
		writer.Write(PreviousProj);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		ModeChangeCounter = reader.ReadByte();
		Phase = reader.ReadByte();
		Phase1FireballCounter = reader.ReadByte();
		Cycle = reader.ReadByte();
		PhaseRotation = reader.ReadSingle();
		PreviousProj = reader.ReadInt32();
	}
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.85f * bossAdjustment);
		NPC.damage = (int)(NPC.damage * 0.65f);
	}
	public override void OnKill()
	{
		if (AvalonWorld.WallOfSteel != -1)
		{
			Main.npc[AvalonWorld.WallOfSteel].ai[3] = 60 * Main.rand.Next(20, 31);
		}
	}
	public override void AI()
	{
		if (AvalonWorld.WallOfSteel == -1)
		{
			NPC.checkDead();
			NPC.life = 0;
			return;
		}
		NPC mainWall = Main.npc[AvalonWorld.WallOfSteel];

		NPC.TargetClosest(true);

		NPC.Center = mainWall.Center + new Vector2(75 * mainWall.direction, 160);
		NPC.direction = mainWall.direction;
		NPC.spriteDirection = mainWall.spriteDirection;

		Vector2 NPCCenter = NPC.Center;
		if (ModeChangeCounter == 0 && Phase != 2) // && Phase != 0 && Phase != 1)
		{
			float rotationModifier = 0.05f;
			float targetCenterX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - NPCCenter.X;
			float targetCenterY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - NPCCenter.Y;
			float rotationToPlayer = (float)Math.Atan2(targetCenterY, targetCenterX) - (mainWall.direction == 1 ? 0 : 3.14f);
			if (rotationToPlayer < 0f)
				rotationToPlayer += 6.283f;
			else if ((double)rotationToPlayer > 6.283)
				rotationToPlayer -= 6.283f;
			if (NPC.rotation < rotationToPlayer)
			{
				if ((double)(rotationToPlayer - NPC.rotation) > 3.1415)
					NPC.rotation -= rotationModifier;
				else
					NPC.rotation += rotationModifier;
			}
			else if (NPC.rotation > rotationToPlayer)
			{
				if ((double)(NPC.rotation - rotationToPlayer) > 3.1415)
					NPC.rotation += rotationModifier;
				else
					NPC.rotation -= rotationModifier;
			}

			if (NPC.rotation > rotationToPlayer - rotationModifier && NPC.rotation < rotationToPlayer + rotationModifier)
				NPC.rotation = rotationToPlayer;

			if (NPC.rotation < 0f)
				NPC.rotation += 6.283f;
			else if (NPC.rotation > 6.283)
				NPC.rotation -= 6.283f;

			if (NPC.rotation > rotationToPlayer - rotationModifier && NPC.rotation < rotationToPlayer + rotationModifier)
				NPC.rotation = rotationToPlayer;

			if (NPC.rotation < 0f)
				NPC.rotation += 6.283f;
			else if (NPC.rotation > 6.283)
				NPC.rotation -= 6.283f;

			if (NPC.rotation > rotationToPlayer - rotationModifier && NPC.rotation < rotationToPlayer + rotationModifier)
			{
				NPC.rotation = rotationToPlayer;
			}
		}
		if (ModeChangeCounter == 0)
		{
			if (Phase == 0)
			{
				//PanningLaserPhase();
				SingleLaserSpamPhase();
			}
			if (Phase == 1)
			{
				//PanningLaserPhase();
				ElectricBoltPhase();
			}
			if (Phase == 2)
			{
				PanningLaserPhase();
			}
		}
		if (ModeChangeCounter == 1)
		{
			PhaseRotation += 0.005f;
			if (PhaseRotation > 0.5)
			{
				PhaseRotation = 0.5f;
				ModeChangeCounter = 2;
			}
			NPC.rotation += PhaseRotation;
		}
		else if (ModeChangeCounter == 2)
		{
			PhaseRotation -= 0.005f;
			if (PhaseRotation < 0f)
				PhaseRotation = 0f;

			NPC.rotation += PhaseRotation;
			NPC.ai[1]++;

			if (NPC.ai[1] == 100)
			{
				NPC.ai[1] = 0;
				Phase++;
				ModeChangeCounter = 0;
				SoundEngine.PlaySound(SoundID.Roar, NPC.position);
				NPC.ai[2] = 0;
				Cycle = 0;
				if (Phase == 3)
				{
					Phase = 0;
				}
				return;
			}
		}
	}

	private void PanningLaserPhase()
	{
		NPC.ai[2]++;
		if (NPC.ai[2] == 10)
		{
			int fire;
			float f = 0f;
			//var laserPos = new Vector2(NPC.Center.X + NPC.Center.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.Center.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
			var laserPos = NPC.Center;
			float rotation = (float)Math.Atan2(laserPos.Y - NPC.PlayerTarget().Center.Y, laserPos.X - NPC.PlayerTarget().Center.X);
			SoundEngine.PlaySound(SoundID.Item33, new Vector2((int)NPC.position.X, AvalonWorld.WallOfSteelT));

			if (Phase1FireballCounter == 0)
			{
				if (NPC.ai[3] <= 0.3f)
				{
					//var spawnOffset = laserPos.DirectionTo(NPC.PlayerTarget().Center).RotatedBy(rotation);
					Vector2 vel = NPC.Center.DirectionTo(NPC.Center + new Vector2((float)(Math.Cos(rotation + NPC.ai[3]) * 12f * -1), (float)(Math.Sin(rotation + NPC.ai[3]) * 12f * -1)));
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), laserPos.X + vel.X * 40f, laserPos.Y + vel.Y * 40f, vel.X * 8f, vel.Y * 8f, ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSLaserSmall>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}

					NPC.rotation = NPC.Center.DirectionTo(Main.projectile[fire].Center).ToRotation() + (NPC.direction == -1 ? MathHelper.Pi : -MathHelper.TwoPi);
					NPC.ai[3] += .1f;
					if (NPC.ai[3] >= 0.3f)
					{
						Phase1FireballCounter = 1;
					}
				}
			}
			else
			{
				if (NPC.ai[3] >= -0.3f)
				{
					Vector2 vel = NPC.Center.DirectionTo(NPC.Center + new Vector2((float)((Math.Cos(rotation + NPC.ai[3]) * 12f) * -1), (float)((Math.Sin(rotation + NPC.ai[3]) * 12f) * -1)));
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), laserPos.X + vel.X * 40f, laserPos.Y + vel.Y * 40f, vel.X * 8f, vel.Y * 8f, ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSLaserSmall>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;

					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
					NPC.rotation = NPC.Center.DirectionTo(Main.projectile[fire].Center).ToRotation() + (NPC.direction == -1 ? MathHelper.Pi : -MathHelper.TwoPi);
					NPC.ai[3] -= .1f;
					if (NPC.ai[3] <= -0.3f)
					{
						Phase1FireballCounter = 0;
					}
				}
			}
			NPC.ai[2] = 0;
			Cycle++;
		}

		if (Cycle == 30)
		{
			ModeChangeCounter = 1;
			Cycle = 0;
		}
	}

	private void ElectricBoltPhase()
	{
		NPC.ai[2]++;
		if (NPC.ai[2] == 60)
		{
			Vector2 modifiedPosition = new Vector2(NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
			Vector2 vectorBetween = NPC.PlayerTarget().Center - modifiedPosition;
			float randomSeed = Main.rand.Next(100);
			Vector2 startVelocity = Vector2.Normalize(vectorBetween.RotatedByRandom(0.78539818525314331)) * 27f;
			int lightning = Projectile.NewProjectile(NPC.GetSource_FromAI(), modifiedPosition,
				startVelocity, ModContent.ProjectileType<Lightning>(), 55, 0f, Main.myPlayer,
				vectorBetween.ToRotation(), randomSeed);
			Main.projectile[lightning].friendly = false;
			Main.projectile[lightning].hostile = true;

			SoundEngine.PlaySound(SoundID.Item91, NPC.Center);

			NPC.ai[2] = 0;
			Cycle++;
		}

		// change modes if 10 cycles are done
		if (Cycle == 10)
		{
			ModeChangeCounter = 1;
			Cycle = 0;
		}
	}

	private void SingleLaserSpamPhase()
	{
		NPC.ai[2]++;
		if (NPC.ai[2] == 75)
		{
			int laser = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f - 10, NPC.velocity.X, NPC.velocity.Y, ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSLaserSmall>(), Main.expertMode ? 70 : 55, 4f);
			Main.projectile[laser].velocity = Vector2.Normalize(Main.player[NPC.target].Center - new Vector2(NPC.Center.X, NPC.Center.Y)) * 11f;
			Main.projectile[laser].hostile = true;
			Main.projectile[laser].friendly = false;
			Main.projectile[laser].tileCollide = false;
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, laser);
			}

			NPC.ai[2] = 0;
			Cycle++; // cycle counter
		}

		// change modes if 4 cycles are done
		if (Cycle == 4)
		{
			ModeChangeCounter = 1;
			Cycle = 0;
		}
	}
}
