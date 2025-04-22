using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Tuhrtl;

public class TuhrtlDoor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlDoorClosed>());
		Item.width = 14;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 40);
	}
}
