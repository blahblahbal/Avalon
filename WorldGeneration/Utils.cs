using Avalon.Tiles.Contagion;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class Utils
{
    public static void ResetSlope(int i, int j)
    {
        Tile t = Main.tile[i, j];
        t.Slope = SlopeType.Solid;
        t.IsHalfBlock = false;
    }

    /// <summary>
    /// A helper method to find the actual surface of the world.
    /// </summary>
    /// <param name="positionX">The x position.</param>
    /// <returns>The surface of the world.</returns>
    public static int TileCheck(int positionX)
    {
        for (int i = (int)(GenVars.worldSurfaceLow - 30); i < Main.maxTilesY; i++)
        {
            Tile tile = Framing.GetTileSafely(positionX, i);
            if ((tile.TileType == TileID.Dirt || tile.TileType == TileID.ClayBlock || tile.TileType == TileID.Stone ||
                tile.TileType == TileID.Sand || tile.TileType == ModContent.TileType<Snotsand>()
                /*|| tile.TileType == ModContent.TileType<Loam>() */|| tile.TileType == TileID.Mud ||
                tile.TileType == TileID.SnowBlock || tile.TileType == TileID.IceBlock) && tile.HasTile)
            {
                return i;
            }
        }
        return 0;
    }

    public static int CaesiumTileCheck(int posX, int posY, int modifier = 1)
    {
        if (modifier == -1)
        {
            int q = posY;
            for (int i = posY - 30; i < posY; i++) // ypos = maxTilesY - 170 (165)
            {
                Tile tile = Framing.GetTileSafely(posX, q);
                Tile tileAbove = Framing.GetTileSafely(posX, q - 1);
                if (!tile.HasTile)
                {
                    q++;
                }
                else if (!tileAbove.HasTile && tile.HasTile)
                {
                    return q;
                }
            }
        }
        else if (modifier == 1)
        {
            int q = posY;
            for (int i = posY; i < posY + 30; i++)
            {
                Tile tile = Framing.GetTileSafely(posX, q);
                Tile tileBelow = Framing.GetTileSafely(posX, q + 1);
                if (!tile.HasTile) // (tile.TileType == ModContent.TileType<Tiles.BlastedStone>() || tile.TileType == ModContent.TileType<Tiles.LaziteGrass>())
                {
                    q--;
                }
                else if (!tileBelow.HasTile && tile.HasTile)
                {
                    return q;
                }
            }
        }
        return posY;
    }

    /// <summary>
    /// Swaps two values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs">Left hand side.</param>
    /// <param name="rhs">Right hand side.</param>
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T t = lhs;
        lhs = rhs;
        rhs = t;
    }
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
    public static void MakeCircle2(int x, int y, int radius, int outerType, int innerType)
    {
        for (int k = x - radius; k <= x + radius; k++)
        {
            for (int l = y - radius; l <= y + radius; l++)
            {
                if (Vector2.Distance(new Vector2(k, l), new Vector2(x, y)) < radius && radius > 1)
                {
                    if (Main.tile[k, l].TileType != innerType)
                    {
                        Tile t = Framing.GetTileSafely(k, l);
                        t.HasTile = true;
                        t.IsHalfBlock = false;
                        t.Slope = SlopeType.Solid;
                        Main.tile[k, l].TileType = (ushort)outerType;
                        WorldGen.SquareTileFrame(k, l);
                    }    
                }
            }
        }
        for (int k = x - radius; k <= x + radius; k++)
        {
            for (int l = y - radius; l <= y + radius; l++)
            {
                if (Vector2.Distance(new Vector2(k, l), new Vector2(x, y)) < radius - 3 && radius - 3 > 1)
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = true;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    Main.tile[k, l].TileType = (ushort)innerType;
                    WorldGen.SquareTileFrame(k, l);
                }
            }
        }
    }
    public static void MakeSquare(int x, int y, int s, int type)
    {
        for (int i = x - s / 2; i < x + s / 2; i++)
        {
            for (int j = y; j < y + s; j++)
            {
                if (Main.tile[i, j].TileType != TileID.WoodBlock)
                {
                    Tile t = Main.tile[i, j];
                    t.HasTile = true;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    Main.tile[i, j].TileType = (ushort)type;
                    WorldGen.SquareTileFrame(i, j);
                }
            }
        }
    }
    public static void MakeCircle(int x, int y, int radius, int tileType, bool walls = false, int wallType = WallID.Dirt)
    {
        for (int k = x - (int)(radius * 0.25); k <= x + (int)(radius * 0.25); k++)
        {
            for (int l = y - radius; l <= y + radius; l++)
            {
                float dist = Vector2.Distance(new Vector2(k, l), new Vector2(x, y));
                if (dist <= radius && dist >= (radius - 29))
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = false;
                }
                if ((dist <= radius && dist >= radius - 7) || (dist <= radius - 22 && dist >= radius - 29))
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = false;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    Main.tile[k, l].TileType = (ushort)tileType;
                    WorldGen.SquareTileFrame(k, l);
                }
                if (walls)
                {
                    if (dist <= radius - 6 && dist >= radius - 23)
                    {
                        Main.tile[k, l].WallType = (ushort)wallType;
                    }
                }
            }
        }
    }

    public static void OreRunner(int i, int j, double strength, int steps, ushort type, ushort typeThatCanBeReplaced)
    {
        double num = strength;
        double num2 = steps;
        Vector2 vector2D = default;
        vector2D.X = i;
        vector2D.Y = j;
        Vector2 vector2D2 = default;
        vector2D2.X = WorldGen.genRand.Next(-10, 11) * 0.1f;
        vector2D2.Y = WorldGen.genRand.Next(-10, 11) * 0.1f;
        while (num > 0.0 && num2 > 0.0)
        {
            if (vector2D.Y < 0.0 && num2 > 0.0 && type == 59)
            {
                num2 = 0.0;
            }
            num = strength * (num2 / (double)steps);
            num2 -= 1.0;
            int num3 = (int)(vector2D.X - num * 0.5);
            int num4 = (int)(vector2D.X + num * 0.5);
            int num5 = (int)(vector2D.Y - num * 0.5);
            int num6 = (int)(vector2D.Y + num * 0.5);
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesX)
            {
                num4 = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesY)
            {
                num6 = Main.maxTilesY;
            }
            for (int k = num3; k < num4; k++)
            {
                for (int l = num5; l < num6; l++)
                {
                    if (Math.Abs((double)k - vector2D.X) + Math.Abs((double)l - vector2D.Y) < strength * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015) &&
                        Main.tile[k, l].HasTile && Main.tile[k, l].TileType == typeThatCanBeReplaced)
                    {
                        Main.tile[k, l].TileType = type;
                        Main.tile[k, l].ClearBlockPaintAndCoating();
                        SquareTileFrame(k, l);
                        if (Main.netMode == 2)
                        {
                            NetMessage.SendTileSquare(-1, k, l);
                        }
                    }
                }
            }
            vector2D += vector2D2;
            vector2D2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
            if (vector2D2.X > 1.0f)
            {
                vector2D2.X = 1.0f;
            }
            if (vector2D2.X < -1.0f)
            {
                vector2D2.X = -1.0f;
            }
        }
    }
    #region hellcastle helper methods
    public static bool HasEnoughRoomForPaintingType(int x, int y, int width, int height, int r = 1)
    {
        for (int i = x - r; i < x + width + r; i++)
        {
            for (int j = y - r; j < y + height + r; j++)
            {
                if (Main.tile[i, j].HasTile)
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Helper method to find if there is a tile in range.
    /// </summary>
    /// <param name="x">X coordinate in tiles.</param>
    /// <param name="y">Y coordinate in tiles.</param>
    /// <param name="radius">The radius from the coordinate in which to check.</param>
    /// <param name="tileType">The tile type of the tile.</param>
    /// <returns>True if not found, false if found.</returns>
    public static bool TileNotInRange(int x, int y, int radius, ushort tileType)
    {
        int xMin = x - radius;
        int xMax = x + radius;
        int yMin = y - radius;
        int yMax = y + radius;

        for (int i = xMin; i < xMax; i++)
        {
            for (int j = yMin; j < yMax; j++)
            {
                if (Main.tile[i, j].TileType == tileType)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// A helper method to check if there are any painting tiles in a specific radius
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate</param>
    /// <param name="radius">The radius from the X and Y coordinates.</param>
    /// <returns>True if not found, false otherwise.</returns>
    public static bool NoPaintingsInRange(int x, int y, int radius)
    {
        int xMin = x - radius;
        int xMax = x + radius;
        int yMin = y - radius;
        int yMax = y + radius;

        for (int i = xMin; i < xMax; i++)
        {
            for (int j = yMin; j < yMax; j++)
            {
                if (Main.tile[i, j].TileType == TileID.Painting2X3 || Main.tile[i, j].TileType == TileID.Painting3X3 ||
                    Main.tile[i, j].TileType == TileID.Painting3X2 || Main.tile[i, j].TileType == TileID.Painting6X4 ||
                    Main.tile[i, j].TileType == ModContent.TileType<Tiles.Paintings2x3>() ||
                    Main.tile[i, j].TileType == ModContent.TileType<Tiles.Paintings3x2>() ||
                    Main.tile[i, j].TileType == ModContent.TileType<Tiles.Paintings3x3>() ||
                    Main.tile[i, j].TileType == ModContent.TileType<Tiles.Paintings>())
                {
                    return false;
                }
            }
        }
        return true;
    }
    public static bool IsThereRoomForChandelier(int x, int y)
    {
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y; j < y + 3; j++)
            {
                if (Main.tile[i, j].HasTile)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool IsValidPlacementForPaintingInHellcastle(int x, int y, int width, int height)
    {
        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                if (Main.tile[i, j].WallType == ModContent.WallType<Walls.ImperviousBrickWallBrownUnsafe>() ||
                    Main.tile[i, j].WallType == ModContent.WallType<Walls.ImperviousBrickWallEctoUnsafe>() ||
                    Main.tile[i, j].WallType == ModContent.WallType<Walls.ImperviousBrickWallWhiteUnsafe>())
                {
                    return false;
                }
            }
        }
        return true;
    }
    #endregion
}
