using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodDoor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodDoorClosed>());
		Item.width = 14;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.Coughwood>(), 6)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<CoughwoodClock>())
			.Register();
	}
}
