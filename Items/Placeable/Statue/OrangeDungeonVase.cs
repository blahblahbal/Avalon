using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Statue;

public class OrangeDungeonVase : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Statues>(), 3);
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
}
