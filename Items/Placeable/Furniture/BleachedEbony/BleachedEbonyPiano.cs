using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyPiano : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyPiano>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 4)
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>(), 15)
			.AddIngredient(ItemID.Book)
			.AddTile(TileID.Sawmill).Register();
	}
}
