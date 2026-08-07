using Avalon.Items.Banners;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
namespace Avalon.NPCs.Underground;

public class MineralSlime : OreSlime
{
	public new static bool AddExtraOre(int ore, int oreDust, Color oreColor)
	{
		Ores = Ores.Append(new OreSlimeData(ore, oreDust, oreColor)).ToArray();
		return true;
	}
	public new static OreSlimeData[] Ores = [
		new OreSlimeData(ItemID.CobaltOre, DustID.Cobalt, new Color(61, 164, 196)),
		new OreSlimeData(ItemID.PalladiumOre, DustID.Palladium, new Color(240, 91, 51)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.DurataniumOre>(), ModContent.DustType<Dusts.DurataniumDust>(), new Color(147, 83, 119)),
		new OreSlimeData(ItemID.MythrilOre, DustID.Mythril, new Color(157, 210, 144)),
		new OreSlimeData(ItemID.OrichalcumOre, DustID.Orichalcum, new Color(248, 113, 227)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.NaquadahOre>(), ModContent.DustType<Dusts.NaquadahDust>(), new Color(80, 86, 160)),
		new OreSlimeData(ItemID.AdamantiteOre, DustID.Adamantite, new Color(221, 85, 152)),
		new OreSlimeData(ItemID.TitaniumOre, DustID.Titanium, new Color(190, 187, 220)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.TroxiniumOre>(), ModContent.DustType<Dusts.TroxiniumDust>(), new Color(214, 191, 43)),
		];
	public override int BestiaryOre => 6;

	public override OreSlimeData[] ListOfOres => Ores;
	public override void AI()
	{
		base.AI();
		NPC.ai[0]++;
	}
	public override void OnKill()
	{
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, Ores[WhichOre].OreItemID, Main.rand.Next(10, 30));
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
