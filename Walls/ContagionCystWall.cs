using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionCystWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall3[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionCystWall>();
        AddMapEntry(new Color(61, 70, 64));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
