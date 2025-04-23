using Avalon.Tiles;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class BasaltPillar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Basalt>());
	}
}
