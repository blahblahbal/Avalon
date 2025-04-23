using Avalon.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class CrossingtheTropics : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 12);
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
	}
}
