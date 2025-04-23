using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class PlatformLeaf : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Savanna.PlatformLeaf>());
		Item.width = 24;
		Item.height = 20;
	}
}
