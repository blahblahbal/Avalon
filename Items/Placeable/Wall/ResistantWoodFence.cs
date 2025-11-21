using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

public class ResistantWoodFence : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 400;
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableWall(ModContent.WallType<Walls.ResistantWoodFence>());
	}
	public override void AddRecipes()
	{
		CreateRecipe(4)
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>())
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOfIndexShift(ItemID.AshWoodFence, 1)
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.ResistantWood>())
			.AddIngredient(this, 4)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(Type)
			.DisableDecraft()
			.Register();
	}
}