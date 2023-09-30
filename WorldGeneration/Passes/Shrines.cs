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

    private static bool IsTooClose(int x, List<Point> positions, int minDistance = 100)
    {
        minDistance = 300;
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
            //ModContent.TileType<Tiles.TuhrtlBrick>(),
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
            //ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
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

        int amtOfBiomes = (int)((float)(Main.maxTilesX / 4200) * 2 + 1);
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
            for (int q = 0; q < 15; q++)
            {
                if (IsTooClose(xCoord, lastPos))
                {
                    xCoord = WorldGen.genRand.Next(xmin, xmax);
                }
                else break;
            }

            Point point = new(xCoord, yCoord); //WorldGen.RandomRectanglePoint(xmin, ymin, xmax, ymax);
            Utils.GetCMXCoord(point.X, point.Y, 120, 110, ref point.X);

            if (Structures.LavaOcean.GenerateLavaOcean(point.X, point.Y))
            {
                if (!placedShrine)
                {
                    Structures.LavaShrine.AddLavaShrine(point.X - 22, point.Y + 2);
                    placedShrine = true;
                }
                lastPos.Add(point);
                num614++;
            }
        }

        for (int q = 0; q < 2; q++)
        {
            var x10 = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
            var y6 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 300);
            if (q == 2) y6 = WorldGen.genRand.Next(GenVars.lavaLine - 50, Main.maxTilesY - 250);
            // causes making cave walls freeze somehow (MIGHT HAVE FIXED THIS (NOPE))
            while (noTiles.Contains(Main.tile[x10, y6].TileType) || noWalls.Contains(Main.tile[x10, y6].WallType))
            {
                if (x10 == Main.maxTilesX / 2)
                {
                    break;
                }
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else
                    x10++;
                //if (x10 >= Main.maxTilesX / 2 - 10 && x10 <= Main.maxTilesX / 2 + 10)
                //    break;
            }
            while (noTiles.Contains(Main.tile[x10 + 30, y6].TileType) || noWalls.Contains(Main.tile[x10 + 30, y6].WallType))
            {
                if (x10 == Main.maxTilesX / 2)
                {
                    break;
                }
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else
                    x10++;
            }
            while (noTiles.Contains(Main.tile[x10, y6 + 20].TileType) || noWalls.Contains(Main.tile[x10, y6 + 20].WallType))
            {
                if (x10 == Main.maxTilesX / 2)
                {
                    break;
                }
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else
                    x10++;
            }
            while (noTiles.Contains(Main.tile[x10 + 30, y6 + 20].TileType) || noWalls.Contains(Main.tile[x10 + 30, y6 + 20].WallType))
            {
                if (x10 == Main.maxTilesX / 2)
                {
                    break;
                }
                if (x10 > (Main.maxTilesX / 2))
                    x10--;
                else
                    x10++;
            }
            if (q == 0) Structures.IceShrine.Generate(x10, y6);
            else if (q == 1) Structures.EvilShrine.GenerateEvilShrine(x10, y6);
            //else if (q == 2) Structures.LavaShrine.AddLavaShrine(x10, y6);
        }
    }
}
