using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class TheBastion : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings3x3>(), 0);
	}
}
