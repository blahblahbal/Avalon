using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class PurpleSlabWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.PurpleSlabWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.PurpleBrick>())
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.PurpleBrick>())
			.AddIngredient(this, 4)
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();
	}
}
