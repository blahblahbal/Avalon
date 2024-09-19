using Avalon.Common;
using Avalon.Projectiles.Hostile.WallOfSteel;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode;

internal class WallofSteelMouthEye : ModNPC
{
	private byte modeChangeCounter = 0;
	private byte Phase = 0;
	private byte Phase1FireballCounter = 0;
	private byte Cycle = 0;
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
	}

	public override void SetDefaults()
	{
		NPC.width = 96;
		NPC.height = 110;
		NPC.noTileCollide = NPC.noGravity = NPC.behindTiles = true;
		NPC.npcSlots = 100f;
		NPC.damage = 150;
		NPC.lifeMax = 93000;
		NPC.timeLeft = 750000;
		NPC.defense = 55;
		NPC.aiStyle = -1;
		NPC.knockBackResist = 0;
		NPC.scale = 1f;
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		//NPC.BossBar = ModContent.GetInstance<BossBars.WallofSteelBossBar>();
		//Music = ExxoAvalonOrigins.MusicMod == null ? MusicID.Boss2 : MusicID.Boss2; // MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/WallofSteel");
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(modeChangeCounter);
		writer.Write(Phase);
		writer.Write(Phase1FireballCounter);
		writer.Write(Cycle);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		modeChangeCounter = reader.ReadByte();
		Phase = reader.ReadByte();
		Phase1FireballCounter = reader.ReadByte();
		Cycle = reader.ReadByte();
	}
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.85f * bossAdjustment);
		NPC.damage = (int)(NPC.damage * 0.65f);
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

		NPC.Center = mainWall.Center + new Vector2(71 * mainWall.direction, -175);
		NPC.direction = mainWall.direction;
		NPC.spriteDirection = mainWall.spriteDirection;

		Vector2 vector38 = NPC.Center;
		float num374 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector38.X;
		float num375 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector38.Y;
		float num376 = (float)Math.Sqrt(num374 * num374 + num375 * num375);
		num374 *= num376;
		num375 *= num376;
		if (modeChangeCounter == 0)
		{
			if (NPC.direction > 0)
			{
				if (Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 > NPC.position.X + NPC.width / 2)
					NPC.rotation = (float)Math.Atan2(0f - num375, 0f - num374) + 3.14f;
				else
					NPC.rotation = 0f;
			}
			else if (Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 < NPC.position.X + NPC.width / 2)
			{
				NPC.rotation = (float)Math.Atan2(num375, num374) + 3.14f;
			}
			else
			{
				NPC.rotation = 0f;
			}
		}

		//Phase = 2;
		if (modeChangeCounter == 0)
		{
			// phase 1 - spawn leeches and shoot fireballs
			if (Phase == 0)
			{
				Phase1FireballCounter++;
				if (Phase1FireballCounter == 90)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) // leeches
					{
						//int num442 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height / 2 + 20f), ModContent.NPCType<MechanicalLeechHead>(), 1);
						//Main.npc[num442].velocity.X = NPC.direction * 8;
					}
				}
				if (Phase1FireballCounter > 90)
				{
					int fire;
					float f = 0f;
					int dmg = Main.expertMode ? 75 : 60;
					var fireballPos = new Vector2(NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
					float rotation = (float)Math.Atan2(fireballPos.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), fireballPos.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
					SoundEngine.PlaySound(SoundID.Item33, new Vector2((int)NPC.position.X, AvalonWorld.WallOfSteelT));
					while (f <= .1f)
					{
						fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation + f) * 12f) * -1), (float)((Math.Sin(rotation + f) * 12f) * -1), ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSCursedFireball>(), dmg, 6f);
						Main.projectile[fire].timeLeft = 600;
						Main.projectile[fire].tileCollide = false;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
						}
						fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation - f) * 12f) * -1), (float)((Math.Sin(rotation - f) * 12f) * -1), ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSCursedFireball>(), dmg, 6f);
						Main.projectile[fire].timeLeft = 600;
						Main.projectile[fire].tileCollide = false;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
						}
						f += .034f;
					}
					Phase1FireballCounter = 0;
				}
				NPC.ai[2]++;
				if (NPC.ai[2] == 90)
				{
					int fire;
					float f = 0f;
					var fireballPos = new Vector2(NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f);
					float rotation = (float)Math.Atan2(fireballPos.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), fireballPos.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
					SoundEngine.PlaySound(SoundID.Item33, new Vector2((int)NPC.position.X, AvalonWorld.WallOfSteelT));
					//while (f <= .1f)
					//{
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation + f) * 12f) * -1), (float)((Math.Sin(rotation + f) * 12f) * -1), ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSCursedFireball>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
					fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), fireballPos.X, fireballPos.Y, (float)((Math.Cos(rotation - f) * 12f) * -1), (float)((Math.Sin(rotation - f) * 12f) * -1), ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSCursedFireball>(), Main.expertMode ? 70 : 55, 6f);
					Main.projectile[fire].timeLeft = 600;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
					f += .1f;
					//}
					NPC.ai[2] = 0;
					Cycle++; // cycle counter
				}

				// change modes if 4 cycles of the fireballs are done
				if (Cycle == 4)
				{
					modeChangeCounter = 1;
					Cycle = 0;
				}
			}
			// phase 2 - flamethrower
			if (Phase == 1)
			{
				NPC.ai[1]++;
				if (NPC.ai[1] > 100 && NPC.ai[1] < 150)
				{
					NPC.defense = 55;
					int fire = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f, NPC.velocity.X, NPC.velocity.Y, ProjectileID.EyeFire, 45, 4f);
					Main.projectile[fire].velocity = Vector2.Normalize(Main.player[NPC.target].Center - new Vector2(NPC.position.X, AvalonWorld.WallOfSteelT)) * 10f;
					Main.projectile[fire].tileCollide = false;
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, fire);
					}
				}
				if (NPC.ai[1] > 300)
				{
					Cycle++;
					NPC.ai[1] = 0;
				}

				// change modes if 4 cycles of the flamethrower are done
				if (Cycle == 3)
				{
					modeChangeCounter = 1;
					Cycle = 0;
				}
			}
			// rocket phase
			if (Phase == 2)
			{
				NPC.ai[2]++;
				if (NPC.ai[2] == 10)
				{
					// do the math to make it shoot out of proper location depending on rotation
					int rocket = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + NPC.DirectionTo(NPC.PlayerTarget().Center).X * 40f, NPC.Center.Y + NPC.DirectionTo(NPC.PlayerTarget().Center).Y * 40f, NPC.velocity.X, NPC.velocity.Y, ModContent.ProjectileType<WoSRocket>(), 65, 4f);
					Main.projectile[rocket].velocity = Vector2.Normalize(Main.player[NPC.target].Center - new Vector2(NPC.position.X, NPC.Center.Y)) * 20f;
					Main.projectile[rocket].ai[2] = Main.rand.NextFromList(Main.rand.NextFloat(-0.8f, 0.8f), Main.rand.NextFloat(-0.3f, 0.3f));
					if (Main.netMode != NetmodeID.SinglePlayer)
					{
						NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, rocket);
					}
					Cycle++;
					NPC.ai[2] = 0;
				}
				if (Cycle == 20)
				{
					modeChangeCounter = 1;
					Cycle = 0;
				}
			}
		}
		if (modeChangeCounter == 1)
		{
			NPC.ai[2] += 0.005f;
			if (NPC.ai[2] > 0.5)
			{
				NPC.ai[2] = 0.5f;
				modeChangeCounter = 2;
			}
			NPC.rotation += NPC.ai[2];
		}
		else if (modeChangeCounter == 2)
		{
			NPC.ai[2] -= 0.005f;
			if (NPC.ai[2] < 0f)
				NPC.ai[2] = 0f;

			NPC.rotation += NPC.ai[2];
			NPC.ai[1]++;

			if (NPC.ai[1] == 100)
			{
				NPC.ai[1] = 0;
				Phase++;
				modeChangeCounter = 0;
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
}
