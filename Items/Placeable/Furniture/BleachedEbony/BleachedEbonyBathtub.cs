using Avalon.Items.Placeable.Beam;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyBathtub : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyBathtub>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>(), 14)
			.AddTile(TileID.Sawmill)
			.SortAfterFirstRecipesOf(ModContent.ItemType<BleachedEbonyBeam>())
			.Register();
	}
}
