using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TropicalMudWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(67, 32, 20));
        DustType = 1;// ModContent.DustType<Dusts.TropicalMudDust>();
    }
}
