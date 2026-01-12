using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class HerbologyBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Crafting.HerbologyBench>());
		Item.width = 22;
		Item.height = 28;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 20);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddRecipeGroup(RecipeGroupID.IronBar, 8).AddRecipeGroup(RecipeGroupID.Wood, 45).AddIngredient(ItemID.GrassSeeds, 20).AddRecipeGroup("Herbs", 15).AddTile(TileID.Anvils).Register();
	}
}
