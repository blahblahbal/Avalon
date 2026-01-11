using Avalon.Items.Placeable.Beam;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodBathtub : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodBathtub>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(itemID: ModContent.ItemType<Tile.Coughwood>(), 14)
			.AddTile(TileID.Sawmill)
			.SortAfterFirstRecipesOf(ModContent.ItemType<CoughwoodBeam>())
			.Register();
	}
}
