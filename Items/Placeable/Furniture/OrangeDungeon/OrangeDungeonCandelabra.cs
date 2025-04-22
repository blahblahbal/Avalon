using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.OrangeDungeon;

public class OrangeDungeonCandelabra : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeDungeonCandelabra>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 3);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tile.OrangeBrick>(), 5)
			.AddIngredient(ItemID.Torch, 3)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
