using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class ContagionCampfire : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ContagionCampfire>());
		Item.width = 12;
		Item.height = 12;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddRecipeGroup("Wood", 10).AddIngredient(ModContent.ItemType<ContagionTorch>(), 5).Register();
	}
}
