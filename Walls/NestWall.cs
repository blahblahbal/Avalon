using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NestWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(130, 113, 96));
        DustType = DustID.MarblePot;
    }
}
