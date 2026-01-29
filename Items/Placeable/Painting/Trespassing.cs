using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class Trespassing : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 2);
		Item.rare = ItemRarityID.Green;
	}
}
