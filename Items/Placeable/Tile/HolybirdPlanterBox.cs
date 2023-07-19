using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class HolybirdPlanterBox : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.BlinkrootPlanterBox);
        Item.createTile = ModContent.TileType<Tiles.PlanterBoxes>();
        Item.placeStyle = 1;
    }
}
