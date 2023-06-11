using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ContagionMouldWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.NewWall2[Type] = true;
        Main.wallHouse[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<ContagionMouldWall>();
        AddMapEntry(new Color(71, 84, 54));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
