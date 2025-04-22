using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Tuhrtl;

public class TuhrtlChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}
}
