using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TourmalineStoneWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        Main.wallBlend[Type] = WallID.Stone;
        AddMapEntry(new Color(52, 52, 52));
        DustType = DustID.Stone;
    }
}
