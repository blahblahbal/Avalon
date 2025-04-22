using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class Grindstone : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Grindstone>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(0, 3, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Extractinator)
			.AddRecipeGroup("GoldBar", 10)
			.AddRecipeGroup(RecipeGroupID.Wood, 15)
			.AddIngredient(ItemID.Chain, 2)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
