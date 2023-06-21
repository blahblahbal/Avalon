using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class BleachedEbonyFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Wall.BleachedEbonyFence>();
        AddMapEntry(new Color(100, 100, 100));
        DustType = ModContent.DustType<BleachedEbonyDust>();
        Main.wallLight[Type] = true;
        WallID.Sets.AllowsWind[Type] = true;
        WallID.Sets.AllowsPlantsToGrow[Type] = true;
    }
}
