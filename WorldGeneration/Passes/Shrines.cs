using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;
using Microsoft.Xna.Framework;
using System;

namespace Avalon.WorldGeneration.Passes;

internal class Shrines : GenPass
{
    public Shrines() : base("Avalon Shrines", 20f) { }

    private static bool IsTooClose(int x, List<Point> positions, int minDistance = 400)
    {
        foreach (Point q in positions)
        {
            if (Math.Abs(x - q.X) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        List<int> noTiles = new List<int>()
        {
            TileID.LihzahrdBrick,
            TileID.BlueDungeonBrick,
            TileID.PinkDungeonBrick,
            TileID.GreenDungeonBrick,
            ModContent.TileType<Tiles.Savanna.TuhrtlBrick>(),
            //ModContent.TileType<Tiles.TropicalGrass>(),
            //ModContent.TileType<Tiles.Loam>(),
            ModContent.TileType<Tiles.OrangeBrick>(),
            ModContent.TileType<Tiles.PurpleBrick>(),
            ModContent.TileType<Tiles.YellowBrick>(),
            ModContent.TileType<Tiles.CrackedOrangeBrick>(),
            ModContent.TileType<Tiles.CrackedPurpleBrick>(),
            ModContent.TileType<Tiles.CrackedYellowBrick>(),
            TileID.Mud,
            TileID.JungleGrass,
            TileID.CrimtaneBrick,
            TileID.DemoniteBrick,
            TileID.EbonstoneBrick
        };
        List<int> noWalls = new List<int>()
        {
            WallID.LihzahrdBrickUnsafe,
            WallID.BlueDungeonSlabUnsafe,
            WallID.BlueDungeonUnsafe,
            WallID.BlueDungeonTileUnsafe,
            WallID.PinkDungeonSlabUnsafe,
            WallID.PinkDungeonUnsafe,
            WallID.PinkDungeonTileUnsafe,
            WallID.GreenDungeonSlabUnsafe,
            WallID.GreenDungeonUnsafe,
            WallID.GreenDungeonTileUnsafe,
            ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
            ModContent.WallType<Walls.OrangeBrickUnsafe>(),
            ModContent.WallType<Walls.OrangeTiledUnsafe>(),
            ModContent.WallType<Walls.OrangeSlabUnsafe>(),
            ModContent.WallType<Walls.PurpleBrickUnsafe>(),
            ModContent.WallType<Walls.PurpleSlabWallUnsafe>(),
            ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
            ModContent.WallType<Walls.YellowBrickUnsafe>(),
            ModContent.WallType<Walls.YellowSlabWallUnsafe>(),
            ModContent.WallType<Walls.YellowTiledWallUnsafe>(),
            WallID.IceBrick
        };

        /*int amtOfBiomes = (int)((float)(Main.maxTilesX / 4200) * 2 + 1);
        float num613 = (Main.maxTilesX - 250) / amtOfBiomes;
        int num614 = 0;

        List<Point> lastPos = new List<Point>();

        bool placedShrine = false;
        while (num614 < amtOfBiomes) // amtofbiomes
        {
            float num615 = (float)num614 / amtOfBiomes;

            int xmin = 100;
            int xmax = Main.maxTilesX - 100;
            int ymin = (int)GenVars.lavaLine - 20;
            int ymax = Main.maxTilesY - 300;

            int xCoord = WorldGen.genRand.Next(xmin, xmax);
            int yCoord = WorldGen.genRand.Next(ymin, ymax);
            for (int q = 0; q < 50; q++)
            {
                if (IsTooClose(xCoord, lastPos) || (xCoord > Main.maxTilesX / 2 - 250 && xCoord < Main.maxTilesX / 2 + 250))
                {
                    xCoord = WorldGen.genRand.Next(xmin, xmax);
                }
                else break;
            }

            Point point = new(xCoord, yCoord);
            int xtemp = point.X - 60;

            Utils.GetCMXCoord(xtemp, point.Y - 65, 120, 110, ref xtemp);

            point.X = xtemp + 60;

            if (Structures.LavaOcean.GenerateLavaOcean(point.X, point.Y))
            {
                if (!placedShrine)
                {
                    Structures.LavaShrine.NewLavaShrine(point.X - 29, point.Y - 12);
                    placedShrine = true;
                }
                GenVars.structures.AddProtectedStructure(new Rectangle(xCoord - 75, yCoord - 50, 150, 100));
                lastPos.Add(point);
                num614++;
            }
        }*/

        for (int q = 0; q < 3; q++)
        {
            var x10 = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
            var y6 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);
            if (q == 2) y6 = WorldGen.genRand.Next(GenVars.lavaLine - 50, Main.maxTilesY - 250);

            if (q == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!GenVars.structures.CanPlace(new(x10, y6, 60, 35)))
                    {
                        x10 = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                        y6 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);
                    }
                    else break;
                }
            }
            else if (q == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!GenVars.structures.CanPlace(new(x10, y6, 41, 22)))
                    {
                        x10 = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                        y6 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);
                    }
                    else break;
                }
            }
            else if (q == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!GenVars.structures.CanPlace(new(x10, y6, 57, 27)))
                    {
                        x10 = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                        y6 = WorldGen.genRand.Next(GenVars.lavaLine - 50, Main.maxTilesY - 250);
                    }
                    else break;
                }
            }
            // causes making cave walls freeze somehow (MIGHT HAVE FIXED THIS (NOPE))
            while (noTiles.Contains(Main.tile[x10, y6].TileType) || noWalls.Contains(Main.tile[x10, y6].WallType))
            {
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else if (x10 < Main.maxTilesX / 2)
                    x10++;
                else break;
            }
            while (noTiles.Contains(Main.tile[x10 + 30, y6].TileType) || noWalls.Contains(Main.tile[x10 + 30, y6].WallType))
            {
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else if (x10 < Main.maxTilesX / 2)
                    x10++;
                else break;
            }
            while (noTiles.Contains(Main.tile[x10, y6 + 20].TileType) || noWalls.Contains(Main.tile[x10, y6 + 20].WallType))
            {
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else if (x10 < Main.maxTilesX / 2)
                    x10++;
                else break;
            }
            while (noTiles.Contains(Main.tile[x10 + 30, y6 + 20].TileType) || noWalls.Contains(Main.tile[x10 + 30, y6 + 20].WallType))
            {
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else if (x10 < Main.maxTilesX / 2)
                    x10++;
                else break;
            }
            if (q == 0)
            {
                Structures.IceShrine.Generate(x10, y6);
                GenVars.structures.AddProtectedStructure(new(x10, y6, 60, 35));
            }
            else if (q == 1)
            {
                Structures.EvilShrine.GenerateEvilShrine(x10, y6);
                GenVars.structures.AddProtectedStructure(new(x10, y6, 41, 22));
            }
            else if (q == 2)
            {
                Structures.LavaShrine.NewLavaShrine(x10, y6);
                GenVars.structures.AddProtectedStructure(new(x10, y6, 57, 27));
            }
        }
    }
}
