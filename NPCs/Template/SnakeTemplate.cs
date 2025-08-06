using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Template
{
	public enum SnakeSegmentType
	{
		/// <summary>
		/// The head segment for the worm.  Only one "head" is considered to be active for any given worm
		/// </summary>
		Head,
		/// <summary>
		/// The body segment.  Follows the segment in front of it
		/// </summary>
		Body,
		/// <summary>
		/// The tail segment.  Has the same AI as the body segments.  Only one "tail" is considered to be active for any given worm
		/// </summary>
		Tail
	}

	/// <summary>
	/// The base class for non-separating Worm enemies.
	/// </summary>
	public abstract class SnakeTemplate : ModNPC
	{
		/*  ai[] usage:
		 *  
		 *  ai[0] = "follower" segment, the segment that's following this segment
		 *  ai[1] = "following" segment, the segment that this segment is following
		 *  
		 *  localAI[0] = used when syncing changes to collision detection
		 *  localAI[1] = checking if Init() was called
		 */

		/// <summary>
		/// Which type of segment this NPC is considered to be
		/// </summary>
		public abstract SnakeSegmentType SegmentType { get; }

		/// <summary>
		/// The maximum velocity for the NPC
		/// </summary>
		public float MoveSpeed { get; set; }

		/// <summary>
		/// The rate at which the NPC gains velocity
		/// </summary>
		public float Acceleration { get; set; }

		/// <summary>
		/// The NPC instance of the head segment for this worm.
		/// </summary>
		public NPC HeadSegment => Main.npc[NPC.realLife];

		/// <summary>
		/// The NPC instance of the segment that this segment is following (ai[1]).  For head segments, this property always returns <see langword="null"/>.
		/// </summary>
		public NPC FollowingNPC => SegmentType == SnakeSegmentType.Head ? null : Main.npc[(int)NPC.ai[1]];

		/// <summary>
		/// The NPC instance of the segment that is following this segment (ai[0]).  For tail segment, this property always returns <see langword="null"/>.
		/// </summary>
		public NPC FollowerNPC => SegmentType == SnakeSegmentType.Tail ? null : Main.npc[(int)NPC.ai[0]];

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return SegmentType == SnakeSegmentType.Head ? null : false;
		}

		private bool startDespawning;

		public sealed override bool PreAI()
		{
			if (NPC.localAI[1] == 0)
			{
				NPC.localAI[1] = 1f;
				Init();
			}

			if (SegmentType == SnakeSegmentType.Head)
			{
				HeadAI();

				if (!NPC.HasValidTarget)
				{
					NPC.TargetClosest(true);

					// If the NPC is a boss and it has no target, force it to fall to the underworld quickly
					if (!NPC.HasValidTarget && NPC.boss)
					{
						NPC.velocity.Y += 8f;

						MoveSpeed = 1000f;

						if (!startDespawning)
						{
							startDespawning = true;

							// Despawn after 90 ticks (1.5 seconds) if the NPC gets far enough away
							NPC.timeLeft = 90;
						}
					}
				}
			}
			else
			{
				BodyTailAI();
			}

			return true;
		}

		// Not visible to public API, but is used to indicate what AI to run
		internal virtual void HeadAI() { }

		internal virtual void BodyTailAI() { }

		public abstract void Init();
	}

	/// <summary>
	/// The base class for head segment NPCs of Worm enemies
	/// </summary>
	public abstract class SnakeHeadTemplate : SnakeTemplate
	{
		public sealed override SnakeSegmentType SegmentType => SnakeSegmentType.Head;

		/// <summary>
		/// The NPCID or ModContent.NPCType for the body segment NPCs.<br/>
		/// This property is only used if <see cref="HasCustomBodySegments"/> returns <see langword="false"/>.
		/// </summary>
		public abstract int BodyType { get; }

		/// <summary>
		/// The NPCID or ModContent.NPCType for the tail segment NPC.<br/>
		/// This property is only used if <see cref="HasCustomBodySegments"/> returns <see langword="false"/>.
		/// </summary>
		public abstract int TailType { get; }

		/// <summary>
		/// The minimum amount of segments expected, including the head and tail segments
		/// </summary>
		public int MinSegmentLength { get; set; }

		/// <summary>
		/// The maximum amount of segments expected, including the head and tail segments
		/// </summary>
		public int MaxSegmentLength { get; set; }

		/// <summary>
		/// Whether the NPC ignores tile collision when attempting to "dig" through tiles, like how Wyverns work.
		/// </summary>
		public bool CanFly { get; set; }

		/// <summary>
		/// The maximum distance in <b>pixels</b> within which the NPC will use tile collision, if <see cref="CanFly"/> returns <see langword="false"/>.<br/>
		/// Defaults to 1000 pixels, which is equivalent to 62.5 tiles.
		/// </summary>
		public virtual int MaxDistanceForUsingTileCollision => 1000;

		/// <summary>
		/// Whether the NPC uses 
		/// </summary>
		public virtual bool HasCustomBodySegments => false;

		/// <summary>
		/// If not <see langword="null"/>, this NPC will target the given world position instead of its player target
		/// </summary>
		public Vector2? ForcedTargetPosition { get; set; }

		/// <summary>
		/// Override this method to use custom body-spawning code.<br/>
		/// This method only runs if <see cref="HasCustomBodySegments"/> returns <see langword="true"/>.
		/// </summary>
		/// <param name="segmentCount">How many body segments are expected to be spawned</param>
		/// <returns>The whoAmI of the most-recently spawned NPC, which is the result of calling <see cref="NPC.NewNPC(Terraria.DataStructures.IEntitySource, int, int, int, int, float, float, float, float, int)"/></returns>
		public virtual int SpawnBodySegments(int segmentCount)
		{
			// Defaults to just returning this NPC's whoAmI, since the tail segment uses the return value as its "following" NPC index
			return NPC.whoAmI;
		}

		/// <summary>
		/// Spawns a body or tail segment of the snake.
		/// </summary>
		/// <param name="source">The spawn source</param>
		/// <param name="type">The ID of the segment NPC to spawn</param>
		/// <param name="latestNPC">The whoAmI of the most-recently spawned segment NPC in the snake, including the head</param>
		/// <returns></returns>
		protected int SpawnSegment(IEntitySource source, int type, int latestNPC)
		{
			// We spawn a new NPC, setting latestNPC to the newer NPC, while also using that same variable
			// to set the parent of this new NPC. The parent of the new NPC (may it be a tail or body part)
			// will determine the movement of this new NPC.
			// Under there, we also set the realLife value of the new NPC, because of what is explained above.
			int oldLatest = latestNPC;
			latestNPC = NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.whoAmI, 0, latestNPC);

			Main.npc[oldLatest].ai[0] = latestNPC;

			NPC latest = Main.npc[latestNPC];
			// NPC.realLife is the whoAmI of the NPC that the spawned NPC will share its health with
			latest.realLife = NPC.whoAmI;

			return latestNPC;
		}

		internal sealed override void HeadAI()
		{
			HeadAI_SpawnSegments();
			HeadAI_Movement_SetRotation(true);
			HeadAI_Slither();



			//bool collision = HeadAI_CheckCollisionForDustSpawns();

			//HeadAI_CheckTargetDistance(ref collision);

			//HeadAI_Movement(true);
		}
		private void HeadAI_JumpAtPlayer()
		{
			NPC.ai[2]++;
			if (Vector2.Distance(NPC.Center, NPC.PlayerTarget().Center) < 160)
			{
				NPC.velocity.X *= 1.05f;
				NPC.velocity.Y *= 1.05f;
				if (NPC.velocity.X > 4.5f) NPC.velocity.X = 4.5f;
				if (NPC.velocity.X < -4.5f) NPC.velocity.X = -4.5f;
				if (NPC.velocity.Y < -4.5f) NPC.velocity.Y = -4.5f;
				if (NPC.velocity.Y > 4.5f) NPC.velocity.Y = 4.5f;
				if (NPC.ai[2] == 140) NPC.ai[2] = 0;
			}
		}
		private void HeadAI_Slither()
		{
			Point a = NPC.Bottom.ToTileCoordinates();
			if (NPC.collideY || Main.tileSolid[Main.tile[a.X, a.Y].TileType] && Main.tile[a.X, a.Y].HasTile)
			{
				Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
			}

			// right side
			if (NPC.velocity.X > 0 && NPC.Center.X >= NPC.PlayerTarget().Center.X + 16 * 4)
			{
				NPC.velocity.Y -= 0.4f;
				if (NPC.velocity.Y < -3) NPC.velocity.Y = -3;
			}
			else if (NPC.Center.X < NPC.PlayerTarget().Center.X)
			{
				if (NPC.velocity.Y > 0f)
					NPC.velocity.Y *= 0.9f;
				NPC.velocity.X += 0.05f;
				if (NPC.velocity.X > 3f) NPC.velocity.X = 3f;
				NPC.ai[2] = 0;
			}
			// left side
			if (NPC.velocity.X < 0 && NPC.Center.X < NPC.PlayerTarget().Center.X - 16 * 4)
			{
				NPC.velocity.Y -= 0.4f;
				if (NPC.velocity.Y < -3) NPC.velocity.Y = -3;
			}
			else if (NPC.Center.X >= NPC.PlayerTarget().Center.X)
			{
				if (NPC.velocity.Y > 0f)
					NPC.velocity.Y *= 0.9f;
				NPC.velocity.X -= 0.05f;
				if (NPC.velocity.X < -3f) NPC.velocity.X = -3f;
				NPC.ai[2] = 0;
			}
		}
		private void HeadAI_SpawnSegments()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				// So, we start the AI off by checking if NPC.ai[0] (the following NPC's whoAmI) is 0.
				// This is practically ALWAYS the case with a freshly spawned NPC, so this means this is the first update.
				// Since this is the first update, we can safely assume we need to spawn the rest of the worm (bodies + tail).
				bool hasFollower = NPC.ai[0] > 0;
				if (!hasFollower)
				{
					// So, here we assign the NPC.realLife value.
					// The NPC.realLife value is mainly used to determine which NPC loses life when we hit this NPC.
					// We don't want every single piece of the worm to have its own HP pool, so this is a neat way to fix that.
					NPC.realLife = NPC.whoAmI;
					// latestNPC is going to be used in SpawnSegment() and I'll explain it there.
					int latestNPC = NPC.whoAmI;

					// Here we determine the length of the worm.
					int randomSnakeLength = Main.rand.Next(MinSegmentLength, MaxSegmentLength + 1);

					int distance = randomSnakeLength - 2;

					IEntitySource source = NPC.GetSource_FromAI();

					if (HasCustomBodySegments)
					{
						// Call the method that'll handle spawning the body segments
						latestNPC = SpawnBodySegments(distance);
					}
					else
					{
						// Spawn the body segments like usual
						while (distance > 0)
						{
							latestNPC = SpawnSegment(source, BodyType, latestNPC);
							distance--;
						}
					}

					// Spawn the tail segment
					SpawnSegment(source, TailType, latestNPC);

					NPC.netUpdate = true;

					// Ensure that all of the segments could spawn.  If they could not, despawn the worm entirely
					int count = 0;
					foreach (var n in Main.ActiveNPCs)
					{
						if ((n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
							count++;
					}

					if (count != randomSnakeLength)
					{
						// Unable to spawn all of the segments... kill the worm
						foreach (var n in Main.ActiveNPCs)
						{
							if ((n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
							{
								n.active = false;
								n.netUpdate = true;
							}
						}
					}

					// Set the player target for good measure
					NPC.TargetClosest(true);
				}
			}
		}

		private void HeadAI_Movement_SetRotation(bool collision)
		{
			// Set the correct rotation for this NPC.
			// Assumes the sprite for the NPC points upward.  You might have to modify this line to properly account for your NPC's orientation
			NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;

			// Some netupdate stuff (multiplayer compatibility).
			if (collision)
			{
				if (NPC.localAI[0] != 1)
					NPC.netUpdate = true;

				NPC.localAI[0] = 1f;
			}
			else
			{
				if (NPC.localAI[0] != 0)
					NPC.netUpdate = true;

				NPC.localAI[0] = 0f;
			}

			// Force a netupdate if the NPC's velocity changed sign and it was not "just hit" by a player
			if (((NPC.velocity.X > 0 && NPC.oldVelocity.X < 0) || (NPC.velocity.X < 0 && NPC.oldVelocity.X > 0) || (NPC.velocity.Y > 0 && NPC.oldVelocity.Y < 0) || (NPC.velocity.Y < 0 && NPC.oldVelocity.Y > 0)) && !NPC.justHit)
				NPC.netUpdate = true;
		}
	}

	public abstract class SnakeBodyTemplate : SnakeTemplate
	{
		public sealed override SnakeSegmentType SegmentType => SnakeSegmentType.Body;

		internal override void BodyTailAI()
		{
			CommonAI_BodyTail(this);
		}

		internal static void CommonAI_BodyTail(SnakeTemplate snake)
		{
			if (!snake.NPC.HasValidTarget)
				snake.NPC.TargetClosest(true);

			if (Main.player[snake.NPC.target].dead && snake.NPC.timeLeft > 30000)
				snake.NPC.timeLeft = 10;

			NPC following = snake.NPC.ai[1] >= Main.maxNPCs ? null : snake.FollowingNPC;
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				// Some of these conditions are possible if the body/tail segment was spawned individually
				// Kill the segment if the segment NPC it's following is no longer valid
				if (following is null || !following.active || following.friendly || following.townNPC || following.lifeMax <= 5)
				{
					snake.NPC.life = 0;
					snake.NPC.HitEffect(0, 10);
					snake.NPC.active = false;
				}
			}

			if (following is not null)
			{
				// Follow behind the segment "in front" of this NPC
				// Use the current NPC.Center to calculate the direction towards the "parent NPC" of this NPC.
				float dirX = following.Center.X - snake.NPC.Center.X;
				float dirY = following.Center.Y - snake.NPC.Center.Y;
				// We then use Atan2 to get a correct rotation towards that parent NPC.
				// Assumes the sprite for the NPC points upward.  You might have to modify this line to properly account for your NPC's orientation
				snake.NPC.rotation = (float)Math.Atan2(dirY, dirX) + MathHelper.PiOver2;
				// We also get the length of the direction vector.
				float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
				// We calculate a new, correct distance.
				float dist = (length - snake.NPC.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;

				// Reset the velocity of this NPC, because we don't want it to move on its own
				snake.NPC.velocity = Vector2.Zero;
				// And set this NPCs position accordingly to that of this NPCs parent NPC.
				snake.NPC.position.X += posX;
				snake.NPC.position.Y += posY;
				Point tilePos = snake.NPC.position.ToTileCoordinates();
				while (Main.tile[tilePos.X, tilePos.Y].HasTile && Main.tileSolid[Main.tile[tilePos.X, tilePos.Y].TileType])
				{
					snake.NPC.position.Y -= 8;
					Point tilePos2 = snake.NPC.position.ToTileCoordinates();
					if (!Main.tile[tilePos2.X, tilePos2.Y].HasTile || !Main.tileSolid[Main.tile[tilePos2.X, tilePos2.Y].TileType])
					{
						break;
					}
				}
			}
		}
	}

	// Since the body and tail segments share the same AI
	public abstract class SnakeTailTemplate : SnakeTemplate
	{
		public sealed override SnakeSegmentType SegmentType => SnakeSegmentType.Tail;

		internal override void BodyTailAI()
		{
			SnakeBodyTemplate.CommonAI_BodyTail(this);
		}
	}
}
