using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.YellowDungeon;

public class YellowBrickPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowBrickPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<YellowBrick>())
			.Register();

		Recipe.Create(ModContent.ItemType<YellowBrick>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.Register();
	}
}
