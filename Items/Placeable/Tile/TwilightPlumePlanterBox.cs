using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class TwilightPlumePlanterBox : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.BlinkrootPlanterBox);
        Item.createTile = ModContent.TileType<Tiles.Herbs.TwilightPlumePlanterBox>();
        Item.placeStyle = 0;
    }
}
