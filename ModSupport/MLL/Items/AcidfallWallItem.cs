using Avalon.ModSupport.MLL.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public class AcidfallWallItem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<AcidfallWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<AcidfallBlock>()).AddTile(TileID.WorkBenches).SortBeforeFirstRecipesOf(ItemID.SandFallBlock).Register();
		Recipe.Create(ModContent.ItemType<AcidfallBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).SortBeforeFirstRecipesOf(ItemID.SandFallBlock).Register();
	}
}
