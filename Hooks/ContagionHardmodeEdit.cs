using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using MonoMod.Cil;
using Terraria.IO;
using Terraria.WorldBuilding;
using Avalon.WorldGeneration.Enums;
using ReLogic.Utilities;
using System;
using Avalon.ModSupport;
using Avalon.Tiles.Contagion.Chunkstone;
using Avalon.Tiles.Contagion.ContagionGrasses;
using Avalon.Tiles.Contagion.SmallPlants;
using Avalon.Tiles.Contagion.Snotsand;
using Avalon.Tiles.Contagion.YellowIce;
using Avalon.Tiles.Contagion.Snotsandstone;
using Avalon.Tiles.Contagion.HardenedSnotsand;
using Avalon.Tiles.Contagion.BacciliteBrick;
using Avalon.Walls.Contagion.HardenedSnotsandWall;
using Avalon.Walls.Contagion.SnotsandstoneWall;
using Avalon.Walls.Contagion.ChunkstoneWall;
using Avalon.Walls.Contagion.ContagionLumpWall;
using Avalon.Walls.Contagion.ContagionMouldWall;
using Avalon.Walls.Contagion.ContagionCystWall;
using Avalon.Walls.Contagion.ContagionBoilWall;
using Avalon.Walls.Contagion.ContagionGrassWall;

namespace Avalon.Hooks
{
    internal class ContagionHardmodeEdit : ModHook
    {
		public override bool IsLoadingEnabled(Mod mod) => !AltLibrarySupport.Enabled;
        protected override void Apply()
        {
            On_WorldGen.GERunner += On_WorldGen_GERunner;
			On_NPC.CreateBrickBoxForWallOfFlesh += On_NPC_CreateBrickBoxForWallOfFlesh;
        }

