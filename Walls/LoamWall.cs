using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class LoamWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(67, 32, 20));
        DustType = DustID.Stone;// ModContent.DustType<Dusts.TropicalMudDust>();
    }
}
