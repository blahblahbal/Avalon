using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class HydrolythOre : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.HydrolythOre>());
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 0, 15);
	}
}
