using Terraria;
using Terraria.ID;

namespace ExxoAvalonOrigins.WorldGeneration
{
    public class Utils
    {
        public static void SquareTileFrame(int i, int j, bool resetFrame = true, bool resetSlope = false, bool largeHerb = false)
        {
            if (resetSlope)
            {
                Tile t = Main.tile[i, j];
                t.Slope = SlopeType.Solid;
                t.IsHalfBlock = false;
            }
            WorldGen.TileFrame(i - 1, j - 1, false, largeHerb);
            WorldGen.TileFrame(i - 1, j, false, largeHerb);
            WorldGen.TileFrame(i - 1, j + 1, false, largeHerb);
            WorldGen.TileFrame(i, j - 1, false, largeHerb);
            WorldGen.TileFrame(i, j, resetFrame, largeHerb);
            WorldGen.TileFrame(i, j + 1, false, largeHerb);
            WorldGen.TileFrame(i + 1, j - 1, false, largeHerb);
            WorldGen.TileFrame(i + 1, j, false, largeHerb);
            WorldGen.TileFrame(i + 1, j + 1, false, largeHerb);
        }
        /// <summary>
        /// A helper method to run WorldGen.SquareTileFrame() over an area.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="r">The radius.</param>
        /// <param name="lh">Whether or not to use Large Herb logic.</param>
        public static void SquareTileFrameArea(int x, int y, int r, bool lh = false)
        {
            for (int i = x - r; i < x + r; i++)
            {
                for (int j = y - r; j < y + r; j++)
                {
                    SquareTileFrame(i, j, true, lh);
                }
            }
        }
        /// <summary>
        /// A helper method to run WorldGen.SquareTileFrame() over an area.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="xr">The number of blocks in the X direction.</param>
        /// <param name="yr">The number of blocks in the Y direction.</param>
        /// <param name="lh">Whether or not to use Large Herb logic.</param>
        public static void SquareTileFrameArea(int x, int y, int xr, int yr, bool lh = false)
        {
            for (int i = x; i < x + xr; i++)
            {
                for (int j = y; j < y + yr; j++)
                {
                    SquareTileFrame(i, j, true, lh);
                }
            }
        }
    }
}
