using Avalon.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trophy;

public class EggmanTrophy : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BossTrophy>(), 5);
		Item.width = 30;
		Item.height = 30;
		Item.rare = ModContent.RarityType<TealRarity>();
		Item.value = Item.sellPrice(0, 1);
	}
}
