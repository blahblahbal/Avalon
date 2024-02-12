using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.ID;
using System;
using Avalon.Common;
using Avalon.Items;
using Microsoft.Xna.Framework;

namespace Avalon.WorldGeneration.Passes;

internal class Tropics
{
    // delete
    public static void LoamWallTask(GenerationProgress progress, GameConfiguration config)
    {
        for (int num177 = GenVars.jungleMinX; num177 <= GenVars.jungleMaxX; num177++)
        {
            for (int num178 = 0; (double)num178 < Main.maxTilesY - 200; num178++)
            {
                if (num177 >= GenVars.jungleMinX + 50 && num177 <= GenVars.jungleMaxX - 50 && num178 < Main.rockLayer)
                {
                    if (Main.tile[num177, num178].HasTile)
                    {
                        if (Main.tile[num177, num178].TileType == TileID.Grass)
                        {
                            Main.tile[num177, num178].TileType = (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>();
                        }
                        if (Main.tile[num177, num178].TileType == TileID.Dirt)
                        {
                            Main.tile[num177, num178].TileType = (ushort)ModContent.TileType<Tiles.Tropics.Loam>();
                        }
                    }
                }
                //if (((num177 >= GenVars.jungleMinX + 2 && num177 <= GenVars.jungleMaxX - 2) || !WorldGen.genRand.NextBool(2)) && ((num177 >= GenVars.jungleMinX + 3 && num177 <= GenVars.jungleMaxX - 3) || !WorldGen.genRand.NextBool(3)) && (Main.tile[num177, num178].WallType == 2 || Main.tile[num177, num178].WallType == 59))
                //    Main.tile[num177, num178].WallType = (ushort) ModContent.WallType<Walls.TropicalMudWall>();
}
        }
    }
    public static void PlatformLeafTrapTask(GenerationProgress progress, GameConfiguration config)
    {
        progress.Message = "Placing platform leaf traps";
        for (int i = 20; i < Main.maxTilesX - 20; i++)
        {
            for (int j = 150; j < Main.maxTilesY - 230; j++)
            {
                if (Main.tile[i, j].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>() && Main.tile[i, j].HasTile &&
                    !Main.tile[i, j - 1].HasTile && !Main.tile[i - 1, j - 1].HasTile && !Main.tile[i + 1, j - 1].HasTile)
                {
                    if (WorldGen.genRand.NextBool(10)) WorldgenHelper.CreateLeafTrap(i, j);
                }
            }
        }
    }
    public static void TuhrtlOutpostTask(GenerationProgress progress, GameConfiguration config)
    {
        int num562 = 0;
        progress.Message = "Generating outpost";
        long num563 = 0L;
        double num564 = 0.25;
        int y21;
        int x23;
        while (true)
        {
            y21 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 500);
            x23 = (int)(((WorldGen.genRand.NextDouble() * num564 + 0.1) * -GenVars.dungeonSide + 0.5) * Main.maxTilesX);
            if (Main.tile[x23, y21].HasTile && Main.tile[x23, y21].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
            {
                break;
            }
            if (num563++ > 2000000)
            {
                if (num564 == 0.35)
                {
                    num562++;
                    if (num562 > 10)
                    {
                        return;
                    }
                }
                num564 = Math.Min(0.35, num564 + 0.05);
                num563 = 0L;
            }
        }
        AvalonWorld.MakeTempOutpost(x23, y21);
        GenVars.structures.AddProtectedStructure(new(x23, y21, 25, 25));
    }
    public static void TropicsSanctumTask(GenerationProgress progress, GameConfiguration config)
    {
        progress.Message = "Adding tropics chests...";
        int amount = WorldGen.genRand.Next(11, 19);
        //bool flag30 = true;
        while (amount > 0)
        {
            int num406 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 250);
            int num407 = ((GenVars.dungeonSide >= 0) ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)(Main.maxTilesX * 0.6), (int)(Main.maxTilesX * 0.85)));
            if (Main.tile[num407, num406].HasTile && Main.tile[num407, num406].TileType == (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>() && GenVars.structures.CanPlace(new(num407, num406, 20, 14)))
            {
                //flag30 = false;
                Structures.TropicsSanctum.MakeSanctum(num407, num406);
                GenVars.structures.AddProtectedStructure(new(num407, num406, 20, 14));
                amount--;
            }
        }
    }
    public static void LihzahrdBrickReSolidTask(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.tileSolid[TileID.LihzahrdBrick] = true;
    }
    public static void GlowingMushroomsandJunglePlantsTask(GenerationProgress progress, GameConfiguration passConfig)
    {
        progress.Set(1f);
        int grass = ModContent.TileType<Tiles.Tropics.TropicalShortGrass>();
        for (int num207 = 0; num207 < Main.maxTilesX; num207++)
        {
            for (int num208 = 0; num208 < Main.maxTilesY; num208++)
            {
                if (Main.tile[num207, num208].HasTile)
                {
                    if (num208 >= (int)Main.worldSurface && Main.tile[num207, num208].TileType == TileID.MushroomGrass && !Main.tile[num207, num208 - 1].HasTile)
                    {
                        WorldGen.GrowTree(num207, num208);
                        if (!Main.tile[num207, num208 - 1].HasTile)
                        {
                            WorldGen.GrowTree(num207, num208);
                            if (!Main.tile[num207, num208 - 1].HasTile)
                            {
                                WorldGen.GrowShroom(num207, num208);
                                if (!Main.tile[num207, num208 - 1].HasTile)
                                {
                                    WorldGen.PlaceTile(num207, num208 - 1, TileID.MushroomPlants, mute: true);
                                }
                            }
                        }
                    }
                    if (Main.tile[num207, num208].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>() && !Main.tile[num207, num208 - 1].HasTile)
                    {
                        WorldGen.PlaceTile(num207, num208 - 1, grass, mute: true, style: WorldGen.genRand.Next(8));
                    }
                }
            }
        }
    }
    public static void JungleBushesTask(GenerationProgress progress, GameConfiguration passConfig)
    {
        progress.Set(1f);
        int bush = ModContent.TileType<Tiles.Tropics.TropicsBushes>();
        for (int num204 = 0; num204 < Main.maxTilesX * 100; num204++)
        {
            int num205 = WorldGen.genRand.Next(40, Main.maxTilesX / 2 - 40);
            if (GenVars.dungeonSide < 0)
            {
                num205 += Main.maxTilesX / 2;
            }
            int num206;
            for (num206 = WorldGen.genRand.Next(Main.maxTilesY - 300); !Main.tile[num205, num206].HasTile && num206 < Main.maxTilesY - 300; num206++)
            {
            }
            if (Main.tile[num205, num206].HasTile && Main.tile[num205, num206].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
            {
                num206--;
                WorldGen.PlaceJunglePlant(num205, num206, (ushort)bush, WorldGen.genRand.Next(8), 0);
                //if (Main.tile[num205, num206].TileType != bush)
                //{
                //    WorldGen.PlaceJunglePlant(num205, num206, (ushort)bush, WorldGen.genRand.Next(9), 1);
                //}
            }
        }
    }

    public static void WaspNests(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Adding nests...";
        int amount = WorldGen.genRand.Next(4, 8);
        //bool flag30 = true;
        while (amount > 0)
        {
            int num406 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 250);
            int num407 = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.85));
            //((AvalonWorld.dungeonSide >= 0) ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)(Main.maxTilesX * 0.6), (int)(Main.maxTilesX * 0.85)));
            if (Main.tile[num407, num406].HasTile && Main.tile[num407, num406].TileType == (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>() && GenVars.structures.CanPlace(new Rectangle(num407 - 100, num406 - 100, 100, 100)))
            {
                //flag30 = false;
                Structures.Nest.CreateWaspNest(num407, num406);
                GenVars.structures.AddProtectedStructure(new Rectangle(num407 - 100, num406 - 100, 100, 100)); // -50, -21, 87, 66
                amount--;
            }
        }
    }
    public static void JunglesWetTask(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Set(1f);
        for (int i = 0; i < Main.maxTilesX; i++)
        {
            int i2 = i;
            for (int j = (int)GenVars.worldSurfaceLow; j < Main.worldSurface - 1.0; j++)
            {
                Tile tile49 = Main.tile[i2, j];
                if (tile49.HasTile)
                {
                    tile49 = Main.tile[i2, j];
                    bool bl = tile49.TileType == 60;
                    if (bl)
                    {
                        tile49 = Main.tile[i2, j - 1];
                        tile49.LiquidAmount = 255;
                        tile49 = Main.tile[i2, j - 2];
                        tile49.LiquidAmount = 255;
                    }
                    break;
                }
            }
        }
    }
    public static void JunglesGrassTask(GenerationProgress progress, GameConfiguration passConfig)
	{
		progress.Message = Lang.gen[77].Value;
		for (int i = 0; i < Main.maxTilesX; i++)
		{
			for (int j = 0; j < Main.maxTilesY; j++)
			{
				if (Main.tile[i, j].HasUnactuatedTile)
				{
					WorldGen.grassSpread = 0;
					WorldGen.SpreadGrass(i, j, ModContent.TileType<Tiles.Tropics.Loam>(), ModContent.TileType<Tiles.Tropics.TropicalGrass>(), repeat: true, default);
				}
				progress.Set(0.2f * ((i * Main.maxTilesY + j) / (float)(Main.maxTilesX * Main.maxTilesY)));
			}
		}
		WorldGen.SmallConsecutivesFound = 0;
		WorldGen.SmallConsecutivesEliminated = 0;
		float rightBorder = Main.maxTilesX - 20;
		for (int i = 10; i < Main.maxTilesX - 10; i++)
		{
            ScanTileColumnAndRemoveClumps(i);
            //WorldGen_ScanTileColumnAndRemoveClumps.Invoke(null, new object[] { i });
            float num835 = (i - 10) / rightBorder;
			progress.Set(0.2f + num835 * 0.8f);
		}
	}

    private static void ScanTileColumnAndRemoveClumps(int x)
    {
        int num = 0;
        int y = 0;
        for (int i = 10; i < Main.maxTilesY - 10; i++)
        {
            if (Main.tile[x, i].HasTile && Main.tileSolid[Main.tile[x, i].TileType] && TileID.Sets.CanBeClearedDuringGeneration[Main.tile[x, i].TileType])
            {
                if (num == 0)
                    y = i;

                num++;
                continue;
            }

            if (num > 0 && num < 20)
            {
                WorldGen.SmallConsecutivesFound++;
                if (WorldGen.tileCounter(x, y) < 20)
                {
                    WorldGen.SmallConsecutivesEliminated++;
                    WorldGen.tileCounterKill();
                }
            }

            num = 0;
        }
    }

    #region reflection stuff
    internal static MethodInfo WorldGen_ScanTileColumnAndRemoveClumps = null;

    internal static void Init()
    {
        WorldGen_ScanTileColumnAndRemoveClumps = typeof(WorldGen).GetMethod("ScanTileColumnAndRemoveClumps", BindingFlags.NonPublic | BindingFlags.Static);
    }

    internal static void Unload()
    {
        WorldGen_ScanTileColumnAndRemoveClumps = null;
    }
    #endregion
}
