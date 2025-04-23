using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class ZirconGemcorn : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.GemTrees.ZirconSapling>());
		Item.value = Item.sellPrice(0, 0, 33, 75);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Acorn)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>())
			.SortAfterFirstRecipesOf(ItemID.GemTreeDiamondSeed)
			.Register();
	}
}
