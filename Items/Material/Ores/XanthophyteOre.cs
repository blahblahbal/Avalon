using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class XanthophyteOre : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.XanthophyteOre>());
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 0, 15);
	}
}
