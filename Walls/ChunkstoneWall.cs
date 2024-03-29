using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class ChunkstoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        WallID.Sets.Conversion.Stone[Type] = true;
        WallID.Sets.CannotBeReplacedByWallSpread[Type] = true;
        AddMapEntry(new Color(38, 49, 33));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
