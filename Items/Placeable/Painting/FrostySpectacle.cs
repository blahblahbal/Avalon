using Avalon.Common.Extensions;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class FrostySpectacle : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 8);
		Item.rare = ItemRarityID.Blue;
	}
}
