using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Tuhrtl;

public class TuhrtlTable : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlTable>());
		Item.width = 26;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	//public override void AddRecipes()
	//{
	//    CreateRecipe()
	//        .AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 8)
	//        .AddTile(TileID.WorkBenches).Register();
	//}
}
