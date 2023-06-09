using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ZirconStoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = WallID.Stone;
        AddMapEntry(new Color(52, 52, 52));
        DustType = DustID.Stone;
    }
}
