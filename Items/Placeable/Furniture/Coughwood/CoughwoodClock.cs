using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodClock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodClock>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("IronBar", 3)
			.AddIngredient(ItemID.Glass, 6)
			.AddIngredient(ModContent.ItemType<Tile.Coughwood>(), 10)
			.AddTile(TileID.Sawmill).Register();
	}
}
