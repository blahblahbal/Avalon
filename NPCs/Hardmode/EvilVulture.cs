using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;

namespace Avalon.NPCs.Hardmode;

public class EvilVulture : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
    }

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.EvilVulture")),
		]);

	public override void SetDefaults()
    {
        NPC.damage = 45;
        NPC.noTileCollide = false;
        NPC.lifeMax = 300;
        NPC.defense = 15;
        NPC.width = 36;
        NPC.aiStyle = 17;
        NPC.value = 750;
        NPC.timeLeft = 750;
        NPC.height = 36;
        AnimationType = 61;
        AIType = 61;
        NPC.knockBackResist = 0.6f;
        NPC.HitSound = SoundID.NPCHit28;
        NPC.DeathSound = SoundID.NPCDeath31;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<Items.Banners.EvilVultureBanner>();
    }
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.5f);
		NPC.damage = (int)(NPC.damage * 0.5f);
	}
	public override void ModifyNPCLoot(NPCLoot loot)
    {
        loot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Beak>(), 2));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion || spawnInfo.Player.ZoneCorrupt || spawnInfo.Player.ZoneCrimson)
        {
            if (Main.hardMode)
            {
                if (Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY + 1].TileType == TileID.Ebonsand ||
                    Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY + 1].TileType == TileID.Crimsand ||
                    Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY + 1].TileType == ModContent.TileType<Tiles.Contagion.Snotsand>())
                {
                    return 1f;
                }
            }
        }
        return 0f;
    }

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CorruptVultureHead").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CorruptVultureWing").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CorruptVultureWing").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CorruptVultureTalon").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CorruptVultureTalon").Type, 0.9f);
		}
	}
}
