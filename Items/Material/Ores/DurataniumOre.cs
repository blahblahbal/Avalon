using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class DurataniumOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.DurataniumOre>());
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 10);
	}
}
