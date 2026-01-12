using Avalon.Items.Material.Bars;
using Avalon.Tiles.Furniture.Metal;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class NickelDoor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<NickelDoorClosed>());
		Item.width = 14;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 40);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<NickelBar>(), 4)
			.AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.LeadDoor)
			.Register();
	}
}
