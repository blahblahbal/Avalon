using Avalon.Common.Extensions;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class FightoftheBumblebee : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 7);
		Item.rare = ItemRarityID.Orange;
	}
}
