using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class Jukebox : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Jukebox>());
		Item.width = 26;
		Item.height = 26;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(gold: 30);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("MusicBoxes", 3)
			.AddIngredient(ItemID.LunarBar, 20)
			.AddRecipeGroup(RecipeGroupID.Wood, 30)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
