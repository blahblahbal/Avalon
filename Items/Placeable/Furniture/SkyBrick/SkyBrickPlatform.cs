using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.SkyBrick;

public class SkyBrickPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.SkyBrick.SkyBrickPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<Tile.SkyBrick>())
			.Register();

		Recipe.Create(ModContent.ItemType<Tile.SkyBrick>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.Register();
	}
}
