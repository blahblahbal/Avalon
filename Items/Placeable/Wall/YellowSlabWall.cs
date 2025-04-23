using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class YellowSlabWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.YellowSlabWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.YellowBrick>())
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.YellowBrick>())
			.AddIngredient(this, 4)
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();
	}
}
