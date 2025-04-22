using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.YellowDungeon;

public class YellowDungeonLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowDungeonLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Tile.YellowBrick>(), 3)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
