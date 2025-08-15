using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.ID;
using System;
using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.GameContent.Biomes;
using Avalon.ModSupport;
using Avalon.WorldGeneration.Enums;
using Avalon.Tiles.Savanna;
using ReLogic.Utilities;
using Avalon.Hooks;
using System.Collections.Generic;
using ModLiquidLib.ModLoader;
using Avalon.ModSupport.MLL.Liquids;

namespace Avalon.WorldGeneration.Passes;

internal class Savanna
{
	public static void ReplaceLavaWithBlood(GenerationProgress progress, GameConfiguration config)
	{
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int y = (int)Main.rockLayer; y < Main.maxTilesY - 150; y++)
			{
				if (Main.tile[x, y].WallType == ModContent.WallType<Walls.NestWall>())
				{
					if (Main.tile[x, y].LiquidAmount > 0)
					{
						Main.tile[x, y].Liquid(LiquidLoader.LiquidType<Blood>());
					}
					if (WorldGen.genRand.NextBool(3))
					{
						Utils.PlaceCustomTight(x, y, (ushort)ModContent.TileType<NestStalac>());
					}
				}
			}
		}
	}

    public static void TuhrtlOutpostReplaceTraps(GenerationProgress progress, GameConfiguration config)
    {
        for (int x = GenVars.tLeft - 20; x < GenVars.tRight + 20; x++)
        {
            for (int y = GenVars.tTop; y < GenVars.tBottom; y++)
            {
                Tile trap = Main.tile[x, y];
                if (trap.TileType == TileID.PressurePlates)
                {
                    switch (trap.TileFrameY / 18)
                    {
                        case 6:
                            trap.TileType = (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlPressurePlate>();
                            trap.TileFrameY -= 18 * 6;
                            break;
                        default:
                            break;
                    }
                }
                if (trap.TileType == TileID.Traps)
                {
                    switch (trap.TileFrameY / 18)
                    {
                        case 1:
                            trap.TileType = (ushort)ModContent.TileType<Tiles.Savanna.CannonballTrap>();
                            trap.TileFrameY -= 18;
                            break;
                        case 2:
                            trap.TileType = (ushort)ModContent.TileType<Tiles.Savanna.PoisonGasTrap>();
                            trap.TileFrameY -= 36;
                            break;
                        case 3:
                            trap.TileType = (ushort)ModContent.TileType<Tiles.Savanna.FireballTrap>();
                            trap.TileFrameY -= 54;
                            break;
                        case 4:
                            trap.TileType = (ushort)ModContent.TileType<Tiles.Savanna.LanceTrap>();
                            trap.TileFrameY -= 72;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public static void TuhrtlOutpostTask(GenerationProgress progress, GameConfiguration config)
    {
        int num648 = 0;
        progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.TuhrtlOutpost");
        long num649 = 0L;
        double num650 = 0.25;
        bool flag38 = false;
        while (true)
        {
            int num651 = (int)Main.rockLayer;
            int num652 = Main.maxTilesY - 500;
            if (num651 > num652 - 1)
                num651 = num652 - 1;

            int num653 = WorldGen.genRand.Next(num651, num652);
            int num654 = (int)(((WorldGen.genRand.NextDouble() * num650 + 0.1) * (double)(-GenVars.dungeonSide) + 0.5) * (double)Main.maxTilesX);
            if (WorldGen.remixWorldGen)
            {
                if (WorldGen.notTheBees)
                {
                    num654 = ((GenVars.dungeonSide <= 0) ? WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.6), (int)((double)Main.maxTilesX * 0.8)) : WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.2), (int)((double)Main.maxTilesX * 0.4)));
                }
                else
                {
                    num654 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.2), (int)((double)Main.maxTilesX * 0.8));
                    while ((double)num654 > (double)Main.maxTilesX * 0.4 && (double)num654 < (double)Main.maxTilesX * 0.6)
                    {
                        num654 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.2), (int)((double)Main.maxTilesX * 0.8));
                    }
                }

                while (Main.tile[num654, num653].HasTile || Main.tile[num654, num653].WallType > 0 || (double)num653 > Main.worldSurface - 5.0)
                {
                    num653--;
                }

                num653++;
                if (Main.tile[num654, num653].HasTile && (Main.tile[num654, num653].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() || Main.tile[num654, num653].TileType == ModContent.TileType<Tiles.Savanna.Loam>()))
                {
                    int num655 = 10;
                    bool flag39 = false;
                    for (int num656 = num654 - num655; num656 <= num656 + num655; num656++)
                    {
                        for (int num657 = num653 - num655; num657 < num655; num657++)
                        {
                            if (Main.tile[num656, num657].TileType == 191 || Main.tileDungeon[Main.tile[num656, num657].TileType])
                                flag39 = true;
                        }
                    }

                    if (!flag39)
                    {
                        flag38 = true;
                        num653 -= 10 + WorldGen.genRand.Next(10);
                        Structures.TuhrtlOutpost.Outpost(num654, num653);
                        break;
                    }
                }
            }
            else if (Main.tile[num654, num653].HasTile && Main.tile[num654, num653].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>())
            {
                flag38 = true;
                Structures.TuhrtlOutpost.Outpost(num654, num653);
                break;
            }

            if (num649++ > 2000000)
            {
                if (num650 == 0.35)
                {
                    num648++;
                    if (num648 > 10)
                        break;
                }

                num650 = Math.Min(0.35, num650 + 0.05);
                num649 = 0L;
            }
        }

        if (!flag38)
        {
            int x14 = Main.maxTilesX - GenVars.dungeonX;
            int y14 = (int)Main.rockLayer + 100;
            if (WorldGen.remixWorldGen)
                x14 = ((!WorldGen.notTheBees) ? ((GenVars.dungeonSide > 0) ? ((int)((double)Main.maxTilesX * 0.4)) : ((int)((double)Main.maxTilesX * 0.6))) : ((GenVars.dungeonSide > 0) ? ((int)((double)Main.maxTilesX * 0.3)) : ((int)((double)Main.maxTilesX * 0.7))));

            Structures.TuhrtlOutpost.Outpost(x14, y14);
        }
    }
    public static void PlatformLeafTrapTask(GenerationProgress progress, GameConfiguration config)
    {
        progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.PlatformLeaves");
        for (int i = 20; i < Main.maxTilesX - 20; i++)
        {
            for (int j = 150; j < Main.maxTilesY - 230; j++)
            {
                if (j > Main.rockLayer)
                {
                    if (Main.tile[i, j].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() && Main.tile[i, j].HasTile &&
                        Main.tile[i + 1, j].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() && Main.tile[i + 1, j].HasTile &&
                        Main.tile[i - 1, j].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() && Main.tile[i - 1, j].HasTile &&
                        !Main.tile[i, j - 1].HasTile && !Main.tile[i - 1, j - 1].HasTile && !Main.tile[i + 1, j - 1].HasTile)
                    {
                        if (WorldGen.genRand.NextBool(5)) Structures.LeafTrap.CreateLeafTrap(i, j + 2);
                    }
                }
            }
        }
    }
    public static void SavannaSanctumTask(GenerationProgress progress, GameConfiguration config)
    {
        progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.Chests");
        float amount = WorldGen.genRand.Next(7, 12);
        amount *= Main.maxTilesX / 4200;
        //bool flag30 = true;
        while (amount > 0)
        {
            int num406 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 250);
            int num407 = ((GenVars.dungeonSide >= 0) ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)(Main.maxTilesX * 0.6), (int)(Main.maxTilesX * 0.85)));
            if (Main.tile[num407, num406].HasTile && Main.tile[num407, num406].TileType == (ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>() && GenVars.structures.CanPlace(new(num407, num406, 20, 14)))
            {
                //flag30 = false;
                Structures.SavannaSanctum.MakeSanctum(num407, num406);
                GenVars.structures.AddProtectedStructure(new(num407, num406, 20, 14));
                amount--;
            }
        }
    }
    public static void LihzahrdBrickReSolidTask(GenerationProgress progress, GameConfiguration configuration)
    {
        Main.tileSolid[TileID.LihzahrdBrick] = true;
        Main.tileSolid[ModContent.TileType<Tiles.Savanna.TuhrtlBrick>()] = true;
        Main.tileSolid[ModContent.TileType<Tiles.Savanna.BrambleSpikes>()] = true;
    }
    public static void GlowingMushroomsandJunglePlantsTask(GenerationProgress progress, GameConfiguration passConfig)
    {
        progress.Set(1f);
        int grass = ModContent.TileType<Tiles.Savanna.SavannaShortGrass>();
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
                    if (Main.tile[num207, num208].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() && !Main.tile[num207, num208 - 1].HasTile &&
                        !Main.tile[num207, num208].IsHalfBlock && Main.tile[num207, num208].Slope == SlopeType.Solid)
                    {
                        Tile t = Main.tile[num207, num208 - 1];
                        t.TileType = (ushort)grass;
                        t.HasTile = true;
                        if (WorldGen.genRand.NextBool(60) && (num208 > Main.rockLayer || Main.remixWorld || WorldGen.remixWorldGen))
                        {
                            t.TileFrameX = 8 * 18;
                        }
                        else if (WorldGen.genRand.NextBool(230) && (num208 > Main.rockLayer || Main.remixWorld || WorldGen.remixWorldGen))
                        {
                            t.TileFrameX = 9 * 18;
                        }
                        else if (WorldGen.genRand.NextBool(15))
                        {
                            if (!WorldGen.genRand.NextBool(3))
                            {
                                t.TileFrameX = (short)(WorldGen.genRand.Next(6, 8) * 18);
                            }
                            else
                            {
                                t.TileFrameX = (short)(WorldGen.genRand.Next(10, 23) * 18);
                            }
                        }
                        else
                        {
                            t.TileFrameX = (short)(WorldGen.genRand.Next(6) * 18);
                        }
                        //WorldGen.PlaceTile(num207, num208 - 1, grass, mute: true, style: WorldGen.genRand.Next(8));
                    }
                }
            }
        }
    }
    public static void JungleBushesTask(GenerationProgress progress, GameConfiguration passConfig)
    {
        progress.Set(1f);
        int bush = ModContent.TileType<Tiles.Savanna.SavannaBushes>();
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
            if (Main.tile[num205, num206].HasTile && Main.tile[num205, num206].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>())
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
        progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.Nests");
        float amount = Main.maxTilesX / 4200;
        int amt = 1 + WorldGen.genRand.Next((int)(5 * amount), (int)(8 * amount));
        //bool flag30 = true;
        while (amt > 0)
        {
            int num406 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 250);
            int num407 = WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.85));
            //((AvalonWorld.dungeonSide >= 0) ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.15), (int)(Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)(Main.maxTilesX * 0.6), (int)(Main.maxTilesX * 0.85)));
            if (Main.tile[num407, num406].HasTile && Main.tile[num407, num406].TileType == (ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>() && GenVars.structures.CanPlace(new Rectangle(num407 - 50, num406 - 21, 87, 66)))
            {
                //flag30 = false;
                Structures.Nest.CreateWaspNest(num407, num406);
                GenVars.structures.AddProtectedStructure(new Rectangle(num407 - 50, num406 - 21, 87, 66)); // -50, -21, 87, 66
                amt--;
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
					WorldGen.SpreadGrass(i, j, ModContent.TileType<Tiles.Savanna.Loam>(), ModContent.TileType<Tiles.Savanna.SavannaGrass>(), repeat: true, default);
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

internal class SavannaPassHook : ModHook
{
	private readonly List<ushort> replaceableTiles = [ (ushort)ModContent.TileType<Loam>() ];

	FieldInfo? worldScale = typeof(JunglePass).GetField("_worldScale", BindingFlags.Instance | BindingFlags.NonPublic);
	protected override void Apply()
	{
		On_JunglePass.ApplyPass += On_JunglePass_ApplyPass;
		IL_WorldGen.TileRunner += IL_WorldGen_TileRunner;
	}



	private void IL_WorldGen_TileRunner(MonoMod.Cil.ILContext il)
	{
		Utilities.AddAlternativeIdChecks(il, (int)TileID.Mud, id => id >= 0 && Data.Sets.TileSets.Loam[id]);
	}

	private void On_JunglePass_ApplyPass(On_JunglePass.orig_ApplyPass orig, JunglePass self, GenerationProgress progress, GameConfiguration configuration)
	{
		if (!AltLibrarySupport.Enabled && ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Savanna)
		{
			progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.Generating");
			
			worldScale?.SetValue(self, Main.maxTilesX / 4200.0 * 1.5);
			double ws = (double)worldScale?.GetValue(self);

			Point val = CreateStartPoint();
			int x = val.X;
			int y = val.Y;
			Point zero = Point.Zero;
			ApplyRandomMovement(ref x, ref y, 100, 100, self);
			zero.X += x;
			zero.Y += y;
			PlaceFirstPassMud(x, y, 3, self);
			PlaceGemsAt(x, y, 63, 2, self);
			progress.Set(0.15);
			ApplyRandomMovement(ref x, ref y, 250, 150, self);
			zero.X += x;
			zero.Y += y;
			PlaceFirstPassMud(x, y, 0, self);
			PlaceGemsAt(x, y, 65, 2, self);
			progress.Set(0.3);
			int oldX = x;
			int oldY = y;
			ApplyRandomMovement(ref x, ref y, 400, 150, self);
			zero.X += x;
			zero.Y += y;
			PlaceFirstPassMud(x, y, -3, self);
			PlaceGemsAt(x, y, 67, 2, self);
			progress.Set(0.45);
			x = zero.X / 3;
			y = zero.Y / 3;
			int num = WorldGen.genRand.Next((int)(400.0 * (double)worldScale?.GetValue(self)), (int)(600.0 * (double)worldScale?.GetValue(self)));
			int num2 = (int)(25.0 * (double)worldScale?.GetValue(self));
			x = Terraria.Utils.Clamp(x, GenVars.leftBeachEnd + num / 2 + num2, GenVars.rightBeachStart - num / 2 - num2);
			GenVars.mudWall = true;
			WorldGen.TileRunner(x, y, num, 75, ModContent.TileType<Loam>(), addTile: false, 0.0, -20.0, noYChange: true);
			GenerateTunnelToSurface(x, y);
			GenVars.mudWall = false;
			CreateCaves();
			ReplaceJungleWalls();
			progress.Set(0.6);
		}
		else orig.Invoke(self, progress, configuration);
	}
	private void GenerateTunnelToSurface(int i, int j)
	{
		double num = WorldGen.genRand.Next(5, 11);
		Vector2D vector2D = new Vector2D
		{
			X = i,
			Y = j
		};
		Vector2D vector2D2 = new Vector2D
		{
			X = (double)WorldGen.genRand.Next(-10, 11) * 0.1,
			Y = (double)WorldGen.genRand.Next(10, 20) * 0.1
		};
		int num2 = 0;
		bool flag = true;
		while (flag)
		{
			if (vector2D.Y < Main.worldSurface)
			{
				if (WorldGen.drunkWorldGen)
				{
					flag = false;
				}
				int value = (int)vector2D.X;
				int value2 = (int)vector2D.Y;
				value = Terraria.Utils.Clamp(value, 10, Main.maxTilesX - 10);
				value2 = Terraria.Utils.Clamp(value2, 10, Main.maxTilesY - 10);
				if (value2 < 5)
				{
					value2 = 5;
				}
				if (Main.tile[value, value2].WallType == 0 && !Main.tile[value, value2].HasTile && Main.tile[value, value2 - 3].WallType == 0 && !Main.tile[value, value2 - 3].HasTile && Main.tile[value, value2 - 1].WallType == 0 && !Main.tile[value, value2 - 1].HasTile && Main.tile[value, value2 - 4].WallType == 0 && !Main.tile[value, value2 - 4].HasTile && Main.tile[value, value2 - 2].WallType == 0 && !Main.tile[value, value2 - 2].HasTile && Main.tile[value, value2 - 5].WallType == 0 && !Main.tile[value, value2 - 5].HasTile)
				{
					flag = false;
				}
			}
			GenVars.JungleX = (int)vector2D.X;
			num += (double)WorldGen.genRand.Next(-20, 21) * 0.1;
			if (num < 5.0)
			{
				num = 5.0;
			}
			if (num > 10.0)
			{
				num = 10.0;
			}
			int value3 = (int)(vector2D.X - num * 0.5);
			int value4 = (int)(vector2D.X + num * 0.5);
			int value5 = (int)(vector2D.Y - num * 0.5);
			int value6 = (int)(vector2D.Y + num * 0.5);
			int num3 = Terraria.Utils.Clamp(value3, 10, Main.maxTilesX - 10);
			value4 = Terraria.Utils.Clamp(value4, 10, Main.maxTilesX - 10);
			value5 = Terraria.Utils.Clamp(value5, 10, Main.maxTilesY - 10);
			value6 = Terraria.Utils.Clamp(value6, 10, Main.maxTilesY - 10);
			for (int k = num3; k < value4; k++)
			{
				for (int l = value5; l < value6; l++)
				{
					if (Math.Abs((double)k - vector2D.X) + Math.Abs((double)l - vector2D.Y) < num * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
					{
						WorldGen.KillTile(k, l);
					}
				}
			}
			num2++;
			if (num2 > 10 && WorldGen.genRand.Next(50) < num2)
			{
				num2 = 0;
				int num4 = -2;
				if (WorldGen.genRand.Next(2) == 0)
				{
					num4 = 2;
				}
				WorldGen.TileRunner((int)vector2D.X, (int)vector2D.Y, WorldGen.genRand.Next(3, 20), WorldGen.genRand.Next(10, 100), -1, addTile: false, num4);
			}
			vector2D += vector2D2;
			vector2D2.Y += (double)WorldGen.genRand.Next(-10, 11) * 0.01;
			if (vector2D2.Y > 0.0)
			{
				vector2D2.Y = 0.0;
			}
			if (vector2D2.Y < -2.0)
			{
				vector2D2.Y = -2.0;
			}
			vector2D2.X += (double)WorldGen.genRand.Next(-10, 11) * 0.1;
			if (vector2D.X < (double)(i - 200))
			{
				vector2D2.X += (double)WorldGen.genRand.Next(5, 21) * 0.1;
			}
			if (vector2D.X > (double)(i + 200))
			{
				vector2D2.X -= (double)WorldGen.genRand.Next(5, 21) * 0.1;
			}
			if (vector2D2.X > 1.5)
			{
				vector2D2.X = 1.5;
			}
			if (vector2D2.X < -1.5)
			{
				vector2D2.X = -1.5;
			}
		}
	}
	private Point CreateStartPoint()
	{
		return new Point(GenVars.jungleOriginX, (int)((double)Main.maxTilesY + Main.rockLayer) / 2);
	}
	private void ApplyRandomMovement(ref int x, ref int y, int xRange, int yRange, JunglePass self)
	{
		x += WorldGen.genRand.Next((int)((double)(-xRange) * (double)worldScale?.GetValue(self)), 1 + (int)((double)xRange * (double)worldScale?.GetValue(self)));
		y += WorldGen.genRand.Next((int)((double)(-yRange) * (double)worldScale?.GetValue(self)), 1 + (int)((double)yRange * (double)worldScale?.GetValue(self)));
		y = Terraria.Utils.Clamp(y, (int)Main.rockLayer, Main.maxTilesY);
	}
	private void PlaceFirstPassMud(int x, int y, int xSpeedScale, JunglePass self)
	{
		GenVars.mudWall = true;
		WorldGen.TileRunner(x, y, WorldGen.genRand.Next((int)(250.0 * (double)worldScale?.GetValue(self)), (int)(500.0 * (double)worldScale?.GetValue(self))), WorldGen.genRand.Next(50, 150), ModContent.TileType<Loam>(), addTile: true, GenVars.dungeonSide * xSpeedScale);
		GenVars.mudWall = false;
	}

	private void PlaceGemsAt(int x, int y, ushort baseGem, int gemVariants, JunglePass self)
	{
		for (int i = 0; (double)i < 6.0 * (double)worldScale?.GetValue(self); i++)
		{
			WorldGen.TileRunner(x + WorldGen.genRand.Next(-(int)(125.0 * (double)worldScale?.GetValue(self)), (int)(125.0 * (double)worldScale?.GetValue(self))), y + WorldGen.genRand.Next(-(int)(125.0 * (double)worldScale?.GetValue(self)), (int)(125.0 * (double)worldScale?.GetValue(self))), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(baseGem, baseGem + gemVariants));
		}
	}

	private void ReplaceJungleWalls()
	{
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int y = (int)Main.worldSurface; y < Main.maxTilesY - 180; y++)
			{
				if (Main.tile[x, y].WallType == WallID.JungleUnsafe)
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.SavannaGrassWall>();
				}
				if (Main.tile[x, y].WallType == WallID.MudUnsafe)
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.LoamWall>();
				}
			}
		}
	}

	private void CreateCaves()
	{
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int y = (int)Main.rockLayer; y < Main.maxTilesY - 200; y++)
			{
				if (replaceableTiles.Contains(Main.tile[x, y].TileType) && WorldGen.genRand.NextBool(750))
				{
					WorldGen.Cavinator(x, y, WorldGen.genRand.Next(10, 25));
				}
				if (Main.tile[x, y].WallType == WallID.JungleUnsafe)
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.SavannaGrassWall>();
				}
			}
		}
	}
}