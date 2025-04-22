using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Heartstone;

public class HeartstonePlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Heartstone.HeartstonePlatform>());
		Item.width = 8;
		Item.height = 10;
	}
	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>()).Register();
		Recipe.Create(ModContent.ItemType<Material.Ores.Heartstone>()).AddIngredient(this, 2).Register();
	}
}
