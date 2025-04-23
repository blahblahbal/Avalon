using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class SkyBrickWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.SkyBrickWallUnsafe>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.SkyBrick>()).AddTile(TileID.WorkBenches).Register();
		CreateRecipe(1).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
