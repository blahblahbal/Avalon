using System;
using System.Collections.Generic;
using Avalon.Common;
using Avalon.Tiles.Contagion;
using Avalon.Walls;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class HardmodeGen : ModSystem
{
    public override void ModifyHardmodeTasks(List<GenPass> list)
    {
        WorldEvil evil = ModContent.GetInstance<AvalonWorld>().WorldEvil;
        if (evil == WorldEvil.Contagion)
        {
            int index2 = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Good"));
            if (index2 != -1)
            {
                list.RemoveAt(index2);
            }
            int index3 = list.FindIndex(genpass => genpass.Name.Equals("Hardmode Evil"));
            if (index3 != -1)
            {
                list.Insert(index3 + 1, new PassLegacy("Hardmode Evil", new WorldGenLegacyMethod(HardmodeContagion)));
                list.RemoveAt(index3);
            }
        }
    }

    //Until one of us is godlike at IL editing ill most likely add manual confection worldgen code here or in the confection

    private static void HardmodeContagion(GenerationProgress progres, GameConfiguration configurations)
    {
        AvalonWorld.StopStalacAndGrassFromBreakingUponHardmodeGeneration = true;
        WorldGen.IsGeneratingHardMode = true;
        WorldGen.TryProtectingSpawnedItems();
        if (Main.rand == null)
        {
            Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
        }
        double num = WorldGen.genRand.Next(300, 400) * 0.001;
        double num2 = WorldGen.genRand.Next(200, 300) * 0.001;
        int num3 = (int)(Main.maxTilesX * num);
        int num4 = (int)(Main.maxTilesX * (1.0 - num));
        int num5 = 1;
        if (WorldGen.genRand.NextBool(2))
        {
            num4 = (int)(Main.maxTilesX * num);
            num3 = (int)(Main.maxTilesX * (1.0 - num));
            num5 = -1;
        }
        int num6 = 1;
        if (GenVars.dungeonX < Main.maxTilesX / 2)
        {
            num6 = -1;
        }
        if (num6 < 0)
        {
            if (num4 < num3)
            {
                num4 = (int)(Main.maxTilesX * num2);
            }
            else
            {
                num3 = (int)(Main.maxTilesX * num2);
            }
        }
        else if (num4 > num3)
        {
            num4 = (int)(Main.maxTilesX * (1.0 - num2));
        }
        else
        {
            num3 = (int)(Main.maxTilesX * (1.0 - num2));
        }
        if (!Main.remixWorld)
        {
            WorldGen.GERunner(num3, 0, 3 * num5, 5.0);
            ContagGERunner(num4, 0, 3 * -num5, 5.0, good: false);
        }
        double num9 = (double)Main.maxTilesX / 4200.0;
        int num10 = (int)(25.0 * num9);
        ShapeData shapeData = new ShapeData();
        int num11 = 0;
        while (num10 > 0)
        {
            if (++num11 % 15000 == 0)
            {
                num10--;
            }
            Point point = WorldGen.RandomWorldPoint((int)Main.worldSurface - 100, 1, 190, 1);
            Tile tile = Main.tile[point.X, point.Y];
            Tile tile2 = Main.tile[point.X, point.Y - 1];
            ushort num12 = 0;
            if (TileID.Sets.Crimson[tile.TileType])
            {
                num12 = (ushort)(192 + WorldGen.genRand.Next(4));
            }
            else if (TileID.Sets.Corrupt[tile.TileType])
            {
                num12 = (ushort)(188 + WorldGen.genRand.Next(4));
            }
            else if (TileID.Sets.Hallow[tile.TileType])
            {
                num12 = (ushort)(200 + WorldGen.genRand.Next(4));
            }
            if (tile.HasTile && num12 != 0 && !tile2.HasTile)
            {
                bool flag = WorldUtils.Gen(new Point(point.X, point.Y - 1), new ShapeFloodFill(1000), Actions.Chain(new Modifiers.IsNotSolid(), new Modifiers.OnlyWalls(0, 54, 55, 56, 57, 58, 59, 61, 185, 212, 213, 214, 215, 2, 196, 197, 198, 199, 15, 40, 71, 64, 204, 205, 206, 207, 208, 209, 210, 211, 71), new Actions.Blank().Output(shapeData)));
                if (shapeData.Count > 50 && flag)
                {
                    WorldUtils.Gen(new Point(point.X, point.Y), new ModShapes.OuterOutline(shapeData, useDiagonals: true, useInterior: true), new Actions.PlaceWall(num12));
                    num10--;
                }
                shapeData.Clear();
            }
        }
        if (Main.netMode == 2)
        {
            Netplay.ResetSections();
        }
        AvalonWorld.StopStalacAndGrassFromBreakingUponHardmodeGeneration = false;
        WorldGen.UndoSpawnedItemProtection();
        WorldGen.IsGeneratingHardMode = false;
    }

    public static void ContagGERunner(int i, int j, double speedX = 0.0, double speedY = 0.0, bool good = true)
    {
        int num = 0;
        for (int k = 20; k < Main.maxTilesX - 20; k++)
        {
            for (int l = 20; l < Main.maxTilesY - 20; l++)
            {
                if (Main.tile[k, l].HasTile && Main.tile[k, l].TileType == TileID.Hive)
                {
                    num++;
                }
            }
        }
        bool flag = false;
        if (num > 200000)
        {
            flag = true;
        }
        int num2 = WorldGen.genRand.Next(200, 250);
        double num3 = (double)Main.maxTilesX / 4200.0;
        num2 = (int)((double)num2 * num3);
        double num4 = num2;
        Vector2D val = default(Vector2D);
        val.X = i;
        val.Y = j;
        Vector2D val2 = default(Vector2D);
        val2.X = (double)WorldGen.genRand.Next(-10, 11) * 0.1;
        val2.Y = (double)WorldGen.genRand.Next(-10, 11) * 0.1;
        if (speedX != 0.0 || speedY != 0.0)
        {
            val2.X = speedX;
            val2.Y = speedY;
        }
        bool flag2 = true;
        while (flag2)
        {
            int num5 = (int)(val.X - num4 * 0.5);
            int num6 = (int)(val.X + num4 * 0.5);
            int num7 = (int)(val.Y - num4 * 0.5);
            int num8 = (int)(val.Y + num4 * 0.5);
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesX)
            {
                num6 = Main.maxTilesX;
            }
            if (num7 < 0)
            {
                num7 = 0;
            }
            if (num8 > Main.maxTilesY - 5)
            {
                num8 = Main.maxTilesY - 5;
            }
            for (int m = num5; m < num6; m++)
            {
                for (int n = num7; n < num8; n++)
                {
                    if (!(Math.Abs((double)m - val.X) + Math.Abs((double)n - val.Y) < (double)num2 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015)))
                    {
                        continue;
                    }

                    if (Main.tile[m, n].WallType == 63 || Main.tile[m, n].WallType == 65 || Main.tile[m, n].WallType == 66 || Main.tile[m, n].WallType == 68 || Main.tile[m, n].WallType == 69 || Main.tile[m, n].WallType == 81)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionGrassWall>();
                    }
                    else if (Main.tile[m, n].WallType == 216)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<HardenedSnotsandWallUnsafe>();
                    }
                    else if (Main.tile[m, n].WallType == 187)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<SnotsandstoneWallUnsafe>();
                    }
                    else if (Main.tile[m, n].WallType == 3 || Main.tile[m, n].WallType == 83)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else if (Main.tile[m, n].WallType == 50 || Main.tile[m, n].WallType == 51 || Main.tile[m, n].WallType == 52 || Main.tile[m, n].WallType == 189 || Main.tile[m, n].WallType == 193 || Main.tile[m, n].WallType == 214 || Main.tile[m, n].WallType == 201)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionLumpWallUnsafe>();
                    }
                    else if (Main.tile[m, n].WallType == 56 || Main.tile[m, n].WallType == 188 || Main.tile[m, n].WallType == 192 || Main.tile[m, n].WallType == 213 || Main.tile[m, n].WallType == 202)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionMouldWallUnsafe>();
                    }
                    else if (Main.tile[m, n].WallType == 48 || Main.tile[m, n].WallType == 49 || Main.tile[m, n].WallType == 53 || Main.tile[m, n].WallType == 57 || Main.tile[m, n].WallType == 58 || Main.tile[m, n].WallType == 190 || Main.tile[m, n].WallType == 194 || Main.tile[m, n].WallType == 212 || Main.tile[m, n].WallType == 203)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionCystWallUnsafe>();
                    }
                    else if (Main.tile[m, n].WallType == 191 || Main.tile[m, n].WallType == 195 || Main.tile[m, n].WallType == 185 || Main.tile[m, n].WallType == 215 || Main.tile[m, n].WallType == 200 || Main.tile[m, n].WallType == 83)
                    {
                        Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionBoilWallUnsafe>();
                    }

                    // TEST
                    //if (Main.tile[m, n].TileType == TileID.Stalactite && (Main.tile[m, n].TileFrameX >= 54 && Main.tile[m, n].TileFrameX <= 90 || Main.tile[m, n].TileFrameX >= 216 && Main.tile[m, n].TileFrameX <= 360))
                    //{
                    //    if (Main.tile[m, n].TileFrameX >= 54 && Main.tile[m, n].TileFrameX <= 90)
                    //    {
                    //        Main.tile[m, n].TileFrameX -= 54;
                    //    }
                    //    if (Main.tile[m, n].TileFrameX >= 216 && Main.tile[m, n].TileFrameX <= 252)
                    //    {
                    //        Main.tile[m, n].TileFrameX -= 216;
                    //    }
                    //    if (Main.tile[m, n].TileFrameX >= 270 && Main.tile[m, n].TileFrameX <= 306)
                    //    {
                    //        Main.tile[m, n].TileFrameX -= 270;
                    //    }
                    //    if (Main.tile[m, n].TileFrameX >= 324 && Main.tile[m, n].TileFrameX <= 360)
                    //    {
                    //        Main.tile[m, n].TileFrameX -= 324;
                    //    }
                    //    Main.tile[m, n].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
                    //}

                    int type = Main.tile[m, n].TileType;

                    if (TileID.Sets.IsVine[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<ContagionVines>();
                    }
                    if (flag && Main.tile[m, n].TileType == TileID.Hive)
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<Chunkstone>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (flag && Main.tile[m, n].TileType == TileID.CrispyHoneyBlock)
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.Grass[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<Ickgrass>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.Stone[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<Chunkstone>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.Sand[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<Snotsand>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.JungleGrass[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                        WorldGen.SquareTileFrame(m, n); 
                    }
                    else if (TileID.Sets.Conversion.Ice[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<YellowIce>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.HardenedSand[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    else if (TileID.Sets.Conversion.Sandstone[type])
                    {
                        Main.tile[m, n].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                        WorldGen.SquareTileFrame(m, n);
                    }
                    
                    
                }
            }
            val += val2;
            val2.X += (double)WorldGen.genRand.Next(-10, 11) * 0.05;
            if (val2.X > speedX + 1.0)
            {
                val2.X = speedX + 1.0;
            }
            if (val2.X < speedX - 1.0)
            {
                val2.X = speedX - 1.0;
            }
            if (val.X < (double)(-num2) || val.Y < (double)(-num2) || val.X > (double)(Main.maxTilesX + num2) || val.Y > (double)(Main.maxTilesY + num2))
            {
                flag2 = false;
            }
        }
    }
}
