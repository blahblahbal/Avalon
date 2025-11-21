using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodBed : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodBed>());
		Item.width = 28;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>(), 15)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<CoughwoodBathtub>())
			.Register();
	}
}
