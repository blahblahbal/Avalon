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
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>())
			.SortAfterFirstRecipesOf(ItemID.AshWoodPlatform)
			.Register();

		Recipe.Create(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
