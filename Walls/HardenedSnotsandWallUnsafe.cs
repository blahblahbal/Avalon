using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class HardenedSnotsandWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.HardenedSand[Type] = true;
        WallID.Sets.AllowsUndergroundDesertEnemiesToSpawn[Type] = true;
        Main.wallBlend[Type] = ModContent.WallType<HardenedSnotsandWall>();
        AddMapEntry(new Color(67, 70, 59));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
