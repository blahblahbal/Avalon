using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwarePlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwarePlatform>());
		Item.width = 8;
		Item.height = 10;
	}
	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>()).Register();
		Recipe.Create(ModContent.ItemType<Tile.MoonplateBlock>()).AddIngredient(this, 2).Register();
	}
}
