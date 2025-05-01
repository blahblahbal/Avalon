using Avalon.Common.Extensions;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class PlanterasRage : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 6);
		Item.rare = ItemRarityID.Lime;
	}
}
