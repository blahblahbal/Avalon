using Avalon.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Statue;

public class Blahtue : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Blahtue>());
		Item.width = 20;
		Item.height = 26;
		Item.rare = ModContent.RarityType<BlahRarity>();
		Item.value = Item.sellPrice(copper: 60);
	}
}
