using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.ID;

namespace Avalon.WorldGeneration.Passes;

internal class Tropics
{
    public static void LihzahrdBrickReSolidTask(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.tileSolid[ModContent.TileType<Tiles.Tropics.TuhrtlBrick>()] = true;
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
                        WorldGen.PlaceTile(num207, num208 - 1, grass, mute: true, style: grass == TileID.JunglePlants ? 0 : WorldGen.genRand.Next(8));
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
                if (Main.tile[num205, num206].TileType != bush)
                {
                    WorldGen.PlaceJunglePlant(num205, num206, (ushort)bush, WorldGen.genRand.Next(12), 1);
                }
            }
        }
    }

    public static void WaspNests(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Adding nests...";
        int amount = WorldGen.genRand.Next(6, 10);
        //bool flag30 = true;
        while (amount > 0)
        {
            int num406 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 250);
            int num407 = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.85));
            //((AvalonWorld.dungeonSide >= 0) ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)(Main.maxTilesX * 0.6), (int)(Main.maxTilesX * 0.85)));
            if (Main.tile[num407, num406].HasTile && Main.tile[num407, num406].TileType == (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>())
            {
                //flag30 = false;
                Structures.Nest.CreateNest(num407, num406);
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
			//WorldGen_ScanTileColumnAndRemoveClumps.Invoke(null, new object[] { i });
			float num835 = (i - 10) / rightBorder;
			progress.Set(0.2f + num835 * 0.8f);
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
