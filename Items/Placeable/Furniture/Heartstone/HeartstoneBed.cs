using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Heartstone;

public class HeartstoneBed : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Heartstone.HeartstoneBed>());
		Item.width = 28;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>(), 15)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
