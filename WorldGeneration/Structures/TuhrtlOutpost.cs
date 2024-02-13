using Avalon.Tiles.Tropics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures;

internal class TuhrtlOutpost
{

    public static void Outpost(int x, int y)
    {
        // height of the vertical box to generate
        int heightOfBox = 0;
        int wallHeightOfBox = 0;

        ushort brick = (ushort)ModContent.TileType<TuhrtlBrick>();
        ushort brickWall = (ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>();

        #region tiles
        int wide = 90;
        int high = 60;

        // add high to height of box, offsetting the diagonal part by high
        heightOfBox += high;

        // set the pyramid step width to 90
        int pstep = 90;
        int yStartSlope = y + high;
        heightOfBox += high;

        for (int pyY = yStartSlope; pyY <= yStartSlope + high + 4; pyY += 2)
        {
            for (int pyX = x - pstep + 1; pyX <= x + pstep + 1; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = brick;
                Main.tile[pyX, pyY - 1].Active(true);
                Main.tile[pyX, pyY - 1].TileType = brick;
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep++;
        }

        int yStartInnerSlope = yStartSlope + high + 4;
        heightOfBox += high + 4;

        for (int pyY = yStartInnerSlope; pyY <= yStartInnerSlope + high + 4; pyY += 2)
        {
            for (int pyX = x - pstep + 2; pyX <= x + pstep; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = brick;
                Main.tile[pyX, pyY - 1].Active(true);
                Main.tile[pyX, pyY - 1].TileType = brick;
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep--;
        }

        // make the main box
        for (int i = x - wide + 1; i < x + wide + 1; i++)
        {
            for (int j = y; j < y + heightOfBox + high; j++)
            {
                Main.tile[i, j].Active(true);
                Main.tile[i, j].TileType = brick;
                Utils.SquareTileFrame(i, j, resetSlope: true);
            }
        }
        #endregion

        #region walls
        int wallWidth = wide - 2;
        int wallHeight = high - 2;

        wallHeightOfBox += wallHeight;

        // set the pyramid step width to 90
        int pstepWall = 90;
        int yStartSlopeWall = y + high;
        wallHeightOfBox += high;

        for (int pyY = yStartSlopeWall; pyY <= yStartSlopeWall + high + 4; pyY += 2)
        {
            for (int pyX = x - pstepWall + 2; pyX <= x + pstepWall; pyX++)
            {
                Main.tile[pyX, pyY].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY);
                Main.tile[pyX, pyY - 1].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY - 1);
            }
            pstepWall++;
        }

        int yStartInnerSlopeWall = yStartSlopeWall + high + 4;
        wallHeightOfBox += high + 4;

        for (int pyY = yStartInnerSlopeWall; pyY <= yStartInnerSlopeWall + high + 4; pyY += 2)
        {
            for (int pyX = x - pstepWall + 3; pyX <= x + pstepWall - 1; pyX++)
            {
                Main.tile[pyX, pyY].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY);
                Main.tile[pyX, pyY - 1].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY - 1);
            }
            pstepWall--;
        }

        // make the main box walls
        for (int i = x - wallWidth; i < x + wallWidth; i++)
        {
            for (int j = y + 1; j < y + wallHeightOfBox + high - 1; j++)
            {
                Main.tile[i, j].WallType = brickWall;
                WorldGen.SquareWallFrame(i, j);
            }
        }

        #endregion

        int xStartTunnel = x + (WorldGen.genRand.NextBool(2) ? wide : -wide);
        int yTunnel = y + high / 2;
        int heightOfStartTunnel = WorldGen.genRand.Next(5, 7);

        Vector2 tunnelEndpoint = new Vector2(x, yTunnel);

        //WorldGeneration.Structures.Hellcastle.MakeTunnel()
    }
}
