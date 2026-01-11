using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodChair : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodChair>());
		Item.width = 12;
		Item.height = 30;
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.Coughwood>(), 4)
			.AddTile(TileID.WorkBenches)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<CoughwoodBed>())
			.Register();
	}
}
