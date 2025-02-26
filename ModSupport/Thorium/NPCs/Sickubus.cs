using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Avalon.Items.Material;
using Terraria.GameContent.Events;
using Avalon.ModSupport.Thorium.Projectiles.Hostile;

namespace Avalon.ModSupport.Thorium.NPCs;

public class Sickubus : ModNPC
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Demon];

		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(8f, 0),
			PortraitPositionXOverride = 8f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ThoriumMod.Bestiary.Sickubus")),
		]);

	public override void SetDefaults()
	{
		NPC.lifeMax = 165;
		NPC.damage = 38;
		NPC.defense = 10;
		NPC.knockBackResist = 0.6f;
		NPC.width = 50;
		NPC.height = 50;
		NPC.aiStyle = NPCAIStyleID.Bat;
		NPC.noGravity = true;
		NPC.scale = 1f;
		NPC.HitSound = SoundID.NPCHit13;
		NPC.DeathSound = SoundID.NPCDeath19;
		NPC.lavaImmune = true;
		//Banner = NPC.type;
		//BannerItem = ModContent.ItemType<Items.Banners.BloodyVultureBanner>();
		NPC.value = Item.buyPrice(0, 0, 5, 0);
		AnimationType = NPCID.Demon;
		SpawnModBiomes = [ModContent.GetInstance<Biomes.UndergroundContagion>().Type];
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!(spawnInfo.SpawnTileY >= Main.maxTilesY * 0.91f) ||
			!spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion ||
			!Main.hardMode ||
			!(!spawnInfo.Invasion && (!DD2Event.Ongoing || !spawnInfo.Player.ZoneOldOneArmy) && ((!Main.pumpkinMoon && !Main.snowMoon) || Main.dayTime)))
		{
			return 0f;
		}
		return 0.1f;
	}

	public override bool? CanFallThroughPlatforms()
	{
		return true;
	}

	public override void AI()
	{
		Player player = Main.player[NPC.target];
		if (NPC.wet)
		{
			NPC.velocity.Y -= 0.5f;
		}
		NPC.ai[0] += 1f;
		if (!player.dead && NPC.ai[0] >= 20f && NPC.ai[0] % 20f == 0f)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, NPC.DirectionTo(player.Center) * 0.35f, ModContent.ProjectileType<PathogenSickle>(), 25, 0f, Main.myPlayer, 0f, 0f, 0f);
			}
			if (NPC.ai[0] >= 60f)
			{
				NPC.ai[0] = -180f;
				NPC.netUpdate = true;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (Main.netMode == NetmodeID.Server)
		{
			return;
		}
		if (NPC.life <= 0)
		{
			for (int i = 0; i < 18; i++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, 2.5f * hit.HitDirection, -2.5f, 0, default, 1.6f);
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GreenTorch, 1.2f * hit.HitDirection, -2.5f, 0, default, 2.2f);
			}
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SickubusHead").Type, 1.1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SickubusWing").Type, 1.1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SickubusWing").Type, 1.1f);
		}
		else
		{
			for (int j = 0; j < (double)hit.Damage / (double)NPC.lifeMax * 50d; j++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptGibs, hit.HitDirection, -1f, 0, default, 0.6f);
			}
		}
	}
	public override void ModifyNPCLoot(NPCLoot loot)
	{
		loot.Add(ItemDropRule.Common(ModContent.ItemType<Pathogen>(), 1, 1, 3));
		loot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 1, 1));
	}
}
