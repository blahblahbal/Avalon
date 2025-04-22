using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class CrystalStoneDoor : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CrystalMines.CrystalStoneDoorClosed>());
		Item.width = 14;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 40);
	}
}
