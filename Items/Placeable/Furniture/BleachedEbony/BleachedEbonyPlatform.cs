using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>())
			.SortAfterFirstRecipesOf(ItemID.AshWoodPlatform)
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.BleachedEbony>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.SortAfterFirstRecipesOf(Type)
			.Register();
	}
}
