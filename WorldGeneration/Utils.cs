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
    public static void PlaceContagionTight(int x, int y)
    {
        if (Main.tile[x, y].LiquidType != LiquidID.Shimmer)
        {
            PlaceUncheckedStalactite(x, y, WorldGen.genRand.NextBool(2), WorldGen.genRand.Next(3));
            if (Main.tile[x, y].TileType == ModContent.TileType<Tiles.Contagion.ContagionStalactgmites>())
            {
                WorldGen.CheckTight(x, y);
            }
        }
    }
    public static void PlaceUncheckedStalactite(int x, int y, bool preferSmall, int variation)
    {
        ushort type = 165;
        variation = Terraria.Utils.Clamp(variation, 0, 2);
        if (WorldGen.SolidTile(x, y - 1) && !Main.tile[x, y].HasTile && !Main.tile[x, y + 1].HasTile)
        {
            if (Main.tile[x, y - 1].TileType == ModContent.TileType<Chunkstone>())
            {
                if (preferSmall)
                {
                    int num12 = variation * 18;
                    Tile t = Main.tile[x, y];
                    t.TileType = type;
                    t.HasTile = true;
                    t.TileFrameX = (short)num12;
                    t.TileFrameY = 72;
                }
                else
                {
                    int num15 = variation * 18;
                    Tile t = Main.tile[x, y];
                    t.HasTile = true;
                    t.TileFrameX = (short)num15;
                    t.TileFrameY = 0;
                    t = Main.tile[x, y + 1];
                    t.HasTile = true;
                    t.TileFrameX = (short)num15;
                    t.TileFrameY = 18;
                }
            }
        }
        else
        {
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Chunkstone>())
            {
                if (preferSmall)
                {
                    int num5 = 54 + variation * 18;
                    Tile t = Main.tile[x, y];
                    t.TileType = type;
                    t.HasTile = true;
                    t.TileFrameX = (short)num5;
                    t.TileFrameY = 90;
                }
                else
                {
                    int num6 = 54 + variation * 18;
                    Tile t = Main.tile[x, y - 1];
                    t.HasTile = true;
                    t.TileFrameX = (short)num6;
                    t.TileFrameY = 36;
                    t = Main.tile[x, y];
                    t.HasTile = true;
                    t.TileFrameX = (short)num6;
                    t.TileFrameY = 54;
                }
            }
        }
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
                return i - 3;
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
    public static void GetCMXCoord(int x, int y, int xLength, int ylength, ref int xCoord)
    {
        bool leftSideActive = false;
        bool rightSideActive = false;
        for (int i = y; i < y + ylength; i++)
        {
            if (Main.tile[x, i].HasTile && (Main.tile[x, i].TileType == TileID.LihzahrdBrick ||
                Main.tile[x, i].TileType == ModContent.TileType<Tiles.ImperviousBrick>() || Main.tileDungeon[Main.tile[x, i].TileType]) ||
                Main.tile[x, i].WallType == WallID.LihzahrdBrickUnsafe ||
                Main.tile[x, i].WallType == ModContent.WallType<Walls.ImperviousBrickWallUnsafe>() ||
                Main.wallDungeon[Main.tile[x, i].WallType])
            {
                leftSideActive = true;
                break;
            }
        }
        for (int i = y; i < y + ylength; i++)
        {
            if (Main.tile[x + xLength, i].HasTile && (Main.tile[x + xLength, i].TileType == TileID.LihzahrdBrick ||
                Main.tile[x + xLength, i].TileType == ModContent.TileType<Tiles.ImperviousBrick>() || Main.tileDungeon[Main.tile[x + xLength, i].TileType]) ||
                Main.tile[x + xLength, i].WallType == WallID.LihzahrdBrickUnsafe ||
                Main.tile[x + xLength, i].WallType == ModContent.WallType<Walls.ImperviousBrickWallUnsafe>() ||
                Main.wallDungeon[Main.tile[x + xLength, i].WallType])
            {
                rightSideActive = true;
                break;
            }
        }
        if (leftSideActive || rightSideActive)
        {
            if (xCoord > Main.maxTilesX / 2) xCoord--;
            else xCoord++;
            if (xCoord < 100)
            {
                xCoord = 100;
                return;
            }
            if (xCoord > Main.maxTilesX - 100)
            {
                xCoord = Main.maxTilesX - 100;
                return;
            }
            GetCMXCoord(xCoord, y, xLength, ylength, ref xCoord);
        }
    }
    public static void TileRunnerCrystalMines(int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool overRide = true, int ignoreTileType = -1)
    {
        if (WorldGen.drunkWorldGen)
        {
            strength *= (double)(1f + WorldGen.genRand.Next(-80, 81) * 0.01f);
            steps = (int)(steps * (1f + WorldGen.genRand.Next(-80, 81) * 0.01f));
        }
        if (WorldGen.getGoodWorldGen && type != 57)
        {
            strength *= (double)(1f + WorldGen.genRand.Next(-80, 81) * 0.015f);
            steps += WorldGen.genRand.Next(3);
        }
        double num = strength;
        float num2 = steps;
        Vector2 vector = default;
        vector.X = i;
        vector.Y = j;
        Vector2 vector2 = default;
        vector2.X = WorldGen.genRand.Next(-10, 11) * 0.1f;
        vector2.Y = WorldGen.genRand.Next(-10, 11) * 0.1f;
        if (speedX != 0f || speedY != 0f)
        {
            vector2.X = speedX;
            vector2.Y = speedY;
        }
        bool flag = type == 368;
        bool flag2 = type == 367;
        bool lava = false;
        if (WorldGen.getGoodWorldGen && WorldGen.genRand.NextBool(4))
        {
            lava = true;
        }
        while (num > 0.0 && num2 > 0f)
        {
            if (WorldGen.drunkWorldGen && WorldGen.genRand.NextBool(30))
            {
                vector.X += WorldGen.genRand.Next(-100, 101) * 0.05f;
                vector.Y += WorldGen.genRand.Next(-100, 101) * 0.05f;
            }
            if (vector.Y < 0f && num2 > 0f && type == 59)
            {
                num2 = 0f;
            }
            num = strength * (double)(num2 / steps);
            num2--;
            int num3 = (int)(vector.X - num * 0.5);
            int num4 = (int)(vector.X + num * 0.5);
            int num5 = (int)(vector.Y - num * 0.5);
            int num6 = (int)(vector.Y + num * 0.5);
            if (num3 < 1)
            {
                num3 = 1;
            }
            if (num4 > Main.maxTilesX - 1)
            {
                num4 = Main.maxTilesX - 1;
            }
            if (num5 < 1)
            {
                num5 = 1;
            }
            if (num6 > Main.maxTilesY - 1)
            {
                num6 = Main.maxTilesY - 1;
            }
            for (int k = num3; k < num4; k++)
            {
                if (k < WorldGen.beachDistance + 50 || k >= Main.maxTilesX - WorldGen.beachDistance - 50)
                {
                    lava = false;
                }
                for (int l = num5; l < num6; l++)
                {
                    if ((WorldGen.drunkWorldGen && l < Main.maxTilesY - 300 && type == 57) || (ignoreTileType >= 0 && Main.tile[k, l].HasTile && Main.tile[k, l].TileType == ignoreTileType) || !((double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015)))
                    {
                        continue;
                    }
                    //if (Main.tileFrameImportant[Main.tile[k, l - 1].TileType] && Main.tile[k, l].HasTile ||
                    //    Main.tileFrameImportant[Main.tile[k, l].TileType] && Main.tile[k, l + 1].HasTile)
                    //{
                    //    continue;
                    //}
                    if (type < 0)
                    {
                        if (Main.tile[k, l].TileType == 53)
                        {
                            continue;
                        }
                        if (type == -2 && Main.tile[k, l].HasTile && (l < GenVars.waterLine || l > GenVars.lavaLine))
                        {
                            Main.tile[k, l].LiquidAmount = byte.MaxValue;
                            Tile t = Main.tile[k, l];
                            if (lava)
                            {
                                t.LiquidType = LiquidID.Lava;
                            }
                            if (l > GenVars.lavaLine)
                            {
                                t.LiquidType = LiquidID.Lava;
                            }
                        }
                        Tile t2 = Main.tile[k, l];
                        t2.HasTile = false;
                        continue;
                    }
                    if (flag && (double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.3 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.01))
                    {
                        WorldGen.PlaceWall(k, l, 180, mute: true);
                    }
                    if (flag2 && (double)(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y)) < strength * 0.3 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.01))
                    {
                        WorldGen.PlaceWall(k, l, 178, mute: true);
                    }

                    if (overRide || !Main.tile[k, l].HasTile)
                    {
                        Tile tile = Main.tile[k, l];
                        bool flag3 = Main.tileStone[type] && tile.TileType != 1;
                        bool flag4 = Main.tileStone[type] && tile.TileType != 1;
                        if (!TileID.Sets.CanBeClearedDuringGeneration[tile.TileType])
                        {
                            flag3 = true;
                        }
                        if (tile.TileType is TileID.Granite or TileID.Marble or TileID.Sandstone or TileID.HardenedSand) // remove hard sand and sandstone later, make this not gen in the UG desert
                            flag3 = false;
                        switch (tile.TileType)
                        {
                            case TileID.Sand:
                                if (type == 59 && GenVars.UndergroundDesertLocation.Contains(k, l))
                                {
                                    flag3 = true;
                                }
                                if (type == 40)
                                {
                                    flag3 = true;
                                }
                                if (l < Main.worldSurface && type != 59)
                                {
                                    flag3 = true;
                                }
                                break;
                            case TileID.GoldBrick:
                            case TileID.Cloud:
                            case TileID.MushroomBlock:
                            case TileID.RainCloud:
                            case TileID.SnowCloud:
                            case TileID.Containers:
                            case TileID.Containers2:
                            case TileID.Cobweb:
                                flag3 = true;
                                break;
                            case 1:
                                if (type == 59 && l < Main.worldSurface + WorldGen.genRand.Next(-50, 50))
                                {
                                    flag3 = true;
                                }
                                break;
                        }
                        if (Main.tileDungeon[tile.TileType] || Main.wallDungeon[tile.WallType] || TileID.Sets.IsVine[tile.TileType] ||
                            TileID.Sets.IsATreeTrunk[tile.TileType] || TileID.Sets.CountsAsGemTree[tile.TileType] ||
                            tile.TileType == TileID.LihzahrdBrick || Main.tileFrameImportant[tile.TileType] ||
                            tile.TileType == TileID.WoodenSpikes || tile.TileType == TileID.Statues || tile.TileType == TileID.Spikes)
                        {
                            flag3 = true;
                        }
                        if (!flag3)
                        {
                            tile.TileType = (ushort)type;
                            tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                            WorldGen.SquareTileFrame(k, l);
                            WorldGen.SquareWallFrame(k, l);
                        }
                        if ((tile.TileType == TileID.Cobweb || Main.tileFrameImportant[tile.TileType]) && !Main.wallDungeon[tile.WallType] &&
                            tile.WallType != WallID.LihzahrdBrickUnsafe)
                        {
                            tile.WallType = (ushort)ModContent.WallType<Walls.CrystalStoneWall>();
                            WorldGen.SquareWallFrame(k, l);
                        }
                    }
                    if (addTile)
                    {
                        Tile t = Main.tile[k, l];
                        t.HasTile = true;
                        t.LiquidAmount = 0;
                        t.LiquidType = LiquidID.Water;
                    }
                    if (noYChange && l < Main.worldSurface && type != 59)
                    {
                        Main.tile[k, l].WallType = 2;
                    }
                    if (type == 59 && l > GenVars.waterLine && Main.tile[k, l].LiquidAmount > 0)
                    {
                        Tile t = Main.tile[k, l];
                        t.LiquidType = LiquidID.Water;
                        t.LiquidAmount = 0;
                    }
                }
            }
            vector += vector2;
            if ((!WorldGen.drunkWorldGen || WorldGen.genRand.Next(3) != 0) && num > 50.0)
            {
                vector += vector2;
                num2 -= 1f;
                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (num > 100.0)
                {
                    vector += vector2;
                    num2 -= 1f;
                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if (num > 150.0)
                    {
                        vector += vector2;
                        num2 -= 1f;
                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (num > 200.0)
                        {
                            vector += vector2;
                            num2 -= 1f;
                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            if (num > 250.0)
                            {
                                vector += vector2;
                                num2 -= 1f;
                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                if (num > 300.0)
                                {
                                    vector += vector2;
                                    num2 -= 1f;
                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    if (num > 400.0)
                                    {
                                        vector += vector2;
                                        num2 -= 1f;
                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        if (num > 500.0)
                                        {
                                            vector += vector2;
                                            num2 -= 1f;
                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            if (num > 600.0)
                                            {
                                                vector += vector2;
                                                num2 -= 1f;
                                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                if (num > 700.0)
                                                {
                                                    vector += vector2;
                                                    num2 -= 1f;
                                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    if (num > 800.0)
                                                    {
                                                        vector += vector2;
                                                        num2 -= 1f;
                                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        if (num > 900.0)
                                                        {
                                                            vector += vector2;
                                                            num2 -= 1f;
                                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
            if (WorldGen.drunkWorldGen)
            {
                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.25f;
            }
            if (vector2.X > 1f)
            {
                vector2.X = 1f;
            }
            if (vector2.X < -1f)
            {
                vector2.X = -1f;
            }
            if (!noYChange)
            {
                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (vector2.Y > 1f)
                {
                    vector2.Y = 1f;
                }
                if (vector2.Y < -1f)
                {
                    vector2.Y = -1f;
                }
            }
            else if (type != 59 && num < 3.0)
            {
                if (vector2.Y > 1f)
                {
                    vector2.Y = 1f;
                }
                if (vector2.Y < -1f)
                {
                    vector2.Y = -1f;
                }
            }
            if (type == 59 && !noYChange)
            {
                if (vector2.Y > 0.5)
                {
                    vector2.Y = 0.5f;
                }
                if (vector2.Y < -0.5)
                {
                    vector2.Y = -0.5f;
                }
                if (vector.Y < Main.rockLayer + 100.0)
                {
                    vector2.Y = 1f;
                }
                if (vector.Y > Main.maxTilesY - 300)
                {
                    vector2.Y = -1f;
                }
            }
        }
    }
}
