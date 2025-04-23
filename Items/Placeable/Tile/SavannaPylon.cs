using Terraria.Enums;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class SavannaPylon : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Savanna.SavannaPylon>());
		Item.SetShopValues(ItemRarityColor.Blue1, Terraria.Item.buyPrice(gold: 10));
	}
}
