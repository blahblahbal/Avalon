using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<Tile.Coughwood>())
			.SortAfterFirstRecipesOf(ItemID.AshWoodPlatform)
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.Coughwood>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
