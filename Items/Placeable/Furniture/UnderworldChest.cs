using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class UnderworldChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Functional.UnderworldChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 20);
	}
}
