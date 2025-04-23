using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class CoughwoodFence : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.CoughwoodFence>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.Coughwood>()).AddTile(TileID.WorkBenches).Register();
		Terraria.Recipe.Create(ModContent.ItemType<Tile.Coughwood>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
