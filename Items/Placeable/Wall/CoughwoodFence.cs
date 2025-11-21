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
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOfIndexShift(ItemID.AshWoodFence, 1)
			.Register();

		Terraria.Recipe.Create(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>())
			.AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
