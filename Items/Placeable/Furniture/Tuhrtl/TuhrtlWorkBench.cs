using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Tuhrtl;

public class TuhrtlWorkBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlWorkBench>());
		Item.width = 28;
		Item.height = 14;
		Item.value = Item.sellPrice(copper: 30);
	}
}
