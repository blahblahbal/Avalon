using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareCandle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.value = Item.sellPrice(copper: 60);
		Item.noWet = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 4)
			.AddIngredient(ItemID.Torch)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
