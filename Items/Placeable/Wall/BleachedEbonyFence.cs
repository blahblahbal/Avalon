using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class BleachedEbonyFence : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.BleachedEbonyFence>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOfIndexShift(ItemID.AshWoodFence, 1)
			.Register();

		Terraria.Recipe.Create(ModContent.ItemType<Tile.BleachedEbony>())
			.AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
