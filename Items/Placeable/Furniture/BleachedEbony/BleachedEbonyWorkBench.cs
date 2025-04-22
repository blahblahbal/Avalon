using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyWorkBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyWorkBench>());
		Item.width = 28;
		Item.height = 14;
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(ModContent.ItemType<Tile.BleachedEbony>(), 10).Register();
	}
}
