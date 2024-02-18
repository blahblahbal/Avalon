using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trap;

class TuhrtlPressurePlate : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LihzahrdPressurePlate);
        Item.placeStyle = 0;
        Item.createTile = ModContent.TileType<Tiles.Tropics.TuhrtlPressurePlate>();
    }
}
