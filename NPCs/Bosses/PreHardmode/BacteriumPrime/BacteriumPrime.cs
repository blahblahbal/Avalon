using Avalon;
using Avalon.Biomes;
using Avalon.Items.BossBags;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.Items.Pets;
using Avalon.Items.Placeable.Trophy;
using Avalon.Items.Placeable.Trophy.Relics;
using Avalon.Items.Vanity;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.PreHardmode.BacteriumPrime;

[AutoloadBossHead]
public class BacteriumPrime : ModNPC
{
	public static int secondStageHeadSlot = -1;
	public override void Load()
	{
		secondStageHeadSlot = Mod.AddBossHeadTexture(BossHeadTexture + "_2", -1);
	}
	public override void BossHeadSlot(ref int index)
	{
		if (!NPC.dontTakeDamage)
		{
			index = secondStageHeadSlot;
		}
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BacteriumPrime"))
		});
	}
	public override void SetStaticDefaults()
	{
		NPCID.Sets.MPAllowedEnemies[Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(0, 10f),
			PortraitPositionYOverride = 8,
			Velocity = 1f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

		Main.npcFrameCount[NPC.type] = 8;
		NPCID.Sets.TrailCacheLength[NPC.type] = 12;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
	}
	public override void SetDefaults()
	{
		NPC.damage = 27;
		NPC.boss = true;
		NPC.noTileCollide = true;
		NPC.lifeMax = 1250;
		NPC.defense = 9;
		NPC.noGravity = true;
		NPC.width = NPC.height = 90;
		NPC.aiStyle = -1;
		NPC.npcSlots = 6f;
		NPC.value = 50000f;
		NPC.HitSound = SoundID.NPCHit8;
		NPC.DeathSound = SoundID.NPCDeath21;
		NPC.knockBackResist = 0f;
		NPC.timeLeft = 200000;
		NPC.dontTakeDamage = true;
		Music = ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/BacteriumPrime") : MusicID.Boss5;
		SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<UndergroundContagion>().Type };
	}
	public override void OnKill()
	{
		if (!NPC.downedBoss2 || Main.rand.NextBool(2))
		{
			WorldGen.spawnMeteor = true;
		}
		if (!NPC.downedBoss2)
		{
			NPC.SetEventFlagCleared(ref NPC.downedBoss2, -1);
		}
		if (!ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime)
		{
			NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime, -1);
		}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeTrophy>(), 10));
		notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacteriumPrimeMask>(), 7));
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BacteriumPrimeBossBag>()));
		notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 15, 41));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Booger>(), 1, 12, 17));
		npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<BacteriumPrimeRelic>()));
		npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<PetriDish>(), 4));
	}
	private Player Target { get => Main.player[NPC.target]; }
	public override void AI()
	{
		Lighting.AddLight(NPC.Center, new Vector3(0.8f, 1.4f, 0) * NPC.Opacity * (0.3f + MathF.Sin((float)Main.timeForVisualEffects * 0.04f) * 0.15f));
		// this is for the afterimages
		NPC.localAI[3] = (int)Main.timeForVisualEffects % 6;

		// despawning and targetting
		if (!NPC.HasValidTarget)
		{
			NPC.TargetClosest();
			if (!NPC.HasValidTarget)
			{
				NPC.despawnEncouraged = true;
			}
		}
		if (NPC.despawnEncouraged)
		{
			NPC.velocity.Y -= 0.1f;
			NPC.alpha += 10;
			return;
		}
		// count the tendrils
		if (NPC.dontTakeDamage)
		{
			NPC.ReflectProjectiles(NPC.Hitbox);
			Phase1();
		}
		else
		{
			Phase2();
		}
	}
	private void Phase1()
	{
		bool outside = Main.tile[NPC.Center.ToTileCoordinates()].WallType == 0;

		int tendrilType = ModContent.NPCType<BacteriumTendril>();
		List<int> tendrilWHOAMIs = new();
		foreach (NPC n in Main.ActiveNPCs)
		{
			if (n.type == tendrilType && n.ai[3] == NPC.whoAmI)
			{
				tendrilWHOAMIs.Add(n.whoAmI);
			}
		}
		// shooting
		NPC.ai[2]+= outside? 1.5f : 1;
		if (NPC.ai[2] >= 170 + (tendrilWHOAMIs.Count * 20) && tendrilWHOAMIs.Count > 0)
		{
			int rand = tendrilWHOAMIs[Main.rand.Next(tendrilWHOAMIs.Count)];
			if (Main.npc[rand].ai[2] == 0)
			{
				Main.npc[rand].ai[2] = 1;
				Main.npc[rand].netUpdate = true;
				NPC.ai[2] = 0;
			}
		}

		if (NPC.ai[0] > 0) // Dash attack
		{
			float dashDuration = 80;
			NPC.ai[1]++;
			if (NPC.ai[1] < 60)
			{
				NPC.velocity *= 0.99f;
			}
			else if (NPC.ai[1] == 60)
			{
				NPC.ai[3] = Main.rand.NextBool() ? -1 : 1;
				NPC.netUpdate = true;
				for (int i = 0; i < NPC.oldPos.Length; i++)
				{
					NPC.oldPos[i] = Vector2.Zero;
				}
				NPC.localAI[0] = 1;
				SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
				NPC.velocity = NPC.Center.DirectionTo(Target.Center).RotatedBy(MathHelper.PiOver2 * NPC.ai[3]) * 13;
			}
			else if (NPC.ai[1] < 60 + dashDuration)
			{
				NPC.velocity = NPC.velocity.RotatedBy(NPC.ai[3] * -(MathHelper.Pi / dashDuration));
			}
			else if (NPC.ai[1] > 60 + dashDuration)
			{
				NPC.ai[0] = NPC.ai[1] = 0;
				NPC.velocity = NPC.Center.DirectionTo(Target.Center) * 3;
			}
		}
		else // simple flying
		{
			if (tendrilWHOAMIs.Count == 0)
			{
				NPC.ai[3]++;
				NPC.velocity *= 0.98f;
				if (NPC.ai[3] == 60)
				{
					NPC.dontTakeDamage = false;
				}
			}
			else
			{
				float speedMultiplier = 1f;
				float accelMultiplier = 1f;
				if (NPC.Center.Distance(Target.Center) > 800)
				{
					speedMultiplier = accelMultiplier = 3f;
				}
				if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height) || outside)
				{
					accelMultiplier *= 7f;
				}
				NPC.SimpleFlyMovement(NPC.Center.DirectionTo(Target.Center) * Utils.Remap(tendrilWHOAMIs.Count, 0, 10, 3f, 1f) * speedMultiplier, Utils.Remap(tendrilWHOAMIs.Count, 0, 10, 0.007f, 0.003f) * accelMultiplier);
			}
		}
		if (NPC.localAI[0] > 0 && NPC.ai[1] < 60 && NPC.dontTakeDamage)
			NPC.localAI[0] -= 0.05f;
	}
	private void Phase2()
	{
		NPC.localAI[0] = 1;
	}
	public override void OnSpawn(IEntitySource source)
	{
		int tendril = ModContent.NPCType<BacteriumTendril>();
		for(int i = 0; i < 12; i++)
		{
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y,tendril,NPC.whoAmI,i / 12f * MathHelper.TwoPi, Main.rand.Next(100), 0, NPC.whoAmI);
		}
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		for (int i = NPCID.Sets.TrailCacheLength[Type] - 1; i >= 0; i--)
		{
			float percent = i / (float)NPCID.Sets.TrailCacheLength[Type];
			percent += (1f / NPCID.Sets.TrailCacheLength[Type]) * (NPC.localAI[3] / 6f);
			spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.oldPos[i] - screenPos + NPC.Size / 2, NPC.frame, Color.Lerp(NPC.GetNPCColorTintedByBuffs(new Color(Lighting.GetSubLight(NPC.oldPos[i] + NPC.Size / 2))), Color.Black, 0.2f + (percent * 0.2f)) * (1f - percent) * 0.3f * NPC.Opacity * NPC.localAI[0], NPC.oldRot[i], new Vector2(66), NPC.scale, SpriteEffects.None, 0);
		}
		spriteBatch.Draw(TextureAssets.Npc[Type].Value, NPC.Center - screenPos, NPC.frame, drawColor * NPC.Opacity, NPC.rotation, new Vector2(66), NPC.scale, SpriteEffects.None, 0);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
	}
	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter ++;
		if (NPC.frameCounter > 6)
		{
			NPC.frameCounter = 0;
			NPC.frame.Y += frameHeight;
			if (NPC.dontTakeDamage)
			{
				if (NPC.frame.Y > frameHeight * 3)
					NPC.frame.Y = 0;
			}
			else
			{
				if (NPC.frame.Y > frameHeight * 7)
					NPC.frame.Y = frameHeight * 4;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
				Mod.Find<ModGore>("BacteriumPrime1").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
				Mod.Find<ModGore>("BacteriumPrime2").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity * 0.8f,
				Mod.Find<ModGore>("BacteriumPrime3").Type, NPC.scale);
			for (int i = 0; i < 30; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-4, 2), 50, default, 2);
				Main.dust[d].velocity += NPC.velocity * Main.rand.NextFloat(0.6f, 1f);
				Main.dust[d].noGravity = true;
				Main.dust[d].fadeIn = 1.2f;
			}
		}
		for (int i = 0; i < 15; i++)
		{
			int d2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 1), 50, default, Main.rand.NextFloat(1, 1.5f));
			Main.dust[d2].velocity += NPC.velocity * Main.rand.NextFloat(0.2f, 0.8f);
			Main.dust[d2].noGravity = true;
		}
	}
}
