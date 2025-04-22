using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.OrangeDungeon;

public class OrangeBrickPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeBrickPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<OrangeBrick>())
			.Register();

		Recipe.Create(ModContent.ItemType<OrangeBrick>())
			.AddIngredient(this, 2)
			.DisableDecraft()
			.Register();
	}
}
