using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.YellowDungeon;

public class YellowDungeonCandle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowDungeonCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.value = Item.sellPrice(copper: 60);
		Item.noWet = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tile.YellowBrick>(), 4)
			.AddIngredient(ItemID.Torch)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
