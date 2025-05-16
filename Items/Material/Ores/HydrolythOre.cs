using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class HydrolythOre : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.HydrolythOre>());
		Item.rare = ModContent.RarityType<Rarities.TealRarity>();
		Item.value = Item.sellPrice(0, 0, 15);
	}
}
