using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class ThoughPoppiesGrow : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings3x2>(), 1);
	}
}
