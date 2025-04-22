using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class Catalyzer : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Catalyzer>());
		Item.width = 26;
		Item.height = 26;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);
	}

	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddRecipeGroup(RecipeGroupID.Wood, 20)
	//        .AddRecipeGroup("DemoniteBar", 5)
	//        .AddRecipeGroup(RecipeGroupID.IronBar, 15)
	//        .AddRecipeGroup("WorkBenches")
	//        .AddCondition(Condition.NearShimmer)
	//        .AddTile(TileID.Anvils)
	//        .Register();
	//}
}
