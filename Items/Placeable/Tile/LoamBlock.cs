using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class LoamBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Savanna.Loam>());
	}
}
