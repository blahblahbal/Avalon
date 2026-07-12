using Avalon.Items.Banners;
using Avalon.Tiles.Furniture.Functional;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Avalon.NPCs.Underground;

public class MineralSlime : OreSlime
{
	public override int[] Ores =>
	[
		ItemID.CobaltOre, ItemID.PalladiumOre, ModContent.ItemType<Items.Material.Ores.DurataniumOre>(),
		ItemID.MythrilOre, ItemID.OrichalcumOre, ModContent.ItemType<Items.Material.Ores.NaquadahOre>(),
		ItemID.AdamantiteOre, ItemID.TitaniumOre, ModContent.ItemType<Items.Material.Ores.TroxiniumOre>()
	];
	public override Color[] OreColor =>
	[
		new Color(61, 164, 196), new Color(240, 91, 51), new Color(147, 83, 119),
		new Color(157, 210, 144), new Color(248, 113, 227), new Color(80, 86, 160),
		new Color(221, 85, 152), new Color(190, 187, 220), new Color(214, 191, 43)
	];
	public override int[] OreDusts =>
	[
		DustID.Cobalt, DustID.Palladium, ModContent.DustType<Dusts.DurataniumDust>(),
		DustID.Mythril, DustID.Orichalcum, ModContent.DustType<Dusts.NaquadahDust>(),
		DustID.Adamantite, DustID.Titanium, ModContent.DustType<Dusts.TroxiniumDust>()
	];
	public override int BestiaryOre => 6;
	public override void AI()
	{
		base.AI();
		NPC.ai[0]++;
	}
	public override void OnKill()
	{
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, Ores[WhichOre], Main.rand.Next(10, 30));
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(new CommonDrop(ItemID.Gel, 1, 3, 6));
	}
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 2;
		Data.Sets.NPCSets.Earthen[NPC.type] = true;
	}
	public override void SetDefaults()
	{
		NPC.damage = 60;
		NPC.lifeMax = 750;
		NPC.defense = 20;
		NPC.width = 52;
		NPC.aiStyle = 1;
		NPC.value = 1000f;
		NPC.knockBackResist = 0.07f;
		NPC.height = 32;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.alpha = 128;
		NPC.scale = 1f;
		AnimationType = NPCID.BlueSlime;
		BannerItem = ModContent.ItemType<MineralSlimeBanner>();
		Banner = NPC.type;
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.OreSlime")),
		});
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
		spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon && Main.hardMode
			? 0.05f : 0f;
}
