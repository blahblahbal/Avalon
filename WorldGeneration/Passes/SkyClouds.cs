using Avalon.World.Structures;
using Terraria;
using Terraria.ID;
using Terraria.IO;
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
            int y = 41;
            if (Main.maxTilesY == 1800)
            {
                y = 51;
            }
            if (Main.maxTilesY == 2400)
            {
                y = 61;
            }
            if (Main.rand.NextBool(2))
            {
                x = Main.maxTilesX - (Main.maxTilesX / 3);
            }


            Utils.GetSkyFortressXCoord(x, y, 209, 158, ref x);
            SkyFortress.Generate(x, y);

            int xcoord;
            int ycoord;
            int Amount_Of_Spawns3 = 200 + Main.maxTilesY / 10;
            for (int amount = 0; amount < Amount_Of_Spawns3; amount++)
            {
                xcoord = WorldGen.genRand.Next(30, Main.maxTilesX - 30);
                if (xcoord > x - 230 && xcoord < x + 230) continue;
                ycoord = WorldGen.genRand.Next(50, 150);
                MakeCloud(xcoord, ycoord);
            }
            //int ypos = (int)((float)(Main.maxTilesY / 1200) * 2 + 1);

            

            
        }

        #region clouds
        public static void MakeCloud(int x, int y)
        {

            GrowWall(x, y, WallID.Cloud, 4);
            GrowFragile(x, y, TileID.Cloud, 3);
            GrowWall(x, y - 3, WallID.Cloud, 8);
            GrowFragile(x, y - 2, TileID.Cloud, 7);
            GrowWall(x + 2, y, WallID.Cloud, 4);
            GrowFragile(x + 1, y, TileID.Cloud, 3);
            GrowWall(x + 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x - 2, y, WallID.Cloud, 4);
            GrowFragile(x - 1, y, TileID.Cloud, 3);
            GrowWall(x - 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x + 4, y, WallID.Cloud, 3);
            GrowFragile(x + 3, y, TileID.Cloud, 2);
            GrowWall(x - 4, y, WallID.Cloud, 3);
            GrowFragile(x - 3, y, TileID.Cloud, 2);

            /* GrowWall(x,y,WallID.Cloud, 4);
            GrowWall(x,y-3,WallID.Cloud, 8);
            GrowWall(x+2,y,WallID.Cloud, 4);
            GrowWall(x+1,y,WallID.Cloud, 5);
            GrowWall(x-2,y,WallID.Cloud, 4);
            GrowWall(x-1,y,WallID.Cloud, 5);
            GrowWall(x+4,y,WallID.Cloud, 3);
            GrowWall(x-4,y,WallID.Cloud, 3); */


            x += 6;
            y -= 6;

            GrowWall(x, y, WallID.Cloud, 4);
            GrowFragile(x, y, TileID.Cloud, 3);
            GrowWall(x, y - 3, WallID.Cloud, 8);
            GrowFragile(x, y - 2, TileID.Cloud, 7);
            GrowWall(x + 2, y, WallID.Cloud, 4);
            GrowFragile(x + 1, y, TileID.Cloud, 3);
            GrowWall(x + 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x - 2, y, WallID.Cloud, 4);
            GrowFragile(x - 1, y, TileID.Cloud, 3);
            GrowWall(x - 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x + 4, y, WallID.Cloud, 3);
            GrowFragile(x + 3, y, TileID.Cloud, 2);
            GrowWall(x - 4, y, WallID.Cloud, 3);
            GrowFragile(x - 3, y, TileID.Cloud, 2);

            /* GrowWall(x,y,WallID.Cloud, 4);
            GrowWall(x,y-3,WallID.Cloud, 8);
            GrowWall(x+2,y,WallID.Cloud, 4);
            GrowWall(x+1,y,WallID.Cloud, 5);
            GrowWall(x-2,y,WallID.Cloud, 4);
            GrowWall(x-1,y,WallID.Cloud, 5);
            GrowWall(x+4,y,WallID.Cloud, 3);
            GrowWall(x-4,y,WallID.Cloud, 3); */

            y += 6;
            x += 6;

            GrowWall(x, y, WallID.Cloud, 4);
            GrowFragile(x, y, TileID.Cloud, 3);
            GrowWall(x, y - 3, WallID.Cloud, 8);
            GrowFragile(x, y - 2, TileID.Cloud, 7);
            GrowWall(x + 2, y, WallID.Cloud, 4);
            GrowFragile(x + 1, y, TileID.Cloud, 3);
            GrowWall(x + 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x - 2, y, WallID.Cloud, 4);
            GrowFragile(x - 1, y, TileID.Cloud, 3);
            GrowWall(x - 1, y, WallID.Cloud, 5);
            GrowFragile(x, y, TileID.Cloud, 4);
            GrowWall(x + 4, y, WallID.Cloud, 3);
            GrowFragile(x + 3, y, TileID.Cloud, 2);
            GrowWall(x - 4, y, WallID.Cloud, 3);
            GrowFragile(x - 3, y, TileID.Cloud, 2);
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
        public static void GrowFragile(int x, int y, ushort type, int rounds)
        {
            int growth = Main.rand.Next(256);

            for (int i = 0; i < 9; i++)
            {
                int j = i % 3;
                int tgro = 1 << i;
                if ((tgro & growth) == tgro)
                {
                    int tx = (x + j - 1);
                    int ty = (int)(y + (i / 3) - 1);

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
                    int ty = (int)(y + (i / 3) - 1);

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
