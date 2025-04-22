using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.BleachedEbony;

public class BleachedEbonyLantern : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.BleachedEbony.BleachedEbonyLantern>());
		Item.width = 12;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.BleachedEbony>(), 6)
			.AddIngredient(ItemID.Torch)
			.AddTile(TileID.WorkBenches).Register();
	}
}
