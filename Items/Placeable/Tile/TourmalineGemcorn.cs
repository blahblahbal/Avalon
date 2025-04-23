using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class TourmalineGemcorn : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.GemTrees.TourmalineSapling>());
		Item.value = Item.sellPrice(0, 0, 11, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Acorn)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>())
			.SortAfterFirstRecipesOf(ItemID.GemTreeTopazSeed)
			.Register();
	}
}
