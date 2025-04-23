using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trophy;

public class BacteriumPrimeTrophy : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BossTrophy>(), 4);
		Item.width = 30;
		Item.height = 30;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}
}
