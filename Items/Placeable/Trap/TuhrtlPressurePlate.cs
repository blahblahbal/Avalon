using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Trap;

public class TuhrtlPressurePlate : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LihzahrdPressurePlate);
        Item.placeStyle = 0;
        Item.createTile = ModContent.TileType<Tiles.Savanna.TuhrtlPressurePlate>();
    }
}
