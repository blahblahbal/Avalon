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

        public static readonly bool[] UnsafeSlabWalls = WallID.Sets.Factory.CreateBoolSet(
            ModContent.WallType<Walls.OrangeSlabUnsafe>(),
            ModContent.WallType<Walls.PurpleSlabWallUnsafe>(),
            ModContent.WallType<Walls.YellowSlabWallUnsafe>()
        );

        public static readonly bool[] UnsafeTiledWalls = WallID.Sets.Factory.CreateBoolSet(
            ModContent.WallType<Walls.OrangeTiledUnsafe>(),
            ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
            ModContent.WallType<Walls.YellowTiledWallUnsafe>()
        );
    }
}
