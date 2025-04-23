using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Statue;

public class BlueLihzahrdStatue : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Statues>(), 6);
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
}
