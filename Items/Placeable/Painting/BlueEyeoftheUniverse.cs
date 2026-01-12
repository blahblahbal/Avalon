using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class BlueEyeoftheUniverse : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.EyeoftheUniverse>(), 1);
		Item.width = 50;
		Item.height = 36;
		Item.rare = ModContent.RarityType<Rarities.MagentaRarity>();
		Item.value = Item.sellPrice(0, 0, 10);
	}
}
