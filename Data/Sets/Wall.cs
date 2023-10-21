using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Data.Sets
{
    internal class Wall
    {
        public static readonly bool[] Chunkstone = WallID.Sets.Factory.CreateBoolSet(
            ModContent.WallType<Walls.ChunkstoneWall>()
        );

        public static readonly bool[] Hellcastle = WallID.Sets.Factory.CreateBoolSet();
    }
}
