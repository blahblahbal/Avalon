using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class MoonWallItem : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.MoonWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>()).AddTile(TileID.WorkBenches).Register();
		Terraria.Recipe.Create(ModContent.ItemType<Tile.MoonplateBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