		private void On_NPC_CreateBrickBoxForWallOfFlesh(On_NPC.orig_CreateBrickBoxForWallOfFlesh orig, NPC self)
		{
			int num = (int)(self.position.X + (float)(self.width / 2)) / 16;
			int num2 = (int)(self.position.Y + (float)(self.height / 2)) / 16;
			int num3 = self.width / 2 / 16 + 1;
			for (int i = num - num3; i <= num + num3; i++)
			{
				for (int j = num2 - num3; j <= num2 + num3; j++)
				{
					if ((i == num - num3 || i == num + num3 || j == num2 - num3 || j == num2 + num3) && !Main.tile[i, j].HasTile)
					{
						Tile t = Main.tile[i, j];
						ushort ttype = TileID.DemoniteBrick;
						if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Crimson) ttype = TileID.CrimtaneBrick;
						else if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion) ttype = (ushort)ModContent.TileType<BacciliteBrickTile>();

						t.TileType = ttype;
						t.HasTile = true;
					}
					Tile t2 = Main.tile[i, j];
					t2.LiquidType = LiquidID.Water;
					t2.LiquidAmount = 0;
					if (Main.netMode == 2)
						NetMessage.SendTileSquare(-1, i, j);
					else
						WorldGen.SquareTileFrame(i, j);
				}
			}
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
            double num3 = Main.maxTilesX / 4200.0;
            num2 = (int)(num2 * num3);
            num4 = num2;
            val = default(Vector2D);
            val.X = i;
            val.Y = j;
            val2 = default(Vector2D);
            val2.X = WorldGen.genRand.Next(-10, 11) * 0.1;
            val2.Y = WorldGen.genRand.Next(-10, 11) * 0.1;
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
                        if (!(Math.Abs(m - val.X) + Math.Abs(n - val.Y) < num2 * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015)))
                        {
                            continue;
                        }

                        if (Main.tile[m, n].WallType == WallID.GrassUnsafe || Main.tile[m, n].WallType == WallID.FlowerUnsafe ||
                            Main.tile[m, n].WallType == WallID.Grass || Main.tile[m, n].WallType == WallID.Flower ||
                            Main.tile[m, n].WallType == WallID.CorruptGrassUnsafe || Main.tile[m, n].WallType == WallID.CrimsonGrassUnsafe)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionGrassWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.HardenedSand)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<HardenedSnotsandWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.Sandstone)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<SnotsandstoneWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.EbonstoneUnsafe || Main.tile[m, n].WallType == WallID.CrimstoneUnsafe)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe2 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe2 ||
                            Main.tile[m, n].WallType == WallID.RocksUnsafe3 || Main.tile[m, n].WallType == WallID.HallowUnsafe2)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionLumpWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.Cave3Unsafe || Main.tile[m, n].WallType == WallID.CorruptionUnsafe1 ||
                            Main.tile[m, n].WallType == WallID.CrimsonUnsafe1 || Main.tile[m, n].WallType == WallID.RocksUnsafe2 ||
                            Main.tile[m, n].WallType == WallID.HallowUnsafe3)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionMouldWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.Cave4Unsafe || Main.tile[m, n].WallType == WallID.Cave5Unsafe ||
                            Main.tile[m, n].WallType == WallID.CorruptionUnsafe3 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe3 ||
                            Main.tile[m, n].WallType == WallID.RocksUnsafe1 || Main.tile[m, n].WallType == WallID.HallowUnsafe4)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionCystWallUnsafe>();
                        }
                        else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe4 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe4 ||
                            Main.tile[m, n].WallType == WallID.Cave8Unsafe || Main.tile[m, n].WallType == WallID.RocksUnsafe4 ||
                            Main.tile[m, n].WallType == WallID.HallowUnsafe1 || Main.tile[m, n].WallType == WallID.CrimstoneUnsafe)
                        {
                            Main.tile[m, n].WallType = (ushort)ModContent.WallType<ContagionBoilWallUnsafe>();
                        }
                        else if (Data.Sets.WallSets.ConvertsToContagionWall[Main.tile[m, n].WallType] >= 0)
                        {
                            Main.tile[m, n].WallType = (ushort)Data.Sets.WallSets.ConvertsToContagionWall[Main.tile[m, n].WallType];
                            WorldGen.SquareTileFrame(m, n);
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
                        // ?
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
                        else if (TileID.Sets.Conversion.Grass[type] && Main.tile[m, n].TileType != (ushort)ModContent.TileType<Ickgrass>())
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
                        else if (Data.Sets.TileSets.ConvertsToContagion[type] >= 0)
                        {
                            Main.tile[m, n].TileType = (ushort)Data.Sets.TileSets.ConvertsToContagion[Main.tile[m, n].TileType];
                            WorldGen.SquareTileFrame(m, n);
                        }
                    }
                }
                val += val2;
                val2.X += WorldGen.genRand.Next(-10, 11) * 0.05;
                if (val2.X > speedX + 1.0)
                {
                    val2.X = speedX + 1.0;
                }
                if (val2.X < speedX - 1.0)
                {
                    val2.X = speedX - 1.0;
                }
                if (val.X < -num2 || val.Y < -num2 || val.X > Main.maxTilesX + num2 || val.Y > Main.maxTilesY + num2)
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
                        if (!(Math.Abs(m - val.X) + Math.Abs(n - val.Y) < num2 * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015)))
                        {
                            continue;
                        }
                        WorldEvil evil = ModContent.GetInstance<AvalonWorld>().WorldEvil;
                        int type = Main.tile[m, n].TileType;
                        if (good)
                        {
                            #region walls
                            if (Main.tile[m, n].WallType == WallID.GrassUnsafe || Main.tile[m, n].WallType == WallID.FlowerUnsafe ||
                            Main.tile[m, n].WallType == WallID.Grass || Main.tile[m, n].WallType == WallID.Flower ||
                            Main.tile[m, n].WallType == WallID.CorruptGrassUnsafe || Main.tile[m, n].WallType == WallID.CrimsonGrassUnsafe)
                            {
                                Main.tile[m, n].WallType = WallID.HallowedGrassUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.HardenedSand)
                            {
                                Main.tile[m, n].WallType = WallID.HallowHardenedSand;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Sandstone)
                            {
                                Main.tile[m, n].WallType = WallID.HallowSandstone;
                            }
                            else if (Main.tile[m, n].WallType == WallID.EbonstoneUnsafe || Main.tile[m, n].WallType == WallID.CrimstoneUnsafe ||
                                Main.tile[m, n].WallType == ModContent.WallType<ChunkstoneWall>())
                            {
                                Main.tile[m, n].WallType = WallID.PearlstoneBrickUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe2 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe2 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe3 || Main.tile[m, n].WallType == ModContent.WallType<ContagionLumpWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.HallowUnsafe2;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave3Unsafe || Main.tile[m, n].WallType == WallID.CorruptionUnsafe1 ||
                                Main.tile[m, n].WallType == WallID.CrimsonUnsafe1 || Main.tile[m, n].WallType == WallID.RocksUnsafe2 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionMouldWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.HallowUnsafe3;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave4Unsafe || Main.tile[m, n].WallType == WallID.Cave5Unsafe ||
                                Main.tile[m, n].WallType == WallID.CorruptionUnsafe3 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe3 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe1 || Main.tile[m, n].WallType == ModContent.WallType<ContagionCystWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.HallowUnsafe4;
                            }
                            else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe4 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe4 ||
                                Main.tile[m, n].WallType == WallID.Cave8Unsafe || Main.tile[m, n].WallType == WallID.RocksUnsafe4 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionBoilWallUnsafe>() || Main.tile[m, n].WallType == WallID.CrimstoneUnsafe)
                            {
                                Main.tile[m, n].WallType = WallID.HallowUnsafe1;
                            }
                            #endregion

                            #region tiles
                            if (TileID.Sets.IsVine[type])
                            {
                                Main.tile[m, n].TileType = TileID.HallowedVines;
                            }

                            if (flag && Main.tile[m, n].TileType == TileID.Hive && Main.tile[m, n].TileType != TileID.Pearlstone)
                            {
                                Main.tile[m, n].TileType = TileID.Pearlstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (flag && Main.tile[m, n].TileType == TileID.CrispyHoneyBlock && Main.tile[m, n].TileType != TileID.HallowHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.HallowHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }

                            if (TileID.Sets.Conversion.Grass[type] && Main.tile[m, n].TileType != TileID.HallowedGrass)
                            {
                                Main.tile[m, n].TileType = TileID.HallowedGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Stone[type] && Main.tile[m, n].TileType != TileID.Pearlstone)
                            {
                                Main.tile[m, n].TileType = TileID.Pearlstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sand[type] && Main.tile[m, n].TileType != TileID.Pearlsand)
                            {
                                Main.tile[m, n].TileType = TileID.Pearlsand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Ice[type] && Main.tile[m, n].TileType != TileID.HallowedIce)
                            {
                                Main.tile[m, n].TileType = TileID.HallowedIce;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type] && Main.tile[m, n].TileType != TileID.HallowHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.HallowHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type] && Main.tile[m, n].TileType != TileID.HallowSandstone)
                            {
                                Main.tile[m, n].TileType = TileID.HallowSandstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            #endregion
                        }
                        else if (evil == WorldEvil.Crimson)
                        {
                            #region walls
                            if (Main.tile[m, n].WallType == WallID.GrassUnsafe || Main.tile[m, n].WallType == WallID.FlowerUnsafe ||
                            Main.tile[m, n].WallType == WallID.Grass || Main.tile[m, n].WallType == WallID.Flower ||
                            Main.tile[m, n].WallType == WallID.CorruptGrassUnsafe || Main.tile[m, n].WallType == ModContent.WallType<ContagionGrassWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonGrassUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.HardenedSand)
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonHardenedSand;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Sandstone)
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonSandstone;
                            }
                            else if (Main.tile[m, n].WallType == WallID.EbonstoneUnsafe || Main.tile[m, n].WallType == ModContent.WallType<ChunkstoneWall>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimstoneUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe2 || Main.tile[m, n].WallType == WallID.HallowUnsafe2 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe3 || Main.tile[m, n].WallType == ModContent.WallType<ContagionLumpWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonUnsafe2;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave3Unsafe || Main.tile[m, n].WallType == WallID.CorruptionUnsafe1 ||
                                Main.tile[m, n].WallType == WallID.HallowUnsafe3 || Main.tile[m, n].WallType == WallID.RocksUnsafe2 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionMouldWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonUnsafe1;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave4Unsafe || Main.tile[m, n].WallType == WallID.Cave5Unsafe ||
                                Main.tile[m, n].WallType == WallID.CorruptionUnsafe3 || Main.tile[m, n].WallType == WallID.HallowUnsafe4 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe1 || Main.tile[m, n].WallType == ModContent.WallType<ContagionCystWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonUnsafe3;
                            }
                            else if (Main.tile[m, n].WallType == WallID.CorruptionUnsafe4 || Main.tile[m, n].WallType == WallID.HallowUnsafe1 ||
                                Main.tile[m, n].WallType == WallID.Cave8Unsafe || Main.tile[m, n].WallType == WallID.RocksUnsafe4 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionBoilWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CrimsonUnsafe4;
                            }
                            #endregion

                            #region tiles
                            if (TileID.Sets.IsVine[type])
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonVines;
                            }

                            if (flag && Main.tile[m, n].TileType == TileID.Hive && Main.tile[m, n].TileType != TileID.Crimstone)
                            {
                                Main.tile[m, n].TileType = TileID.Crimstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (flag && Main.tile[m, n].TileType == TileID.CrispyHoneyBlock && Main.tile[m, n].TileType != TileID.CrimsonHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }

                            if (TileID.Sets.Conversion.Grass[type] && Main.tile[m, n].TileType != TileID.CrimsonGrass)
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Stone[type] && Main.tile[m, n].TileType != TileID.Crimstone)
                            {
                                Main.tile[m, n].TileType = TileID.Crimstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sand[type] && Main.tile[m, n].TileType != TileID.Crimsand)
                            {
                                Main.tile[m, n].TileType = TileID.Crimsand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Ice[type] && Main.tile[m, n].TileType != TileID.FleshIce)
                            {
                                Main.tile[m, n].TileType = TileID.FleshIce;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type] && Main.tile[m, n].TileType != TileID.CrimsonHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type] && Main.tile[m, n].TileType != TileID.CrimsonSandstone)
                            {
                                Main.tile[m, n].TileType = TileID.CrimsonSandstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            #endregion
                        }
                        else if (evil == WorldEvil.Corruption)
                        {
                            #region walls
                            if (Main.tile[m, n].WallType == WallID.GrassUnsafe || Main.tile[m, n].WallType == WallID.FlowerUnsafe ||
                            Main.tile[m, n].WallType == WallID.Grass || Main.tile[m, n].WallType == WallID.Flower ||
                            Main.tile[m, n].WallType == ModContent.WallType<ContagionGrassWallUnsafe>() || Main.tile[m, n].WallType == WallID.CrimsonGrassUnsafe)
                            {
                                Main.tile[m, n].WallType = WallID.CorruptGrassUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.HardenedSand)
                            {
                                Main.tile[m, n].WallType = WallID.CorruptHardenedSand;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Sandstone)
                            {
                                Main.tile[m, n].WallType = WallID.CorruptSandstone;
                            }
                            else if (Main.tile[m, n].WallType == WallID.CrimstoneUnsafe || Main.tile[m, n].WallType == ModContent.WallType<ChunkstoneWall>())
                            {
                                Main.tile[m, n].WallType = WallID.EbonstoneUnsafe;
                            }
                            else if (Main.tile[m, n].WallType == WallID.HallowUnsafe2 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe2 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe3 || Main.tile[m, n].WallType == ModContent.WallType<ContagionLumpWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CorruptionUnsafe2;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave3Unsafe || Main.tile[m, n].WallType == WallID.HallowUnsafe3 ||
                                Main.tile[m, n].WallType == WallID.CrimsonUnsafe1 || Main.tile[m, n].WallType == WallID.RocksUnsafe2 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionMouldWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CorruptionUnsafe1;
                            }
                            else if (Main.tile[m, n].WallType == WallID.Cave4Unsafe || Main.tile[m, n].WallType == WallID.Cave5Unsafe ||
                                Main.tile[m, n].WallType == WallID.HallowUnsafe4 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe3 ||
                                Main.tile[m, n].WallType == WallID.RocksUnsafe1 || Main.tile[m, n].WallType == ModContent.WallType<ContagionCystWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CorruptionUnsafe3;
                            }
                            else if (Main.tile[m, n].WallType == WallID.HallowUnsafe1 || Main.tile[m, n].WallType == WallID.CrimsonUnsafe4 ||
                                Main.tile[m, n].WallType == WallID.Cave8Unsafe || Main.tile[m, n].WallType == WallID.RocksUnsafe4 ||
                                Main.tile[m, n].WallType == ModContent.WallType<ContagionBoilWallUnsafe>())
                            {
                                Main.tile[m, n].WallType = WallID.CorruptionUnsafe4;
                            }
                            #endregion

                            #region tiles
                            if (TileID.Sets.IsVine[type])
                            {
                                Main.tile[m, n].TileType = TileID.CorruptVines;
                            }

                            if (flag && Main.tile[m, n].TileType == TileID.Hive && Main.tile[m, n].TileType != TileID.Ebonstone)
                            {
                                Main.tile[m, n].TileType = TileID.Ebonstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (flag && Main.tile[m, n].TileType == TileID.CrispyHoneyBlock && Main.tile[m, n].TileType != TileID.CorruptHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.CorruptHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }

                            if (TileID.Sets.Conversion.Grass[type] && Main.tile[m, n].TileType != TileID.CorruptGrass)
                            {
                                Main.tile[m, n].TileType = TileID.CorruptGrass;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Stone[type] && Main.tile[m, n].TileType != TileID.Ebonstone)
                            {
                                Main.tile[m, n].TileType = TileID.Ebonstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sand[type] && Main.tile[m, n].TileType != TileID.Ebonsand)
                            {
                                Main.tile[m, n].TileType = TileID.Ebonsand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Ice[type] && Main.tile[m, n].TileType != TileID.CorruptIce)
                            {
                                Main.tile[m, n].TileType = TileID.CorruptIce;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type] && Main.tile[m, n].TileType != TileID.CorruptHardenedSand)
                            {
                                Main.tile[m, n].TileType = TileID.CorruptHardenedSand;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type] && Main.tile[m, n].TileType != TileID.CorruptSandstone)
                            {
                                Main.tile[m, n].TileType = TileID.CorruptSandstone;
                                WorldGen.SquareTileFrame(m, n);
                            }
                            #endregion
                        }
                    }
                }
                val += val2;
                val2.X += WorldGen.genRand.Next(-10, 11) * 0.05;
                if (val2.X > speedX + 1.0)
                {
                    val2.X = speedX + 1.0;
                }
                if (val2.X < speedX - 1.0)
                {
                    val2.X = speedX - 1.0;
                }
                if (val.X < -num2 || val.Y < -num2 || val.X > Main.maxTilesX + num2 || val.Y > Main.maxTilesY + num2)
                {
                    flag2 = false;
                }
            }
        }
    }
}
