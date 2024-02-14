using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures;

public class Nest
{
    public static void ClearHexagon(int centerX, int centerY, int hexagonSize)
    {
        int halfHexagonSize = hexagonSize / 2;

        for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
        {
            int y1 = Math.Abs(x) / 2;
            int y2 = hexagonSize - Math.Abs(x) / 2;

            for (int y = y1; y < y2; y++)
            {
                int tileX = centerX + x;
                int tileY = centerY + y;

                if (WorldGen.InWorld(tileX, tileY))
                {
                    Tile t = Main.tile[tileX, tileY];
                    t.HasTile = false;
                    t.WallType = (ushort)ModContent.WallType<Walls.NestWall>();
                    t.LiquidAmount = 0;
                    WorldGen.SquareWallFrame(tileX, tileY);
                    if (WorldGen.genRand.NextBool(15))
                    {
                        t.LiquidAmount = 100;
                        t.LiquidType = LiquidID.Honey;
                        WorldGen.SquareTileFrame(tileX, tileY);
                    }
                }
            }
        }
    }
    public static Vector2 MakeCell(int x, int y)
    {
        CreateHexagon(x, y, 26, ModContent.TileType<Tiles.Tropics.Nest>());
        CreateHexagon(x, y, 23, 0, true);
        ClearHexagon(x, y + 3, 20);
        return new Vector2(x, y);
    }
    public static void CreateHexagon(int centerX, int centerY, int hexagonSize, int tileType, bool doWalls = false)
    {
        int halfHexagonSize = hexagonSize / 2;

        for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
        {
            int y1 = Math.Abs(x) / 2;
            int y2 = hexagonSize - Math.Abs(x) / 2;

            for (int y = y1; y < y2; y++)
            {
                int tileX = centerX + x;
                int tileY = centerY + y;

                if (WorldGen.InWorld(tileX, tileY))
                {
                    Tile t = Main.tile[tileX, tileY];
                    if (!doWalls)
                    {
                        t.HasTile = true;
                        t.TileType = (ushort)tileType;
                        WorldGen.SquareTileFrame(tileX, tileY);
                    }
                    else
                    {
                        t.WallType = (ushort)ModContent.WallType<Walls.NestWall>();
                        WorldGen.SquareWallFrame(tileX, tileY);
                    }
                }
            }
        }
    }
    public static void CreateWaspNest(int x, int y)
    {
        bool placedLarva = false;
        MakeCell(x, y);
        // figure out how to make the larva gen in a random cell
        if (WorldGen.genRand.NextBool(8))
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                Main.tile[i, y + 18].TileType = TileID.Hive;
                Main.tile[i, y + 18].Active(true);
            }
            WorldGen.PlaceTile(x, y + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
            placedLarva = true;
        }


        int rightSide = WorldGen.genRand.Next(2);
        int leftSide = WorldGen.genRand.Next(2);

