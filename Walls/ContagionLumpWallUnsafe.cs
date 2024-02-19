using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionLumpWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall1[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionLumpWall>();
        AddMapEntry(new Color(61, 71, 51));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
