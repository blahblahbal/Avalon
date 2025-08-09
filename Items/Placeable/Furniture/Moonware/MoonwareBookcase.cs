using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareBookcase : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareBookcase>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 20)
			.AddIngredient(ItemID.Book, 10)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
