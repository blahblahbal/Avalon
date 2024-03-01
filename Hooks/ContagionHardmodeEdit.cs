using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Walls;
using Avalon.Tiles.Contagion;
using MonoMod.Cil;
using Terraria.IO;
using Terraria.WorldBuilding;
using Avalon.WorldGeneration.Enums;
using ReLogic.Utilities;
using System;

namespace Avalon.Hooks
{
    internal class ContagionHardmodeEdit : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.GERunner += On_WorldGen_GERunner;
        }

        private void On_WorldGen_GERunner(On_WorldGen.orig_GERunner orig, int i, int j, double speedX, double speedY, bool good)
        {
            WorldEvil evil = ModContent.GetInstance<AvalonWorld>().WorldEvil;
            if (evil == WorldEvil.Contagion && !good)
            {
                AvalonWorld.StopStalacAndGrassFromBreakingUponHardmodeGeneration = true;
                ContagGERunner(i, j, speedX, speedY, good);
                AvalonWorld.StopStalacAndGrassFromBreakingUponHardmodeGeneration = false;
                return;
            }
            else
            {
                GERunnerEdits(i, j, speedX, speedY, good);
            }
            orig.Invoke(i, j, speedX, speedY, good);
        }

        private static void GERunnerCalulations(out Vector2D val, out int num4, out int num2, out Vector2D val2, out bool flag, int i, int j, double speedX = 0.0, double speedY = 0.0)
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
            flag = false;
            if (num > 200000)
            {
                flag = true;
            }
            num2 = WorldGen.genRand.Next(200, 250);
            double num3 = (double)Main.maxTilesX / 4200.0;
            num2 = (int)((double)num2 * num3);
            num4 = num2;
            val = default(Vector2D);
            val.X = i;
            val.Y = j;
            val2 = default(Vector2D);
            val2.X = (double)WorldGen.genRand.Next(-10, 11) * 0.1;
            val2.Y = (double)WorldGen.genRand.Next(-10, 11) * 0.1;
            if (speedX != 0.0 || speedY != 0.0)
            {
                val2.X = speedX;
                val2.Y = speedY;
            }
        }

        public static void ContagGERunner(int i, int j, double speedX = 0.0, double speedY = 0.0, bool good = true)
        {
            GERunnerCalulations(out Vector2D val, out int num4, out int num2, out Vector2D val2, out bool flag, i, j, speedX, speedY);
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
                        if (flag && Main.tile[m, n].TileType == TileID.Hive && Main.tile[m, n].TileType != (ushort)ModContent.TileType<Chunkstone>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<Chunkstone>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (flag && Main.tile[m, n].TileType == TileID.CrispyHoneyBlock && Main.tile[m, n].TileType != (ushort)ModContent.TileType<HardenedSnotsand>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.Grass[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<HardenedSnotsand>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<Ickgrass>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.Stone[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<Chunkstone>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<Chunkstone>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.Sand[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<Snotsand>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<Snotsand>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.JungleGrass[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<ContagionJungleGrass>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.Ice[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<YellowIce>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<YellowIce>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.HardenedSand[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<HardenedSnotsand>())
                        {
                            Main.tile[m, n].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                            WorldGen.SquareTileFrame(m, n);
                        }
                        else if (TileID.Sets.Conversion.Sandstone[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<Snotsandstone>())
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

        public static void GERunnerEdits(int i, int j, double speedX = 0.0, double speedY = 0.0, bool good = true)
        {
            GERunnerCalulations(out Vector2D val, out int num4, out int num2, out Vector2D val2, out bool flag, i, j, speedX, speedY);
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
                        WorldEvil evil = ModContent.GetInstance<AvalonWorld>().WorldEvil;

                        if (good) //someone add manual support, im not spending 5 hours on listing every tile and wall
                        {
                            if (Main.tile[m, n].TileType == ModContent.TileType<Ickgrass>())
                            {
                                Main.tile[m, n].TileType = TileID.HallowedGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            //Add mod call converts that list modded tile and what it converts to, im too fucking depressed to do this right now, sorry for not completing alot of shit
                        }
                        else if (evil == WorldEvil.Crimson)
                        {
                            if (Main.tile[m, n].TileType == ModContent.TileType<Ickgrass>())
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
                        }
                        else if (evil == WorldEvil.Corruption)
                        {
                            if (Main.tile[m, n].TileType == ModContent.TileType<Ickgrass>())
                            {
                                Main.tile[m, n].TileType = TileID.CorruptGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
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
}
