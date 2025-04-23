using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class TourmalineStoneWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.TourmalineStoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.TourmalineStoneBlock>()).AddTile(TileID.WorkBenches).Register();
		Recipe.Create(ModContent.ItemType<Tile.TourmalineStoneBlock>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
	}
}