        List<Vector2> centers = new List<Vector2>();
        if (rightSide == 0)
        {
            Vector2 rSideM = MakeCell(x + 24, y);

            // possible larva placement
            if (WorldGen.genRand.NextBool(6) && !placedLarva)
            {
                for (int i = x + 23; i <= x + 25; i++)
                {
                    Main.tile[i, y + 18].TileType = TileID.Hive;
                    Main.tile[i, y + 18].Active(true);
                }
                WorldGen.PlaceTile(x + 24, y + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
                placedLarva = true;
            }

            int xOffR = (int)(rSideM.X + 24 * (float)Math.Cos(2 * 60 * Math.PI / 180f));
            int yOffR = (int)(rSideM.Y + 24 * (float)Math.Sin(2 * 60 * Math.PI / 180f));
            centers.Add(new Vector2(x, y + 12));
            centers.Add(rSideM + new Vector2(0, 12));
            centers.Add(rSideM + new Vector2(0, 12));
            Vector2 cell = MakeCell(xOffR, yOffR);

            // possible larva placement
            if (WorldGen.genRand.NextBool(5) && !placedLarva)
            {
                for (int i = xOffR - 1; i <= xOffR + 1; i++)
                {
                    Main.tile[i, yOffR + 18].TileType = TileID.Hive;
                    Main.tile[i, yOffR + 18].Active(true);
                }
                WorldGen.PlaceTile(xOffR, yOffR + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
                placedLarva = true;
            }

            int xoffcell = (int)(cell.X + 24 * (float)Math.Cos(3 * 60 * Math.PI / 180f));
            int yoffcell = (int)(cell.Y + 24 * (float)Math.Sin(3 * 60 * Math.PI / 180f));
            centers.Add(cell + new Vector2(0, 12));

            if (WorldGen.genRand.NextBool(2))
            {
                MakeCell(xoffcell, yoffcell);
                centers.Add(cell + new Vector2(0, 12));
                centers.Add(new Vector2(xoffcell, yoffcell + 12));
            }
        }

        if (leftSide == 0)
        {
            Vector2 lSideM = MakeCell(x - 24, y);

            // possible larva placement
            if (WorldGen.genRand.NextBool(6) && !placedLarva)
            {
                for (int i = x - 25; i <= x - 23; i++)
                {
                    Main.tile[i, y + 18].TileType = TileID.Hive;
                    Main.tile[i, y + 18].Active(true);
                }
                WorldGen.PlaceTile(x - 24, y + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
                placedLarva = true;
            }

            int xOffL = (int)(lSideM.X + 24 * (float)Math.Cos(1 * 60 * Math.PI / 180f));
            int yOffL = (int)(lSideM.Y + 24 * (float)Math.Sin(1 * 60 * Math.PI / 180f));
            centers.Add(new Vector2(x, y + 12));
            centers.Add(lSideM + new Vector2(0, 12));
            centers.Add(lSideM + new Vector2(0, 12));

            Vector2 cell = MakeCell(xOffL, yOffL);

            // possible larva placement
            if (WorldGen.genRand.NextBool(5) && !placedLarva)
            {
                for (int i = xOffL - 1; i <= xOffL + 1; i++)
                {
                    Main.tile[i, yOffL + 18].TileType = TileID.Hive;
                    Main.tile[i, yOffL + 18].Active(true);
                }
                WorldGen.PlaceTile(xOffL, yOffL + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
                placedLarva = true;
            }

            int xoffcell = (int)(cell.X + 24 * (float)Math.Cos(3 * 60 * Math.PI / 180f));
            int yoffcell = (int)(cell.Y + 24 * (float)Math.Sin(3 * 60 * Math.PI / 180f));
            centers.Add(cell + new Vector2(0, 12));

            if (WorldGen.genRand.NextBool(2))
            {
                MakeCell(xoffcell, yoffcell);
                centers.Add(cell + new Vector2(0, 12));
                centers.Add(new Vector2(xoffcell, yoffcell + 12));
            }
        }

        //Main.tile[x, y].TileType = (ushort)ModContent.TileType<Tiles.Tropics.Nest>();
        //Main.tile[x - 1, y].TileType = (ushort)ModContent.TileType<Tiles.Tropics.Nest>();
        //Main.tile[x + 1, y].TileType = (ushort)ModContent.TileType<Tiles.Tropics.Nest>();
        //Tile t = Main.tile[x, y];
        //t.HasTile = true;
        //Tile t2 = Main.tile[x - 1, y];
        //t2.HasTile = true;
        //Tile t3 = Main.tile[x + 1, y];
        //t3.HasTile = true;
        //WorldGen.PlaceTile(x, y - 1, (ushort)ModContent.TileType<Tiles.WaspLarva>());


        int xoff1 = (int)(x + 24 * (float)Math.Cos(1 * 60 * Math.PI / 180f));
        int yoff1 = (int)(y + 24 * (float)Math.Sin(1 * 60 * Math.PI / 180f));
        MakeCell(xoff1, yoff1);

        // possible larva placement
        if (WorldGen.genRand.NextBool(5) && !placedLarva)
        {
            for (int i = xoff1 - 1; i <= xoff1 + 1; i++)
            {
                Main.tile[i, yoff1 + 18].TileType = TileID.Hive;
                Main.tile[i, yoff1 + 18].Active(true);
            }
            WorldGen.PlaceTile(xoff1, yoff1 + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
            placedLarva = true;
        }

        centers.Add(new Vector2(xoff1, yoff1 + 12));

        int xoff2 = (int)(x + 24 * (float)Math.Cos(2 * 60 * Math.PI / 180f));
        int yoff2 = (int)(y + 24 * (float)Math.Sin(2 * 60 * Math.PI / 180f));
        MakeCell(xoff2, yoff2);

        // possible larva placement
        if (WorldGen.genRand.NextBool(4) && !placedLarva)
        {
            for (int i = xoff2 - 1; i <= xoff2 + 1; i++)
            {
                Main.tile[i, yoff2 + 18].TileType = TileID.Hive;
                Main.tile[i, yoff2 + 18].Active(true);
            }
            WorldGen.PlaceTile(xoff2, yoff2 + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
            placedLarva = true;
        }

        centers.Add(new Vector2(xoff2, yoff2 + 12));

        int xoff3 = (int)(x + 23 * (float)Math.Cos(4 * 60 * Math.PI / 180f));
        int yoff3 = (int)(y + 23 * (float)Math.Sin(4 * 60 * Math.PI / 180f));
        Vector2 cellthing = MakeCell(xoff3, yoff3);

        // final larva placement
        if (!placedLarva)
        {
            for (int i = xoff3 - 1; i <= xoff3 + 1; i++)
            {
                Main.tile[i, yoff3 + 18].TileType = TileID.Hive;
                Main.tile[i, yoff3 + 18].Active(true);
            }
            WorldGen.PlaceTile(xoff3, yoff3 + 17, (ushort)ModContent.TileType<Tiles.Tropics.WaspLarva>());
            placedLarva = true;
        }

        if (WorldGen.genRand.NextBool(3))
        {
            //int xoff4 = (int)(cellthing.X + 15 * (float)Math.Cos(5 * 60 * Math.PI / 180f));
            //int yoff4 = (int)(cellthing.Y + 15 * (float)Math.Sin(5 * 60 * Math.PI / 180f));
            MakeCell(xoff2 - 24, yoff2);
        }

        if (centers.Count > 0)
        {
            for (int i = 0; i < centers.Count; i += 2)
            {
                if (WorldGen.genRand.NextBool(2))
                {
                    BoreTunnelOriginal((int)centers[i].X, (int)centers[i].Y, (int)centers[i + 1].X, (int)centers[i + 1].Y, 3, TileID.Hive);
                }
            }
        }
        for (int i = x - 50; i < x + 37; i++)
        {
            for (int j = y - 21; j < y + 45; j++)
            {
                if (Main.tile[i, j].TileType == TileID.Hive)
                {
                    Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.Tropics.Nest>();
                }
            }
        }
    }
    public static List<Vector2> BoreTunnelOriginal(int x0, int y0, int x1, int y1, float r, ushort type)
    {
        List<Vector2> result = new List<Vector2>();
        bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (flag)
        {
            Utils.Swap(ref x0, ref y0);
            Utils.Swap(ref x1, ref y1);
        }
        if (x0 > x1)
        {
            Utils.Swap(ref x0, ref x1);
            Utils.Swap(ref y0, ref y1);
        }
        int num = x1 - x0;
        int num2 = Math.Abs(y1 - y0);
        int num3 = num / 2;
        int num4 = (y0 < y1) ? 1 : -1;
        int num5 = y0;
        for (int i = x0; i <= x1; i++)
        {
            if (flag)
            {
                MakeCircleNest(num5, i, (int)r, type, (ushort)ModContent.WallType<Walls.NestWall>());
                result.Add(new Vector2(num5, i));
            }
            else
            {
                MakeCircleNest(i, num5, (int)r, type, (ushort)ModContent.WallType<Walls.NestWall>());
                result.Add(new Vector2(i, num5));
            }
            num3 -= num2;
            if (num3 < 0)
            {
                num5 += num4;
                num3 += num;
            }
        }
        return result;
    }
    public static void MakeCircleNest(int x, int y, int r, ushort tileType, ushort wallType = 0)
    {
        for (int k = x - r; k <= x + r; k++)
        {
            for (int l = y - r; l <= y + r; l++)
            {
                if (Vector2.Distance(new Vector2(k, l), new Vector2(x, y)) < r)
                {
                    Tile t = Framing.GetTileSafely(k, l);
                    if (t.TileType != ModContent.TileType<Tiles.Tropics.WaspLarva>() && t.TileType != TileID.Hive)
                    {
                        t.HasTile = false;
                        t.IsHalfBlock = false;
                        t.Slope = SlopeType.Solid;
                        WorldGen.SquareTileFrame(k, l);
                    }
                    t.WallType = wallType;
                    WorldGen.SquareWallFrame(k, l);
                }
                if (Vector2.Distance(new Vector2(k, l), new Vector2(x, y)) < r - 1)
                {
                    Tile t = Framing.GetTileSafely(k, l);
                    t.WallType = wallType;
                    WorldGen.SquareWallFrame(k, l);
                }
            }
        }
    }




    // old code
    public static List<Vector2> MakeCell(Vector2 start, int sideLength, bool clockwise = true)
    {
        List<Vector2> cells = new List<Vector2>();
        // add the center point to the cells list
        cells.Add(start + new Vector2(clockwise ? 14 : -14, -5));
        float endX = start.X;
        float endY = start.Y - sideLength;
        BoreTunnelOriginal((int)start.X, (int)start.Y, (int)endX, (int)endY, 2, TileID.Hive);

        // going clockwise
        if (clockwise)
        {
            for (int angleCounter = -30; angleCounter <= 210; angleCounter += 60)
            {
                cells.Add(start);
                start = new Vector2(endX, endY);

                float angle = (float)(Math.PI / 180) * angleCounter;
                int sideAdd = angleCounter == 90 ? 0 : 4;
                endX = (float)(start.X + (sideLength + sideAdd) * Math.Cos(angle));
                endY = (float)(start.Y + (sideLength + sideAdd) * Math.Sin(angle));
                BoreTunnelOriginal((int)start.X, (int)start.Y, (int)endX, (int)endY, 2, TileID.Hive);
            }
        }
        // going counterclockwise
        else
        {
            for (int angleCounter = 210; angleCounter >= -90; angleCounter -= 60)
            {
                cells.Add(start);
                start = new Vector2(endX, endY);
                float angle = (float)(Math.PI / 180) * angleCounter;
                int sideAdd = (angleCounter == 90 || angleCounter == -90) ? 0 : 4;
                endX = (float)(start.X + (sideLength + sideAdd) * Math.Cos(angle));
                endY = (float)(start.Y + (sideLength + sideAdd) * Math.Sin(angle));
                BoreTunnelOriginal((int)start.X, (int)start.Y, (int)endX, (int)endY, 2, TileID.Hive);
            }
        }
        return cells;
    }
    public static void CreateNest(int x, int y)
    {
        bool direction = WorldGen.genRand.NextBool();
        int index = 3;
        int index2 = 5;
        if (WorldGen.genRand.NextBool())
        {
            index = 5;
            index2 = 3;
        }
        List<Vector2> returnedPoints = MakeCell(new Vector2(x, y), 12, direction);
        List<Vector2> returnedPoints2 = MakeCell(returnedPoints[index], 12, !direction);
        List<Vector2> returnedPoints3 = MakeCell(returnedPoints2[index2], 12, direction);
        List<Vector2> returnedPoints4 = MakeCell(returnedPoints[index2], 12, !direction);
        List<Vector2> returnedPoints5 = MakeCell(returnedPoints3[index], 12, direction);
        List<Vector2> returnedPoints6 = MakeCell(returnedPoints[index2], 12, direction);
        List<Vector2> returnedPoints7 = MakeCell(returnedPoints4[index], 12, direction);

        ClearHexagon((int)returnedPoints[0].X, (int)returnedPoints[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints2[0].X, (int)returnedPoints2[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints3[0].X, (int)returnedPoints3[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints4[0].X, (int)returnedPoints4[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints5[0].X, (int)returnedPoints5[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints6[0].X, (int)returnedPoints6[0].Y - 12, 24);
        ClearHexagon((int)returnedPoints7[0].X, (int)returnedPoints7[0].Y - 12, 24);

        switch (WorldGen.genRand.Next(7))
        {
            case 0:
                for (int i = (int)returnedPoints[0].X - 1; i <= (int)returnedPoints[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints[0].Y + 5, TileID.Hive);
                }
                break;
            case 1:
                for (int i = (int)returnedPoints2[0].X - 1; i <= (int)returnedPoints2[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints2[0].Y + 5, TileID.Hive);
                }
                break;
            case 2:
                for (int i = (int)returnedPoints3[0].X - 1; i <= (int)returnedPoints3[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints3[0].Y + 5, TileID.Hive);
                }
                break;
            case 3:
                for (int i = (int)returnedPoints4[0].X - 1; i <= (int)returnedPoints4[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints4[0].Y + 5, TileID.Hive);
                }
                break;
            case 4:
                for (int i = (int)returnedPoints5[0].X - 1; i <= (int)returnedPoints5[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints5[0].Y + 5, TileID.Hive);
                }
                break;
            case 5:
                for (int i = (int)returnedPoints6[0].X - 1; i <= (int)returnedPoints6[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints6[0].Y + 5, TileID.Hive);
                }
                break;
            case 6:
                for (int i = (int)returnedPoints7[0].X - 1; i <= (int)returnedPoints7[0].X + 1; i++)
                {
                    WorldGen.PlaceTile(i, (int)returnedPoints7[0].Y + 5, TileID.Hive);
                }
                break;
        }

    }
}
