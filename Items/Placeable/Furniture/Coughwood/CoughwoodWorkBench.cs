using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodWorkBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodWorkBench>());
		Item.width = 28;
		Item.height = 14;
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(ModContent.ItemType<Tile.Coughwood>(), 10).Register();
	}
}
