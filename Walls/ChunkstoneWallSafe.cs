using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ChunkstoneWallSafe : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.Stone[Type] = true;
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(34, 44, 25));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
