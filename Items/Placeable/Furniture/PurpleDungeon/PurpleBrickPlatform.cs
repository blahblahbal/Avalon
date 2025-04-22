using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.PurpleDungeon;

public class PurpleBrickPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleBrickPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<PurpleBrick>())
			.Register();

		Recipe.Create(ModContent.ItemType<PurpleBrick>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.Register();
	}
}
