using Avalon.ModSupport.MLL.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class BloodfallWallItem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<BloodfallWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<BloodfallBlock>()).AddTile(TileID.WorkBenches).SortBeforeFirstRecipesOf(ItemID.SandFallBlock).Register();
		Recipe.Create(ModContent.ItemType<BloodfallBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).SortBeforeFirstRecipesOf(ItemID.SandFallBlock).Register();
	}
}
