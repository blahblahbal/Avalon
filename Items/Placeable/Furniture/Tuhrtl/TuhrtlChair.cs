using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Tuhrtl;

public class TuhrtlChair : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlChair>());
		Item.width = 12;
		Item.height = 30;
		Item.value = Item.sellPrice(copper: 30);
	}
}
