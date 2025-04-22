using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.OrangeDungeon;

public class OrangeDungeonChandelier : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.OrangeDungeon.OrangeDungeonChandelier>());
		Item.width = 26;
		Item.height = 26;
		Item.value = Item.sellPrice(silver: 6);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tile.OrangeBrick>(), 4)
			.AddIngredient(ItemID.Torch, 4)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
