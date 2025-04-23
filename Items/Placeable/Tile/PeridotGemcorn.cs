using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class PeridotGemcorn : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.GemTrees.PeridotSapling>());
		Item.value = Item.sellPrice(0, 0, 22, 50);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Acorn)
			.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>())
			.SortAfterFirstRecipesOf(ItemID.GemTreeEmeraldSeed)
			.Register();
	}
}
