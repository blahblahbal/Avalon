using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Walls;
using Avalon.Tiles.Contagion;

namespace Avalon.Hooks
{
    internal class ContagionSpread : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.hardUpdateWorld += On_WorldGen_hardUpdateWorld; //This contaions tile spreading
            On_WorldGen.UpdateWorld_OvergroundTile += On_WorldGen_UpdateWorld_OvergroundTile; //these contain wall spreading
            On_WorldGen.UpdateWorld_UndergroundTile += On_WorldGen_UpdateWorld_UndergroundTile;
            On_WorldGen.SpreadDesertWalls += On_WorldGen_SpreadDesertWalls; //this contains desert wall spreading
            IL_WorldGen.GrowCactus += IL_WorldGen_GrowCactus;
            IL_WorldGen.StonePatch += IL_WorldGen_StonePatch;
        }

        public override void Unload()
        {
            On_WorldGen.hardUpdateWorld -= On_WorldGen_hardUpdateWorld;
            On_WorldGen.UpdateWorld_OvergroundTile -= On_WorldGen_UpdateWorld_OvergroundTile;
            On_WorldGen.UpdateWorld_UndergroundTile -= On_WorldGen_UpdateWorld_UndergroundTile;
            On_WorldGen.SpreadDesertWalls -= On_WorldGen_SpreadDesertWalls;
        }

        //Make sure that once the jungle grass is added that it is given to all the checks for tiles

        private void IL_WorldGen_GrowCactus(MonoMod.Cil.ILContext il)
        {
            Utilities.AddAlternativeIdChecks(il, TileID.Crimsand, id => TileID.Sets.Factory.CreateBoolSet(ModContent.TileType<Snotsand>())[id]);
        }
        private void IL_WorldGen_StonePatch(MonoMod.Cil.ILContext il)
        {
            Utilities.AddAlternativeIdChecks(il, TileID.CrimsonGrass, id => TileID.Sets.Factory.CreateBoolSet(ModContent.TileType<Ickgrass>())[id]);
        }
        private void On_WorldGen_SpreadDesertWalls(On_WorldGen.orig_SpreadDesertWalls orig, int wallDist, int i, int j)
        {
            orig.Invoke(wallDist, i, j);
            if (WorldGen.InWorld(i, j, 10) || (WallID.Sets.Conversion.Sandstone[Main.tile[i, j].WallType] && (Main.tile[i, j].HasTile || TileID.Sets.Conversion.Sandstone[Main.tile[i, j].TileType]) && WallID.Sets.Conversion.HardenedSand[Main.tile[i, j].WallType]))
            {
                bool num = false;
                int wall = Main.tile[i, j].WallType;
                int type = Main.tile[i, j].TileType;
                if ((wall == ModContent.WallType<ChunkstoneWall>() || wall == ModContent.WallType<ContagionLumpWallUnsafe>() || wall == ModContent.WallType<ContagionMouldWallUnsafe>() || wall == ModContent.WallType<ContagionBoilWallUnsafe>() || wall == ModContent.WallType<ContagionCystWallUnsafe>() || wall == ModContent.WallType<ContagionGrassWall>() || wall == ModContent.WallType<HardenedSnotsandWallUnsafe>() || wall == ModContent.WallType<SnotsandstoneWallUnsafe>()) || (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>() || type == ModContent.TileType<ContagionShortGrass>() || type == ModContent.TileType<ContagionVines>() || type == ModContent.TileType<Snotsand>() || type == ModContent.TileType<Chunkstone>() || type == ModContent.TileType<YellowIce>() || type == ModContent.TileType<HardenedSnotsand>() || type == ModContent.TileType<Snotsandstone>()))
                {
                    num = true;
                }
                if (num == false)
                {
                    return;
                }
                int num2 = i + WorldGen.genRand.Next(-2, 3);
                int num3 = j + WorldGen.genRand.Next(-2, 3);
                bool flag = false;
                if (WallID.Sets.Conversion.PureSand[Main.tile[num2, num3].WallType])
                {
                    if (num == true)
                    {
                        for (int num4 = i - wallDist; num4 < i + wallDist; num4++)
                        {
                            for (int num5 = j - wallDist; num5 < j + wallDist; num5++)
                            {
                                int type2 = Main.tile[num4, num5].TileType;
                                if (Main.tile[num4, num5].HasTile && (type2 == ModContent.TileType<Ickgrass>() || type2 == ModContent.TileType<ContagionJungleGrass>() || type2 == ModContent.TileType<ContagionShortGrass>() || type2 == ModContent.TileType<ContagionVines>() || type2 == ModContent.TileType<Snotsand>() || type2 == ModContent.TileType<Chunkstone>() || type2 == ModContent.TileType<YellowIce>() || type2 == ModContent.TileType<HardenedSnotsand>() || type2 == ModContent.TileType<Snotsandstone>()))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                break;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    return;
                }
                ushort? num6 = null;
                if (WallID.Sets.Conversion.Sandstone[Main.tile[num2, num3].WallType])
                {
                    if (num == true)
                    {
                        num6 = (ushort)ModContent.WallType<SnotsandstoneWallUnsafe>();
                    }
                }
                if (WallID.Sets.Conversion.HardenedSand[Main.tile[num2, num3].WallType])
                {
                    if (num == true)
                    {
                        num6 = (ushort)ModContent.WallType<HardenedSnotsandWallUnsafe>();
                    }
                }
                if (num6.HasValue && Main.tile[num2, num3].WallType != num6.Value)
                {
                    Main.tile[num2, num3].WallType = num6.Value;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, num2, num3);
                    }
                }
            }
        }

        private void On_WorldGen_UpdateWorld_OvergroundTile(On_WorldGen.orig_UpdateWorld_OvergroundTile orig, int i, int j, bool checkNPCSpawns, int wallDist)
        {
            orig.Invoke(i, j, checkNPCSpawns, wallDist);
            if (WorldGen.AllowedToSpreadInfections)
            {
                if (Main.tile[i, j].WallType == ModContent.WallType<ContagionGrassWall>() || ((Main.tile[i, j].TileType == ModContent.TileType<Ickgrass>() || Main.tile[i, j].TileType == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[i, j].HasTile))
                {
                    int num30 = i + WorldGen.genRand.Next(-2, 3);
                    int num31 = j + WorldGen.genRand.Next(-2, 3);
                    if ((WorldGen.InWorld(num30, num31, 10) && Main.tile[num30, num31].WallType == 63) || Main.tile[num30, num31].WallType == 65 || Main.tile[num30, num31].WallType == 66 || Main.tile[num30, num31].WallType == 68)
                    {
                        bool flag4 = false;
                        for (int num32 = i - wallDist; num32 < i + wallDist; num32++)
                        {
                            for (int num33 = j - wallDist; num33 < j + wallDist; num33++)
                            {
                                if (Main.tile[num32, num33].HasTile)
                                {
                                    int type6 = Main.tile[num32, num33].TileType;
                                    if (type6 == ModContent.TileType<Ickgrass>() || type6 == ModContent.TileType<ContagionJungleGrass>() || type6 == ModContent.TileType<ContagionShortGrass>() || type6 == ModContent.TileType<ContagionVines>() || type6 == ModContent.TileType<Snotsand>() || type6 == ModContent.TileType<Chunkstone>() || type6 == ModContent.TileType<YellowIce>() || type6 == ModContent.TileType<HardenedSnotsand>() || type6 == ModContent.TileType<Snotsandstone>())
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag4)
                        {
                            Main.tile[num30, num31].WallType = (ushort)ModContent.WallType<ContagionGrassWall>();
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, num30, num31);
                            }
                        }
                    }
                }
            }
        }

        private void On_WorldGen_UpdateWorld_UndergroundTile(On_WorldGen.orig_UpdateWorld_UndergroundTile orig, int i, int j, bool checkNPCSpawns, int wallDist)
        {
            orig.Invoke(i, j, checkNPCSpawns, wallDist);
            if (WorldGen.AllowedToSpreadInfections)
            {
                if (Main.tile[i, j].WallType == ModContent.WallType<ContagionGrassWall>() || ((Main.tile[i, j].TileType == ModContent.TileType<Ickgrass>() || Main.tile[i, j].TileType == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[i, j].HasTile))
                {
                    int num30 = i + WorldGen.genRand.Next(-2, 3);
                    int num31 = j + WorldGen.genRand.Next(-2, 3);
                    if ((WorldGen.InWorld(num30, num31, 10) && Main.tile[num30, num31].WallType == 63) || Main.tile[num30, num31].WallType == 65 || Main.tile[num30, num31].WallType == 66 || Main.tile[num30, num31].WallType == 68)
                    {
                        bool flag4 = false;
                        for (int num32 = i - wallDist; num32 < i + wallDist; num32++)
                        {
                            for (int num33 = j - wallDist; num33 < j + wallDist; num33++)
                            {
                                if (Main.tile[num32, num33].HasTile)
                                {
                                    int type6 = Main.tile[num32, num33].TileType;
                                    if (type6 == ModContent.TileType<Ickgrass>() || type6 == ModContent.TileType<ContagionJungleGrass>() || type6 == ModContent.TileType<ContagionShortGrass>() || type6 == ModContent.TileType<ContagionVines>() || type6 == ModContent.TileType<Snotsand>() || type6 == ModContent.TileType<Chunkstone>() || type6 == ModContent.TileType<YellowIce>() || type6 == ModContent.TileType<HardenedSnotsand>() || type6 == ModContent.TileType<Snotsandstone>())
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag4)
                        {
                            Main.tile[num30, num31].WallType = (ushort)ModContent.WallType<ContagionGrassWall>();
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendTileSquare(-1, num30, num31);
                            }
                        }
                    }
                }
            }
        }

        private void On_WorldGen_hardUpdateWorld(On_WorldGen.orig_hardUpdateWorld orig, int i, int j)
        {
            orig.Invoke(i, j);
            if (Main.hardMode)
            {
                int type = Main.tile[i, j].TileType;
                if ((NPC.downedPlantBoss && WorldGen.genRand.NextBool(2)) || WorldGen.AllowedToSpreadInfections)
                {
                    if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>() || type == ModContent.TileType<ContagionShortGrass>() || type == ModContent.TileType<ContagionVines>() || type == ModContent.TileType<Snotsand>() || type == ModContent.TileType<Chunkstone>() || type == ModContent.TileType<YellowIce>() || type == ModContent.TileType<HardenedSnotsand>() || type == ModContent.TileType<Snotsandstone>())
                    {
                        bool flag4 = true;
                        while (flag4)
                        {
                            flag4 = false;
                            int num15 = i + WorldGen.genRand.Next(-3, 4);
                            int num16 = j + WorldGen.genRand.Next(-3, 4);
                            if (!WorldGen.InWorld(num15, num16, 10) || WorldGen.CountNearBlocksTypes(num15, num16, 2, 1, 27) > 0)
                            {
                                continue;
                            }
                            if (Main.tile[num15, num16].TileType == 2)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<Ickgrass>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            if (Main.tile[num15, num16].TileType == 60)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            //if (Main.tile[num15, num16].TileType == 59)
                            //{
                            //    if (WorldGen.genRand.Next(2) == 0)
                            //    {
                            //        flag4 = true;
                            //    }
                            //    Main.tile[num15, num16].TileType = TileID.Dirt;
                            //    WorldGen.SquareTileFrame(num15, num16);
                            //    NetMessage.SendTileSquare(-1, num15, num16);
                            //}
                            else if (Main.tile[num15, num16].TileType == 1 || Main.tileMoss[Main.tile[num15, num16].TileType])
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<Chunkstone>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            else if (Main.tile[num15, num16].TileType == 53)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<Snotsand>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            else if (Main.tile[num15, num16].TileType == 396)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            else if (Main.tile[num15, num16].TileType == 397)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                            else if (Main.tile[num15, num16].TileType == 161)
                            {
                                if (WorldGen.genRand.NextBool(2))
                                {
                                    flag4 = true;
                                }
                                Main.tile[num15, num16].TileType = (ushort)ModContent.TileType<YellowIce>();
                                WorldGen.SquareTileFrame(num15, num16);
                                NetMessage.SendTileSquare(-1, num15, num16);
                            }
                        }
                    }
                }
            }
        }
    }
}
