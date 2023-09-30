using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures
{
    internal class LavaOcean
    {
        public static void Generate(int x, int y)
        {
            int xRad = WorldGen.genRand.Next(50, 60);
            int yRad = WorldGen.genRand.Next(20, 35);
            int thickness = WorldGen.genRand.Next(5, 11);

            if (WorldGen.genRand.NextBool(3))
            {
                xRad += WorldGen.genRand.Next(5, 12);
            }

            MakeOval(x, y, xRad, yRad, TileID.Stone);

            MakeOval(x, y, xRad - thickness, yRad - thickness, ushort.MaxValue);
        }

        public static bool GenerateLavaOcean(int X, int Y)
        {
            ushort tileStone = (ushort)ModContent.TileType<Tiles.BlastedStone>();

            int num = 0;
            double num12 = 0.6;
            double num18 = 1.3;
            double num19 = 0.3;
            if (num == 0)
            {
                num12 = 0.55;
                num18 = 2.0;
            }
            num12 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
            num18 *= 1.05 - WorldGen.genRand.NextDouble() * 0.1;
            num19 *= 1.0 - WorldGen.genRand.NextDouble() * 0.1;
            int biomeWidth = WorldGen.genRand.Next(105, 125);
            int num21 = (int)(biomeWidth * num19);
            int num22 = (int)(biomeWidth * num12);
            int num23 = WorldGen.genRand.Next(9, 13);
            int num24 = X - biomeWidth + (biomeWidth / 3);
            int num2 = X + biomeWidth - (biomeWidth / 3);
            int num3 = Y - biomeWidth;
            int num4 = Y + biomeWidth;
            for (int i = num3; i <= num4; i++)
            {
                for (int j = num24; j <= num2; j++)
                {
                    if (!WorldGen.InWorld(j, i, 50))
                    {
                        return false;
                    }
                    if (Main.tile[j, i].TileType == 203 || Main.tile[j, i].TileType == 25)
                    {
                        return false;
                    }
                }
            }
            int num5 = Y;
            if (WorldGen.genRand.Next(4) == 0)
            {
                num5 = Y - WorldGen.genRand.Next(2);
            }
            int num6 = Y - num23;
            if (WorldGen.genRand.Next(4) == 0)
            {
                num6 = Y - num23 - WorldGen.genRand.Next(2);
            }
            for (int k = num3; k <= num4; k++)
            {
                for (int l = num24; l <= num2; l++)
                {
                    Tile t0 = Main.tile[l, k];
                    t0.LiquidAmount = 0;
                    if (WorldGen.genRand.Next(4) == 0)
                    {
                        num5 = Y - WorldGen.genRand.Next(2);
                    }
                    if (WorldGen.genRand.Next(4) == 0)
                    {
                        num6 = Y - num23 + WorldGen.genRand.Next(2);
                    }
                    int num7 = ((k <= Y) ? ((int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow(Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))) : ((int)Math.Sqrt(Math.Pow((double)Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2) + Math.Pow((double)Math.Abs(k - Y) * 1.8 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.2))));
                    if (num7 < biomeWidth)
                    {
                        Tile t = Main.tile[l, k];
                        t.Slope = SlopeType.Solid;
                        t.IsHalfBlock = false;
                        t.TileType = tileStone;
                        if (l > num24 + 5 + WorldGen.genRand.Next(2) && l < num2 - 5 - WorldGen.genRand.Next(2))
                        {
                            t.HasTile = true;
                        }
                        if ((double)num7 < (double)biomeWidth * 0.9)
                        {
                            t.WallType = WallID.ObsidianBackUnsafe;
                        }
                        WorldGen.SquareTileFrame(l, k);
                    }
                    num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3) + Math.Pow((double)Math.Abs(k - Y) * num18 * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 2.3));
                    if (k > num6 && k < num5)
                    {
                        Tile t = Main.tile[l, k];
                        t.HasTile = false;
                    }
                    if (k < num5 && num7 < (int)(num22 * (1.0 + WorldGen.genRand.NextDouble() * 0.02)))
                    {
                        Tile t = Main.tile[l, k];
                        t.HasTile = false;
                    }
                    num7 = (int)Math.Sqrt(Math.Pow(Math.Abs(l - X) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85) + Math.Pow((Math.Abs(k - Y) * 2) * (1.0 + WorldGen.genRand.NextDouble() * 0.02), 1.85));
                    if (k < Y - 1 || num7 >= (int)(num21 * (1.0 + WorldGen.genRand.NextDouble() * 0.025)))
                    {
                        continue;
                    }
                    if (k <= Y + 2 || num7 != num21 - 1 || WorldGen.genRand.Next(2) != 0)
                    {
                        Tile t = Main.tile[l, k];
                        t.HasTile = false;
                    }
                    if (k >= Y)
                    {
                        Tile t = Main.tile[l, k];
                        if (k == Y)
                        {
                            t.LiquidAmount = 127;
                        }
                        else
                        {
                            t.LiquidAmount = byte.MaxValue;
                        }
                        t.LiquidType = LiquidID.Lava;
                    }
                }
            }
            if (num == 0)
            {
                num24 = (int)(X - biomeWidth * num19) - WorldGen.genRand.Next(-15, 1) - 5;
                num2 = (int)(X + biomeWidth * num19) + WorldGen.genRand.Next(0, 16);
                int m = num24;
                int num8 = 0;
                for (; m < num2; m += WorldGen.genRand.Next(9, 14))
                {
                    int num9 = Y - 3;
                    while (!Main.tile[m, num9].HasTile)
                    {
                        num9--;
                    }
                    num9 -= 4;
                    int num10 = WorldGen.genRand.Next(5, 10);
                    int num11 = WorldGen.genRand.Next(5, 10);
                    int n = m - num10;
                    while (num10 > 0)
                    {
                        for (n = m - num10; n < m + num10; n++)
                        {
                            Tile t = Main.tile[n, num9];
                            t.HasTile = true;
                            t.TileType = tileStone;
                        }
                        num8++;
                        if (WorldGen.genRand.Next(3) < num8)
                        {
                            num8 = 0;
                            num10--;
                            m += WorldGen.genRand.Next(-1, 2);
                        }
                        if (num11 <= 0)
                        {
                            num10--;
                        }
                        num11--;
                        num9++;
                    }
                    n -= WorldGen.genRand.Next(1, 3);
                    Tile t1 = Main.tile[n, num9 - 2];
                    Tile t2 = Main.tile[n, num9 - 1];
                    Tile t3 = Main.tile[n, num9];
                    t1.HasTile = true;
                    t1.TileType = tileStone;
                    t2.HasTile = true;
                    t2.TileType = tileStone;
                    t3.HasTile = true;
                    t3.TileType = tileStone;
                    WorldGen.SquareTileFrame(n, num9 - 2);
                    WorldGen.SquareTileFrame(n, num9 - 1);
                    WorldGen.SquareTileFrame(n, num9);
                    if (WorldGen.genRand.NextBool(2))
                    {
                        Tile t4 = Main.tile[n, num9 + 1];
                        t4.HasTile = true;
                        t4.TileType = tileStone;
                        WorldGen.SquareTileFrame(n, num9 + 1);
                        Utils.PlaceCustomTight(n, num9 + 2, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
                    }
                    else
                    {
                        Utils.PlaceCustomTight(n, num9 + 1, (ushort)ModContent.TileType<Tiles.BlastedStalac>());
                    }
                }
            }
            return true;
        }

        public static void MakeStalac(int x, int y, int length, int width, ushort centerType, ushort borderType, int direction = 1)
        {
            // Store the x and y in new vars
            int startX = x;
            int startY = y;

            // Define variables to determine how many tiles to travel in one direction before changing direction
            int howManyTimes = 0;
            int maxTimes = WorldGen.genRand.Next(4) + 2;
            int modifier = 1;

            // Change direction (left/right) of the spike before generating
            if (WorldGen.genRand.NextBool())
            {
                modifier *= -1;
            }

            // Initial assignment of last position
            Vector2 lastPos = new(x, y);

            // Loop until length
            for (int q = 1; q <= length; q++)
            {
                // Grab the distance between the start and the current position
                float distFromStart = Vector2.Distance(new Vector2(startX, startY), new Vector2(x, y));

                // If the distance is divisible by 4 and the width is less than 5, reduce the width
                //int w = width;
                //if ((int)distFromStart % 4 == 0 || w < 5)
                //{
                //    width--;
                //}

                // Put a circle between the last point and the current
                int betweenXPos = (int)(lastPos.X + x) / 2;
                int betweenYPos = (int)(lastPos.Y + y) / 2;
                Utils.MakeCircle2(betweenXPos, betweenYPos, (int)((length - q)) * 2, borderType, borderType);

                // Make a square/circle of the tile
                Utils.MakeCircle2(x, y, (int)((length - q)) * 2, borderType, borderType);

                // Assign the last position to the current position
                lastPos = new(x, y);

                // Make the spike go in the opposite direction after a certain amount of tiles
                howManyTimes++;
                if (howManyTimes % maxTimes == 0)
                {
                    modifier *= -1;
                    howManyTimes = 0;
                    maxTimes = WorldGen.genRand.Next(4) + 2;
                }

                // Add to the x to make the spike turn/curve in a direction
                x += (WorldGen.genRand.Next(2) + 1) * modifier; // * (maxTimes / (WorldGen.genRand.Next(2) + 1));

                // Add a bit to the y coord to make it go up or down depending on the parameter
                //if (w > 3)
                //{
                //    y += (WorldGen.genRand.Next(3) + 2) * direction;
                //}
                //else
                {
                    y += 1 * direction;
                }
            }
        }


        public static void MakeOval(int x, int y, int xRadius, int yRadius, int type)
        {
            int xmin = x - xRadius;
            int ymin = y - yRadius;
            int xmax = x + xRadius;
            int ymax = y + yRadius;
            for (int i = xmin; i < xmax + 1; i++)
            {
                for (int j = ymin; j < ymax + 1; j++)
                {
                    if (Utils.IsInsideEllipse(i, j, new Vector2(x, y), xRadius, yRadius))
                    {
                        if (type == 65535)
                        {
                            Tile t = Main.tile[i, j];
                            t.HasTile = false;
                            WorldGen.SquareTileFrame(i, j);
                            t.WallType = WallID.ObsidianBackUnsafe;
                            if (j > y + 3)
                            {
                                t.LiquidAmount = 255;
                                t.LiquidType = LiquidID.Lava;
                            }
                            if (j < y - yRadius / 3)
                            {
                                if (i % 7 == 0)
                                    MakeStalac(i, j, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(3, 5), TileID.Stone, TileID.Stone, 1);
                            }
                        }
                        else
                        {
                            Tile t = Main.tile[i, j];
                            t.HasTile = true;
                            t.TileType = (ushort)type;
                            //if (Utils.IsInsideEllipse(i, j, new Vector2(x, y), xRadius - 1, yRadius - 1))
                            //{
                            //    t.WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                            //}

                            WorldGen.SquareTileFrame(i, j);
                        }
                    }
                }
            }
        }
    }
}
