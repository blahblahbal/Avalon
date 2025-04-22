using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.OrangeDungeon;

public class OrangeDungeonLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeDungeonLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Tile.OrangeBrick>(), 3)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
