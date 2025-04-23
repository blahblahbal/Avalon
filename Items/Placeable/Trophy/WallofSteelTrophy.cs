using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trophy;

public class WallofSteelTrophy : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BossTrophy>(), 6);
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}
}
