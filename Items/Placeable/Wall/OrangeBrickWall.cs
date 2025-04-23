using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class OrangeBrickWall : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.OrangeBrickWall>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.OrangeBrick>())
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.OrangeBrick>())
			.AddIngredient(this, 4)
			.AddTile(TileID.HeavyWorkBench)
			.DisableDecraft()
			.Register();
	}
}
