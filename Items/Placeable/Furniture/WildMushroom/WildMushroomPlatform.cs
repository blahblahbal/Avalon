using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomPlatform : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomPlatform>());
		Item.width = 8;
		Item.height = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(ModContent.ItemType<Material.WildMushroom>()).Register();
		Recipe.Create(ModContent.ItemType<Material.WildMushroom>()).AddIngredient(this, 2).DisableDecraft().Register();
	}
}
