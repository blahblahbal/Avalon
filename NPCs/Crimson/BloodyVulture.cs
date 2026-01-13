using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace Avalon.NPCs.Crimson;

public class BloodyVulture : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 6;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frozen] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2(0, 8f),
			PortraitPositionYOverride = 13f
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(
		[
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BloodyVulture")),
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
        BannerItem = ModContent.ItemType<Items.Banners.BloodyVultureBanner>();
		if (NPC.IsABestiaryIconDummy)
		{
			NPC.noGravity = true;
			NPC.velocity.Y = 1f;
		}
	}
	public override void ModifyNPCLoot(NPCLoot loot)
    {
        loot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Material.Beak>(), 2));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneCrimson)
        {
            if (Main.hardMode)
            {
                if (Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY + 1].TileType == TileID.Crimsand)
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
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BloodyVultureHead").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BloodyVultureWing").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BloodyVultureWing").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BloodyVultureTalon").Type, 0.9f);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BloodyVultureTalon").Type, 0.9f);
		}
	}
}
