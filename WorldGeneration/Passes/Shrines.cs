using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace Avalon.WorldGeneration.Passes;

internal class Shrines : GenPass
{
    public Shrines() : base("Avalon Shrines", 20f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        List<int> noTiles = new List<int>()
        {
            TileID.LihzahrdBrick,
            TileID.BlueDungeonBrick,
            TileID.PinkDungeonBrick,
            TileID.GreenDungeonBrick,
            //ModContent.TileType<Tiles.TuhrtlBrick>(),
            //ModContent.TileType<Tiles.OrangeBrick>(),
            //ModContent.TileType<Tiles.TropicalGrass>(),
            //ModContent.TileType<Tiles.Loam>(),
            //ModContent.TileType<Tiles.OrangeBrick>(),
            //ModContent.TileType<Tiles.PurpleBrick>(),
            //ModContent.TileType<Tiles.YellowBrick>(),
            //ModContent.TileType<Tiles.CrackedOrangeBrick>(),
            //ModContent.TileType<Tiles.CrackedPurpleBrick>(),
            //ModContent.TileType<Tiles.CrackedYellowBrick>(),
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
            //ModContent.WallType<Walls.OrangeBrickUnsafe>(),
            //ModContent.WallType<Walls.OrangeTiledUnsafe>(),
            //ModContent.WallType<Walls.OrangeSlabUnsafe>(),
            //ModContent.WallType<Walls.PurpleBrickUnsafe>(),
            //ModContent.WallType<Walls.PurpleSlabWallUnsafe>(),
            //ModContent.WallType<Walls.PurpleTiledWallUnsafe>(),
            //ModContent.WallType<Walls.YellowBrickUnsafe>(),
            //ModContent.WallType<Walls.YellowSlabWallUnsafe>(),
            //ModContent.WallType<Walls.YellowTiledWallUnsafe>(),
            WallID.IceBrick
        };
        for (int q = 0; q < 3; q++)
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
            else if (q == 2) Structures.LavaShrine.AddLavaShrine(x10, y6);
        }
    }
}
