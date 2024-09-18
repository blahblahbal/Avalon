using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode;

internal class WallofSteelLaserEye : ModNPC
{
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

		NPC.Center = mainWall.Center + new Vector2(81 * mainWall.direction, 166);
		NPC.direction = mainWall.direction;
		NPC.spriteDirection = mainWall.spriteDirection;

		Vector2 vector38 = NPC.Center;
		float num374 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector38.X;
		float num375 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector38.Y;
		float num376 = (float)Math.Sqrt(num374 * num374 + num375 * num375);
		num374 *= num376;
		num375 *= num376;
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

		if (NPC.life > NPC.lifeMax / 3)
		{
			NPC.ai[2]++;
			if (NPC.ai[2] == 100)
			{
				int laser = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.velocity.X < 0 ? NPC.Center.X : NPC.Center.X + NPC.width, NPC.Center.Y - 34, NPC.velocity.X, NPC.velocity.Y, ModContent.ProjectileType<Projectiles.Hostile.WallOfSteel.WoSLaserSmall>(), Main.expertMode ? 70 : 55, 4f);
				Main.projectile[laser].velocity = Vector2.Normalize(Main.player[NPC.target].Center - new Vector2(NPC.position.X, AvalonWorld.WallOfSteelB)) * 11f;
				Main.projectile[laser].hostile = true;
				Main.projectile[laser].friendly = false;
				Main.projectile[laser].tileCollide = false;
				if (Main.netMode != NetmodeID.SinglePlayer)
				{
					NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.Empty, laser);
				}

				NPC.ai[2] = 0;
			}
		}
	}
}
