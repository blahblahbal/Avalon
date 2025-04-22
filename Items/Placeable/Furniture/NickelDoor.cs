using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class NickelDoor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.NickelDoorClosed>());
		Item.width = 14;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 40);
	}
}
