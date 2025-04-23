using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class SweetstemPlanterBox : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(ItemID.BlinkrootPlanterBox);
		Item.createTile = ModContent.TileType<Tiles.Herbs.SweetstemPlanterBox>();
		Item.placeStyle = 0;
	}
}
