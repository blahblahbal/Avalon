using Avalon.Common;
using Avalon.Tiles.Ores;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using MonoMod.Cil;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks
{
    internal class GemCaves : ModHook
    {
        private static bool[] Gem = new bool[9];
        protected override void Apply()
        {
            On_WorldGen.randGem += OnRandGem;
            On_WorldGen.randGemTile += OnRandGemTile;
            On_WorldGen.Spread.Gem += On_Spread_Gem;
            On_WorldGen.gemCave += OnGemCave;
        }
        private static void OnGemCave(On_WorldGen.orig_gemCave orig, int x, int y)
        {
            WorldGen.countTiles(x, y);
            for (int i = 0; i < 9; i++)
            {
                Gem[i] = false;
            }
            Gem[WorldGen.genRand.Next(9)] = true;
            for (int j = 0; j < 9; j++)
            {
                if (WorldGen.genRand.NextBool(9))
                {
                    Gem[j] = true;
                }
            }
            WorldGen.Spread.Gem(x, y);
        }
        private static int OnRandGem(On_WorldGen.orig_randGem orig)
        {
            int num = WorldGen.genRand.Next(9);
            while (!Gem[num])
            {
                num = WorldGen.genRand.Next(9);
            }
            return num;
        }
        private static ushort OnRandGemTile(On_WorldGen.orig_randGemTile orig)
        {
            if (!WorldGen.genRand.NextBool(20))
            {
                return 1;
            }
            return (ushort)WorldGen.randGem() switch
            {
                0 => 67,
                1 => 66,
                2 => 63,
                3 => 65,
                4 => 64,
                5 => 68,
                6 => (ushort)ModContent.TileType<Tourmaline>(),
                7 => (ushort)ModContent.TileType<Peridot>(),
                _ => (ushort)ModContent.TileType<Zircon>()
            };
        }
        private static void On_Spread_Gem(On_WorldGen.Spread.orig_Gem orig, int x, int y)
        {
            if (!WorldGen.InWorld(x, y))
            {
                return;
            }
            List<Point> list = new List<Point>();
            List<Point> list2 = new List<Point>();
            HashSet<Point> hashSet = new HashSet<Point>();
            list2.Add(new Point(x, y));
            Point item2 = default(Point);
            while (list2.Count > 0)
            {
                list.Clear();
                list.AddRange(list2);
                list2.Clear();
                while (list.Count > 0)
                {
                    Point item = list[0];
                    if (!WorldGen.InWorld(item.X, item.Y, 1))
                    {
                        list.Remove(item);
                        continue;
                    }
                    hashSet.Add(item);
                    list.Remove(item);
                    Tile tile = Main.tile[item.X, item.Y];
                    if (WorldGen.SolidTile(item.X, item.Y) || tile.WallType != 0)
                    {
                        if (tile.HasTile)
                        {
                            if (Gemmable(tile.TileType))
                            {
                                tile.TileType = WorldGen.randGemTile();
                            }
                            Tile tile2 = Main.tile[item.X - 1, item.Y];
                            if (Gemmable(tile2.TileType))
                            {
                                tile2.TileType = WorldGen.randGemTile();
                            }
                            tile2 = Main.tile[item.X + 1, item.Y];
                            if (Gemmable(tile2.TileType))
                            {
                                tile2.TileType = WorldGen.randGemTile();
                            }
                            tile2 = Main.tile[item.X, item.Y - 1];
                            if (Gemmable(tile2.TileType))
                            {
                                tile2.TileType = WorldGen.randGemTile();
                            }
                            tile2 = Main.tile[item.X, item.Y + 1];
                            if (Gemmable(tile2.TileType))
                            {
                                tile2.TileType = WorldGen.randGemTile();
                            }
                        }
                    }
                    else
                    {
                        int g = WorldGen.randGem();
                        if (g < 6)
                        {
                            tile.WallType = (ushort)(48 + g);
                            if (!tile.HasTile && WorldGen.genRand.NextBool(2))
                            {
                                WorldGen.PlaceTile(item.X, item.Y, 178, true, false, -1, g);
                            }
                        }
                        else
                        {
                            if (g == 6)
                            {
                                tile.WallType = (ushort)ModContent.WallType<Walls.TourmalineStoneWallUnsafe>();
                                if (!tile.HasTile && WorldGen.genRand.NextBool(2))
                                {
                                    WorldGen.PlaceTile(item.X, item.Y, ModContent.TileType<Tiles.PlacedGems>(), true, false, -1, 3);
                                }
                            }
                            if (g == 7)
                            {
                                tile.WallType = (ushort)ModContent.WallType<Walls.PeridotStoneWallUnsafe>();
                                if (!tile.HasTile && WorldGen.genRand.NextBool(2))
                                {
                                    WorldGen.PlaceTile(item.X, item.Y, ModContent.TileType<Tiles.PlacedGems>(), true, false, -1, 4);
                                }
                            }
                            if (g == 8)
                            {
                                tile.WallType = (ushort)ModContent.WallType<Walls.ZirconStoneWallUnsafe>();
                                if (!tile.HasTile && WorldGen.genRand.NextBool(2))
                                {
                                    WorldGen.PlaceTile(item.X, item.Y, ModContent.TileType<Tiles.PlacedGems>(), true, false, -1, 5);
                                }
                            }
                        }
                        
                        item2 = new Point(item.X - 1, item.Y);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new Point(item.X + 1, item.Y);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new Point(item.X, item.Y - 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                        item2 = new Point(item.X, item.Y + 1);
                        if (!hashSet.Contains(item2))
                        {
                            list2.Add(item2);
                        }
                    }
                }
            }
        }

        private static bool Gemmable(int type)
        {
            if (type != 0 && type != 1 && type != 40 && type != 59 && type != 60 && type != 70 && type != 147)
            {
                return type == 161;
            }
            return true;
        }
        //private static void IL_GemCave(ILContext il)
        //{
        //    var c = new ILCursor(il);

        //    c.GotoNext(instruction => instruction.MatchCall<WorldGen>(nameof(WorldGen.PlaceTile)));
        //    c.GotoNext(MoveType.After, instruction => instruction.MatchBrfalse(out _));



        //    c.EmitDelegate(() =>
        //    {
        //        int g = WorldGen.randGem();
        //        int tile = TileID.ExposedGems;
        //        if (g > 5)
        //        {
        //            tile = ModContent.TileType<Tiles.PlacedGems>();
        //        }
        //        WorldGen.PlaceTile()
        //    });
        //}
    }
}
