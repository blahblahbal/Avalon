using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.YellowDungeon;

public class YellowDungeonWorkBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.YellowDungeon.YellowDungeonWorkbench>());
		Item.width = 28;
		Item.height = 14;
		Item.value = Item.sellPrice(copper: 30);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Tile.YellowBrick>(), 10)
			.AddTile(TileID.BoneWelder)
			.Register();
	}
}
