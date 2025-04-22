using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.PurpleDungeon;

public class PurpleDungeonSofa : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.PurpleDungeon.PurpleDungeonSofa>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tile.PurpleBrick>(), 5)
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
