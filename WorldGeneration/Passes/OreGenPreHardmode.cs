using Avalon.Common;
using Avalon.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes; // Write original code challenged FAILED

public class OreGenPreHardmode : GenPass
{
    public OreGenPreHardmode(string name, float loadWeight) : base(name, loadWeight)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int tourmaline = 0; tourmaline < (int)(Main.maxTilesX * 0.08); tourmaline++)
        {
            int xc = WorldGen.genRand.Next(0, Main.maxTilesX);
            int yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            while (Main.tile[xc, yc].TileType != TileID.Stone)
            {
                xc = WorldGen.genRand.Next(0, Main.maxTilesX);
                yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            }
            WorldGen.TileRunner(xc, yc, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), ModContent.TileType<Tourmaline>());
        }
        for (int peridot = 0; peridot < (int)(Main.maxTilesX * 0.08); peridot++)
        {
            int xc = WorldGen.genRand.Next(0, Main.maxTilesX);
            int yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            while (Main.tile[xc, yc].TileType != TileID.Stone)
            {
                xc = WorldGen.genRand.Next(0, Main.maxTilesX);
                yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            }
            WorldGen.TileRunner(xc, yc, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), ModContent.TileType<Peridot>());
        }
        for (int zircon = 0; zircon < (int)(Main.maxTilesX * 0.08); zircon++)
        {
            int xc = WorldGen.genRand.Next(0, Main.maxTilesX);
            int yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            while (Main.tile[xc, yc].TileType != TileID.Stone)
            {
                xc = WorldGen.genRand.Next(0, Main.maxTilesX);
                yc = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
            }
            WorldGen.TileRunner(xc, yc, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), ModContent.TileType<Zircon>());
        }
        int roiOre = ModContent.TileType<RhodiumOre>();
        if (AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Osmium)
        {
            roiOre = ModContent.TileType<OsmiumOre>();
        }
        if (AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Iridium)
        {
            roiOre = ModContent.TileType<IridiumOre>();
        }
        for (int roi = 0; roi < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); roi++)
        {
            WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY), WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(4, 7), roiOre);
        }

        #region motherloads

        if (WorldGen.genRand.NextBool(3))
        {
            // heartstone
            int i6 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double stuff;
            stuff = Main.rockLayer - 75f;
            int j6 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            WorldGen.OreRunner(i6, j6, WorldGen.genRand.Next(20, 29), WorldGen.genRand.Next(20, 29),
                (ushort)ModContent.TileType<Heartstone>());
        }

        for (int asdfasdf = 0; asdfasdf < 2; asdfasdf++)
        {
            // copper
            int i6 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double stuff;
            stuff = Main.rockLayer - 75f;
            int j6 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            WorldGen.OreRunner(i6, j6, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(23, 33),
                (ushort)WorldGen.SavedOreTiers.Copper);

            // iron
            int i3 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int j3 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            WorldGen.OreRunner(i3, j3, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(23, 33),
                (ushort)WorldGen.SavedOreTiers.Iron);

            // silver
            int i4 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int j4 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            WorldGen.OreRunner(i4, j4, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(23, 33),
                (ushort)WorldGen.SavedOreTiers.Silver);

            // gold
            int i5 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int j5 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            WorldGen.OreRunner(i5, j5, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(23, 33),
                (ushort)WorldGen.SavedOreTiers.Gold);
            // rhodium/osmium
            //int i7 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            //int j7 = WorldGen.genRand.Next((int)stuff, Main.maxTilesY - 200);
            //WorldGen.OreRunner(i7, j7, (double)WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(23, 33), (ushort)ExxoAvalonOriginsWorld.rhodiumOre.GetTile());
        }

        #endregion motherloads

        #region boltstone, heartstone, and starstone
        for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 2E-05); i++)
        {
            int i8 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double rockLayer = Main.rockLayer;
            int j8 = WorldGen.genRand.Next((int)rockLayer, Main.maxTilesY - 150);
            GenerateHearts(i8, j8, ModContent.TileType<Heartstone>());
        }

        for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 1E-05); i++)
        {
            int i8 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double rockLayer = Main.rockLayer;
            int j8 = WorldGen.genRand.Next((int)rockLayer, Main.maxTilesY - 150);
            GenerateStars(i8, j8, (ushort)ModContent.TileType<Starstone>());
        }

        for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 7E-06); i++)
        {
            int i8 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            double rockLayer = Main.rockLayer + 50;
            int j8 = WorldGen.genRand.Next((int)rockLayer, Main.maxTilesY - 150);
            GenerateBolts(i8, j8, (ushort)ModContent.TileType<Boltstone>());
        }
        #endregion boltstone, heartstone, and starstone
    }

    public static void GenerateStars(int x, int y, ushort type)
    {
        int size = WorldGen.genRand.Next(2);
        if (WorldGen.genRand.Next(20) == 0)
        {
            size = 2;
        }

        if (size == 0)
        {
            //Main.tile[x + 3, y].active(true);
            WorldGen.PlaceTile(x + 3, y, type, forced: true);
            for (int i = x + 2; i <= x + 4; i++)
            {
                //Main.tile[i, y + 1].active(true);
                WorldGen.PlaceTile(i, y + 1, type, forced: true);
            }

            for (int i = x; i <= x + 6; i++)
            {
                //Main.tile[i, y + 2].active(true);
                WorldGen.PlaceTile(i, y + 2, type, forced: true);
            }

            for (int i = x + 1; i <= x + 5; i++)
            {
                //Main.tile[i, y + 3].active(true);
                WorldGen.PlaceTile(i, y + 3, type, forced: true);
            }

            for (int i = x + 2; i <= x + 4; i++)
            {
                WorldGen.PlaceTile(i, y + 4, type, forced: true);
            }

            for (int i = x + 1; i <= x + 5; i++)
            {
                if (i != x + 3)
                {
                    WorldGen.PlaceTile(i, y + 5, type, forced: true);
                }
            }
        }
        else if (size == 1)
        {
            for (int j = y; j <= y + 1; j++)
            {
                //Main.tile[x + 4, j].active(true);
                WorldGen.PlaceTile(x + 4, j, type, forced: true);
            }

            for (int i = x + 3; i <= x + 5; i++)
            {
                //Main.tile[i, y + 2].active(true);
                WorldGen.PlaceTile(i, y + 2, type, forced: true);
            }

            for (int i = x; i <= x + 8; i++)
            {
                //Main.tile[i, y + 3].active(true);
                WorldGen.PlaceTile(i, y + 3, type, forced: true);
            }

            for (int i = x + 1; i <= x + 7; i++)
            {
                WorldGen.PlaceTile(i, y + 4, type, forced: true);
            }

            for (int i = x + 2; i <= x + 6; i++)
            {
                for (int j = y + 5; j <= y + 6; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }

            for (int i = x + 1; i <= x + 7; i++)
            {
                if (i != x + 4)
                {
                    WorldGen.PlaceTile(i, y + 7, type, forced: true);
                }
            }

            for (int i = x + 1; i <= x + 7; i++)
            {
                if (i <= x + 2 || i >= x + 6)
                {
                    WorldGen.PlaceTile(i, y + 7, type, forced: true);
                }
            }
        }
        else if (size == 2)
        {
            for (int j = y; j <= y + 1; j++)
            {
                WorldGen.PlaceTile(x + 5, j, type, forced: true);
                WorldGen.SquareTileFrame(x + 5, j);
            }

            for (int i = x + 4; i <= x + 6; i++)
            {
                WorldGen.PlaceTile(i, y + 2, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 2);
            }

            for (int i = x + 3; i <= x + 7; i++)
            {
                WorldGen.PlaceTile(i, y + 3, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 3);
            }

            for (int i = x; i <= x + 10; i++)
            {
                WorldGen.PlaceTile(i, y + 4, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 4);
            }

            for (int i = x + 1; i <= x + 9; i++)
            {
                WorldGen.PlaceTile(i, y + 5, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 5);
            }

            for (int i = x + 2; i <= x + 8; i++)
            {
                WorldGen.PlaceTile(i, y + 6, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 6);
            }

            for (int i = x + 3; i <= x + 7; i++)
            {
                WorldGen.PlaceTile(i, y + 7, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 7);
            }

            for (int i = x + 2; i <= x + 8; i++)
            {
                WorldGen.PlaceTile(i, y + 8, type, forced: true);
                WorldGen.SquareTileFrame(i, y + 8);
            }

            for (int i = x + 1; i <= x + 9; i++)
            {
                for (int j = y + 9; j <= y + 10; j++)
                {
                    if (((i >= x + 2 && i <= x + 4) || (i >= x + 6 && i <= x + 8)) && j == y + 9)
                    {
                        WorldGen.PlaceTile(i, j, type, forced: true);
                        WorldGen.SquareTileFrame(i, j);
                    }

                    if (((i >= x + 1 && i <= x + 3) || (i >= x + 7 && i <= x + 9)) && j == y + 10)
                    {
                        WorldGen.PlaceTile(i, j, type, forced: true);
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
            }
        }
    }

    public static void GenerateHearts(int i, int j, int tile)
    {
        int size = WorldGen.genRand.Next(2);
        if (size == 0)
        {
            size = 1;
        }
        else if (size == 1)
        {
            size = 3;
        }
        if (WorldGen.genRand.Next(20) == 0)
        {
            size = 5;
        }
        int num2 = 1;
        WorldGen.PlaceTile(i, j + 1, (ushort)tile, forced: true);
        WorldGen.SquareTileFrame(i, j + 1);
        for (int k = j; k >= j - size; k--)
        {
            for (int l = i - num2; l <= i + num2; l++)
            {
                if ((l != i - num2 && l != i + num2) || num2 != size + 1)
                {
                    WorldGen.PlaceTile(l, k, (ushort)tile, forced: true);
                    WorldGen.SquareTileFrame(l, k);
                }
            }
            num2++;
        }
        for (int m = i - num2 + 1; m <= i + num2 - 1; m++)
        {
            WorldGen.PlaceTile(m, j - size - 1, (ushort)tile, forced: true);
            WorldGen.SquareTileFrame(m, j + size + 1);
        }
        for (int n = i - num2 + 2; n <= i + num2 - 2; n++)
        {
            if (n != i)
            {
                WorldGen.PlaceTile(n, j - size - 2, (ushort)tile, forced: true);
                WorldGen.SquareTileFrame(n, j + size + 2);
            }
        }
        for (int num3 = i - num2 + 3; num3 <= i + num2 - 3; num3++)
        {
            if (num3 != i && num3 != i + 1 && num3 != i - 1)
            {
                WorldGen.PlaceTile(num3, j - size - 3, (ushort)tile, forced: true);
                WorldGen.SquareTileFrame(num3, j + size + 3);
            }
        }
    }

    public static void GenerateBolts(int x, int y, ushort type)
    {
        int size = WorldGen.genRand.Next(2);
        if (WorldGen.genRand.Next(20) == 0)
        {
            size = 2;
        }
        if (size == 0)
        {
            for (int i = x + 1; i <= x + 4; i++)
            {
                for (int j = y; j <= y + 1; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }
            for (int i = x; i <= x + 3; i++)
            {
                WorldGen.PlaceTile(i, y + 2, type, forced: true);
            }
            for (int i = x; i <= x + 4; i++)
            {
                WorldGen.PlaceTile(i, y + 3, type, forced: true);
            }
            for (int i = x + 1; i <= x + 3; i++)
            {
                WorldGen.PlaceTile(i, y + 4, type, forced: true);
            }
            for (int i = x; i <= x + 2; i++)
            {
                WorldGen.PlaceTile(i, y + 5, type, forced: true);
            }
            for (int i = x; i <= x + 1; i++)
            {
                WorldGen.PlaceTile(i, y + 6, type, forced: true);
            }
        }
        else if (size == 1)
        {
            for (int i = x + 1; i <= x + 5; i++)
            {
                for (int j = y; j <= y + 1; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }
            for (int i = x; i <= x + 4; i++)
            {
                WorldGen.PlaceTile(i, y + 2, type, forced: true);
            }
            for (int i = x; i <= x + 5; i++)
            {
                WorldGen.PlaceTile(i, y + 3, type, forced: true);
            }
            for (int i = x + 2; i <= x + 5; i++)
            {
                WorldGen.PlaceTile(i, y + 4, type, forced: true);
            }
            for (int i = x + 1; i <= x + 4; i++)
            {
                WorldGen.PlaceTile(i, y + 5, type, forced: true);
            }
            for (int i = x + 1; i <= x + 3; i++)
            {
                WorldGen.PlaceTile(i, y + 6, type, forced: true);
            }
            for (int i = x; i <= x + 2; i++)
            {
                WorldGen.PlaceTile(i, y + 7, type, forced: true);
            }
            for (int i = x; i <= x + 1; i++)
            {
                WorldGen.PlaceTile(i, y + 8, type, forced: true);
            }
        }
        else if (size == 2)
        {
            for (int i = x + 2; i <= x + 8; i++)
            {
                for (int j = y; j <= y + 1; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }
            for (int i = x + 1; i <= x + 7; i++)
            {
                for (int j = y + 2; j <= y + 3; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }
            for (int j = y + 3; j <= y + 4; j++)
            {
                WorldGen.PlaceTile(x + 8, j, type, forced: true);
            }
            for (int i = x; i <= x + 7; i++)
            {
                for (int j = y + 4; j <= y + 5; j++)
                {
                    WorldGen.PlaceTile(i, j, type, forced: true);
                }
            }
            for (int i = x + 2; i <= x + 6; i++)
            {
                WorldGen.PlaceTile(i, y + 6, type, forced: true);
            }
            for (int i = x + 1; i <= x + 5; i++)
            {
                WorldGen.PlaceTile(i, y + 7, type, forced: true);
            }
            for (int i = x + 1; i <= x + 4; i++)
            {
                WorldGen.PlaceTile(i, y + 8, type, forced: true);
            }
            for (int i = x; i <= x + 3; i++)
            {
                WorldGen.PlaceTile(i, y + 9, type, forced: true);
            }
            for (int i = x; i <= x + 2; i++)
            {
                WorldGen.PlaceTile(i, y + 10, type, forced: true);
            }
            for (int i = x; i <= x + 1; i++)
            {
                WorldGen.PlaceTile(i, y + 11, type, forced: true);
            }
        }
    }
}
