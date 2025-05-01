using Avalon.Common.Extensions;
using Avalon.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class DanceoftheLotus : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 16);
	}
}
