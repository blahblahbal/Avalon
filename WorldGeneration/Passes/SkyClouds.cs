using Avalon.World.Structures;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes
{
    internal class SkyClouds : GenPass
    {
        public SkyClouds() : base("Avalon Sky", 20f)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            int x = Main.maxTilesX / 2 - 100;
            int y = GetYOffset(Main.maxTilesY);
            //if (Main.maxTilesY == 1800)
            //{
            //    y = 51;
            //}
            //if (Main.maxTilesY == 2400)
            //{
            //    y = 61;
            //}
            if (Main.rand.NextBool(2))
            {
                x = Main.maxTilesX - (Main.maxTilesX / 3);
            }


            Utils.GetSkyFortressXCoord(x, y, 209, 158, ref x);
            SkyFortress.Generate(x, y);

            int xcoord;
            int ycoord;
            int Amount_Of_Spawns3 = 10 + Main.maxTilesY / 10;
            for (int amount = 0; amount < Amount_Of_Spawns3; amount++)
            {
                xcoord = WorldGen.genRand.Next(90, Main.maxTilesX - 90);
                if (xcoord > x - 220 && xcoord < x + 379) continue;
                ycoord = WorldGen.genRand.Next(100, 200);
                MakeCloud(xcoord, ycoord);
            }
            //int ypos = (int)((float)(Main.maxTilesY / 1200) * 2 + 1);
        }

        public static bool CheckTilesInRange(int x, int y, int radius)
        {
            bool flag = true;
            for (int i = x - radius; i < x + radius; i++)
            {
                for (int j = y - radius; j < y + radius; j++)
                {
                    if (Main.tile[i, j].HasTile &&
                        (Main.tile[i, j].TileType == TileID.Dirt || Main.tile[i, j].TileType == TileID.Grass ||
                        Main.tile[i, j].TileType == TileID.Trees || Main.tile[i, j].TileType == TileID.Sunplate ||
                        Main.tile[i, j].TileType == ModContent.TileType<Tiles.MoonplateBlock>() ||
                        Main.tile[i, j].TileType == ModContent.TileType<Tiles.TwiliplateBlock>()))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        public static int GetYOffset(int maxTilesY)
        {
            // Calculate the Y offset based on world size
            int smallWorldMaxY = 4200;
            int mediumWorldMaxY = 6400;
            int largeWorldMaxY = 8400;

            int smallWorldOffset = 41;
            int mediumWorldOffset = 51;
            int largeWorldOffset = 61;

            if (maxTilesY <= smallWorldMaxY)
                return smallWorldOffset;
            else if (maxTilesY >= largeWorldMaxY)
                return largeWorldOffset;
            else
            {
                float percentage = (float)(maxTilesY - smallWorldMaxY) / (largeWorldMaxY - smallWorldMaxY);
                int yOffset = (int)MathHelper.Lerp(smallWorldOffset, largeWorldOffset, percentage);
                return yOffset;
            }
        }

        #region clouds
        public static void MakeCloud(int x, int y)
        {
            int tileType = (x > 1500 && x < Main.maxTilesX - 1500) ? TileID.Cloud : TileID.RainCloud;
            int maxWidth = WorldGen.genRand.Next(4, 15);
            if (Main.rand.NextBool(10))
            {
                maxWidth = WorldGen.genRand.Next(20, 25);
            }
            float widthMultiply = WorldGen.genRand.NextFloat(1, 3);
            for (float height = 0; height < maxWidth; height += WorldGen.genRand.NextFloat(0.5f, 1f))
            {
                float startingWidth = maxWidth - height * widthMultiply;
                for (float width = -startingWidth; width < startingWidth; width += 0.5f)
                {
                    int TILEFUCKABLE = tileType;
                    //if (height > maxWidth / 3) TILEFUCKABLE = TileID.Dirt;
                    Utils.MakeCircle2(x + (int)(width * height) + WorldGen.genRand.Next(-7, 7), y - (int)(height * 2) + WorldGen.genRand.Next(-4, 4), WorldGen.genRand.Next(4, 7), TILEFUCKABLE, TILEFUCKABLE);
                }
            }

            #region classic
            //if (!CheckTilesInRange(x, y, 8))
            //{
            //    return;
            //}
            //GrowWall(x, y, WallID.Cloud, 4);
            //GrowFragile(x, y, TileID.Cloud, 3);
            //GrowWall(x, y - 3, WallID.Cloud, 8);
            //GrowFragile(x, y - 2, TileID.Cloud, 7);
            //GrowWall(x + 2, y, WallID.Cloud, 4);
            //GrowFragile(x + 1, y, TileID.Cloud, 3);
            //GrowWall(x + 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x - 2, y, WallID.Cloud, 4);
            //GrowFragile(x - 1, y, TileID.Cloud, 3);
            //GrowWall(x - 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x + 4, y, WallID.Cloud, 3);
            //GrowFragile(x + 3, y, TileID.Cloud, 2);
            //GrowWall(x - 4, y, WallID.Cloud, 3);
            //GrowFragile(x - 3, y, TileID.Cloud, 2);

            ///* GrowWall(x,y,WallID.Cloud, 4);
            //GrowWall(x,y-3,WallID.Cloud, 8);
            //GrowWall(x+2,y,WallID.Cloud, 4);
            //GrowWall(x+1,y,WallID.Cloud, 5);
            //GrowWall(x-2,y,WallID.Cloud, 4);
            //GrowWall(x-1,y,WallID.Cloud, 5);
            //GrowWall(x+4,y,WallID.Cloud, 3);
            //GrowWall(x-4,y,WallID.Cloud, 3); */


            //x += 6;
            //y -= 6;

            //GrowWall(x, y, WallID.Cloud, 4);
            //GrowFragile(x, y, TileID.Cloud, 3);
            //GrowWall(x, y - 3, WallID.Cloud, 8);
            //GrowFragile(x, y - 2, TileID.Cloud, 7);
            //GrowWall(x + 2, y, WallID.Cloud, 4);
            //GrowFragile(x + 1, y, TileID.Cloud, 3);
            //GrowWall(x + 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x - 2, y, WallID.Cloud, 4);
            //GrowFragile(x - 1, y, TileID.Cloud, 3);
            //GrowWall(x - 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x + 4, y, WallID.Cloud, 3);
            //GrowFragile(x + 3, y, TileID.Cloud, 2);
            //GrowWall(x - 4, y, WallID.Cloud, 3);
            //GrowFragile(x - 3, y, TileID.Cloud, 2);

            ///* GrowWall(x,y,WallID.Cloud, 4);
            //GrowWall(x,y-3,WallID.Cloud, 8);
            //GrowWall(x+2,y,WallID.Cloud, 4);
            //GrowWall(x+1,y,WallID.Cloud, 5);
            //GrowWall(x-2,y,WallID.Cloud, 4);
            //GrowWall(x-1,y,WallID.Cloud, 5);
            //GrowWall(x+4,y,WallID.Cloud, 3);
            //GrowWall(x-4,y,WallID.Cloud, 3); */

            //y += 6;
            //x += 6;

            //GrowWall(x, y, WallID.Cloud, 4);
            //GrowFragile(x, y, TileID.Cloud, 3);
            //GrowWall(x, y - 3, WallID.Cloud, 8);
            //GrowFragile(x, y - 2, TileID.Cloud, 7);
            //GrowWall(x + 2, y, WallID.Cloud, 4);
            //GrowFragile(x + 1, y, TileID.Cloud, 3);
            //GrowWall(x + 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x - 2, y, WallID.Cloud, 4);
            //GrowFragile(x - 1, y, TileID.Cloud, 3);
            //GrowWall(x - 1, y, WallID.Cloud, 5);
            //GrowFragile(x, y, TileID.Cloud, 4);
            //GrowWall(x + 4, y, WallID.Cloud, 3);
            //GrowFragile(x + 3, y, TileID.Cloud, 2);
            //GrowWall(x - 4, y, WallID.Cloud, 3);
            //GrowFragile(x - 3, y, TileID.Cloud, 2);
            #endregion classic
        }

        public static void CloudPlatform()
        {
            int i = Main.rand.Next(30, Main.maxTilesX - 30);
            int j = Main.rand.Next(100, 150);

            if (Main.tile[i, j].WallType == WallID.Cloud)
            {
                GrowFragile(i, j, TileID.Cloud, 5);
                GrowFragile(i + 2, j, TileID.Cloud, 5);
            }
        }

        public static void GenerateParabola(int x, int y, int width, int height)
        {
            int startX = x; // Adjust the starting X-coordinate of the parabola
            int startY = y; // Adjust the starting Y-coordinate of the parabola
            int parabolaWidth = width; // Adjust the width of the parabola

            for (int q = 0; q < parabolaWidth; q++)
            {
                int z = (int)(startY - (0.1f * Math.Pow(q - startX, 1.5))); // Adjust the coefficient to control the shape of the parabola

                // Place the tile
                //Utils.MakeCircle(startX + q, z, 5, TileID.Cloud, false, WallID.Cloud);
                WorldGen.PlaceTile(startX + q, z, TileID.Cloud); // Change the tile ID to the desired tile

                // Place additional tiles to create the parabolic shape
                for (int i = 1; i <= 3; i++)
                {
                    //Utils.MakeCircle(startX + q, z + i, 5, TileID.Cloud, false, WallID.Cloud);
                    WorldGen.PlaceTile(startX + q, z + i, TileID.Cloud); // Change the tile ID to the desired tile
                }
            }

            //int width = Main.maxTilesX;
            //int height = Main.maxTilesY / 2; // Adjust the height of the parabola

            //int xCenter = width / 2;
            //int yCenter = y + height / 2;

            //for (int q = x; q < x + width; q++)
            //{
            //    int z = (int)(yCenter + (0.1f * Math.Pow(q - xCenter, 2))); // Adjust the coefficient to control the shape of the parabola

            //    // Place the tile
            //    Utils.MakeCircle(q, z, 5, TileID.Cloud, false, WallID.Cloud);

            //    //WorldGen.PlaceTile(q, z, TileID.Dirt); // Change the tile ID to the desired tile

            //    // Place additional tiles to create the parabolic shape
            //    for (int i = 1; i <= 3; i++)
            //    {
            //        Utils.MakeCircle(q, z + i, 5, TileID.Cloud, false, WallID.Cloud);
            //        //WorldGen.PlaceTile(q, z + i, TileID.Dirt); // Change the tile ID to the desired tile
            //    }
            //}
        }

        public static void GrowCloudIsland(int x, int y, int width, int height)
        {
            for (int i = x + 4; i < x + width - 4; i++)
            {
                Utils.MakeCircleNormal(i, y + height / 2, height / 2, TileID.Dirt);
            }

            GenerateParabola(x, y, width, height);
        }

        public static void GrowFragile(int x, int y, ushort type, int rounds)
        {
            int growth = WorldGen.genRand.Next(256);

            for (int i = 0; i < 9; i++)
            {
                int j = i % 3;
                int tgro = 1 << i;
                if ((tgro & growth) == tgro)
                {
                    int tx = (x + j - 1);
                    int ty = y + (i / 3) - 1;

                    if (!Main.tile[tx, ty].HasTile)
                    {
                        Tile t = Main.tile[tx, ty];
                        t.HasTile = true;
                        t.TileType = type;
                        WorldGen.SquareTileFrame(tx, ty);
                        if (rounds > 0)
                        {
                            GrowFragile(tx, ty, type, rounds - 1);
                        }
                    }
                }
            }
            WorldGen.SquareTileFrame(x, y);
        }
        public static void GrowWall(int x, int y, ushort type, int rounds)
        {
            int growth = Main.rand.Next(256);

            for (int i = 0; i < 9; i++)
            {
                int j = i % 3;
                int tgro = 1 << i;
                if ((tgro & growth) == tgro)
                {
                    int tx = (x + j - 1);
                    int ty = y + (i / 3) - 1;

                    if (Main.tile[tx, ty].WallType == 0)
                    {

                        Main.tile[tx, ty].WallType = type;
                        if (rounds > 0)
                        {
                            GrowWall(tx, ty, type, rounds - 1);
                        }
                    }
                }
            }
            WorldGen.SquareWallFrame(x, y);
        }
        #endregion
    }
}
