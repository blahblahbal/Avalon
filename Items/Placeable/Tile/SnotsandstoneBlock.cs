using Terraria.ModLoader;
using Terraria;
using Avalon.Tiles.Contagion;

namespace Avalon.Items.Placeable.Tile;
public class SnotsandstoneBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Snotsandstone>());
	}
}