using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Walls;

public class CrystalStoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(103, 127, 195));
        DustType = DustID.PinkCrystalShard;
    }
}
