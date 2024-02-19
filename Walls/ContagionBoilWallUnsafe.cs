using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionBoilWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall4[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionBoilWall>();
        AddMapEntry(new Color(63, 66, 56));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
