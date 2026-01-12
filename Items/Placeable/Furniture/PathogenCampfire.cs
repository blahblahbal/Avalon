using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class PathogenCampfire : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Pathogen.PathogenCampfire>());
		Item.width = 12;
		Item.height = 12;
	}
	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("Wood", 10).AddIngredient(ModContent.ItemType<PathogenTorch>(), 5).Register();
	}
}
