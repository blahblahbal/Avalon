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

        /// <summary>
        /// Used to add extra walls to the Contagion's worldgen, such as modded stone walls converting to chunkstone walls upon hardmode
        /// <br/> Example:
        /// <br/> Data.Sets.Wall.ConvertsToContagionWall[Type] = WallID.LivingWood;
        /// <br/> This will turn the modded wall of choice into Living wood walls if the contagion's worldgen hits directly ontop of the modded wall
        /// </summary>
        public static int[] ConvertsToContagionWall = WallID.Sets.Factory.CreateIntSet(-1);
    }
}
