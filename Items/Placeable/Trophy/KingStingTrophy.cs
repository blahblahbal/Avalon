using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trophy;

public class KingStingTrophy : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<BossTrophy>(), 9);
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}
}
