using Avalon.Common.Extensions;
using Avalon.Tiles.Furniture;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class Forgotten : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings2x3>(), 3);
	}
}
