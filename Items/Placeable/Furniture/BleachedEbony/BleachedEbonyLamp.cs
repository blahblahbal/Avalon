using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>(), 3)
			.AddTile(TileID.WorkBenches).Register();
	}
}
