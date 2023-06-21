using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class SnotsandstoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.Sandstone[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<SnotsandstoneWall>();
        AddMapEntry(new Color(67, 70, 59));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
