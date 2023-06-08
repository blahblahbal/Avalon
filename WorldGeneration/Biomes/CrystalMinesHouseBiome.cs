// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// Terraria.GameContent.Biomes.CaveHouseBiome
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Biomes;

public class CrystalMinesHouseBiome : MicroBiome
{
    private class BuildData
    {
        public delegate void ProcessRoomMethod(Rectangle room);

        public static BuildData Crystal = CreateCrystalData();

        public static BuildData Default = CreateDefaultData();

        public ushort Tile;

        public byte Wall;

        public int PlatformStyle;

        public int DoorStyle;

        public int TableStyle;

        public int WorkbenchStyle;

        public int PianoStyle;

        public int BookcaseStyle;

        public int ChairStyle;

        public int ChestStyle;

        public ProcessRoomMethod ProcessRoom;

        public static BuildData CreateCrystalData()
        {
            return new BuildData
            {
                Tile = (ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>(),
                Wall = (byte)WallID.Crystal,
                DoorStyle = 36,
                PlatformStyle = 30,
                TableStyle = 0, // tile 469
                WorkbenchStyle = 31,
                PianoStyle = 30,
                BookcaseStyle = 32,
                ChairStyle = 36,
                ChestStyle = 0, // tile 467
                ProcessRoom = AgeCrystalRoom
            };
        }

        public static BuildData CreateDefaultData()
        {
            return new BuildData
            {
                Tile = 30,
                Wall = 27,
                PlatformStyle = 0,
                DoorStyle = 0,
                TableStyle = 0,
                WorkbenchStyle = 0,
                PianoStyle = 0,
                BookcaseStyle = 0,
                ChairStyle = 0,
                ChestStyle = 1,
                ProcessRoom = AgeDefaultRoom
            };
        }
    }

    private const int VERTICAL_EXIT_WIDTH = 3;

    public static bool[] _blacklistedTiles = TileID.Sets.Factory.CreateBoolSet(true, 225, 41, 43, 44, 226, 203, 112, 25, 151);
    public static int[] blacklistedWalls =
    {
        WallID.BlueDungeonSlabUnsafe,
        WallID.BlueDungeonTileUnsafe,
        WallID.BlueDungeonUnsafe,
        WallID.GreenDungeonSlabUnsafe,
        WallID.GreenDungeonTileUnsafe,
        WallID.GreenDungeonUnsafe,
        WallID.PinkDungeonSlabUnsafe,
        WallID.PinkDungeonTileUnsafe,
        WallID.PinkDungeonUnsafe,
        WallID.LihzahrdBrickUnsafe,
        //ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>(),
        ModContent.WallType<Walls.OrangeBrickUnsafe>(),
        ModContent.WallType<Walls.OrangeTiledUnsafe>(),
        ModContent.WallType<Walls.OrangeSlabUnsafe>()
    };

    private Rectangle GetRoom(Point origin)
    {
        Point result;
        bool flag = WorldUtils.Find(origin, Searches.Chain(new Searches.Left(25), new Conditions.IsSolid()), out result);
        Point result2;
        bool num = WorldUtils.Find(origin, Searches.Chain(new Searches.Right(25), new Conditions.IsSolid()), out result2);
        if (!flag)
        {
            result = new Point(origin.X - 25, origin.Y);
        }
        if (!num)
        {
            result2 = new Point(origin.X + 25, origin.Y);
        }
        Rectangle result3 = new Rectangle(origin.X, origin.Y, 0, 0);
        if (origin.X - result.X > result2.X - origin.X)
        {
            result3.X = result.X;
            result3.Width = Terraria.Utils.Clamp(result2.X - result.X, 15, 30);
        }
        else
        {
            result3.Width = Terraria.Utils.Clamp(result2.X - result.X, 15, 30);
            result3.X = result2.X - result3.Width;
        }
        Point result4;
        bool flag2 = WorldUtils.Find(result, Searches.Chain(new Searches.Up(10), new Conditions.IsSolid()), out result4);
        Point result5;
        bool num2 = WorldUtils.Find(result2, Searches.Chain(new Searches.Up(10), new Conditions.IsSolid()), out result5);
        if (!flag2)
        {
            result4 = new Point(origin.X, origin.Y - 10);
        }
        if (!num2)
        {
            result5 = new Point(origin.X, origin.Y - 10);
        }
        result3.Height = Terraria.Utils.Clamp(Math.Max(origin.Y - result4.Y, origin.Y - result5.Y), 8, 12);
        result3.Y -= result3.Height;
        return result3;
    }

    private float RoomSolidPrecentage(Rectangle room)
    {
        float num = room.Width * room.Height;
        Ref<int> @ref = new Ref<int>(0);
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.IsSolid(), new Actions.Count(@ref)));
        return (float)@ref.Value / num;
    }

    private bool FindVerticalExit(Rectangle wall, bool isUp, out int exitX)
    {
        Point result;
        bool result2 = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? (-5) : 0)), Searches.Chain(new Searches.Left(wall.Width - 3), new Conditions.IsSolid().Not().AreaOr(3, 5)), out result);
        exitX = result.X;
        return result2;
    }

    private bool FindSideExit(Rectangle wall, bool isLeft, out int exitY)
    {
        Point result;
        bool result2 = WorldUtils.Find(new Point(wall.X + (isLeft ? (-4) : 0), wall.Y + wall.Height - 3), Searches.Chain(new Searches.Up(wall.Height - 3), new Conditions.IsSolid().Not().AreaOr(4, 3)), out result);
        exitY = result.Y;
        return result2;
    }

    public static bool AddCrystalChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
    {
        int k = j;
        while (k < Main.maxTilesY)
        {
            if (Main.tile[i, k].HasTile && Main.tileSolid[Main.tile[i, k].TileType])
            {
                int num = k;
                int num2 = WorldGen.PlaceChest(i - 1, num - 1, (ushort)ModContent.TileType<Tiles.CrystalMines.CrystalMinesChest>(), notNearOtherChests, 1);
                if (num2 >= 0)
                {
                    int num3 = 0;
                    while (num3 == 0)
                    {
                        int r = Main.rand.Next(2);
                        //if (r == 0)
                        //{
                        //    Main.chest[num2].item[0].SetDefaults(ModContent.ItemType<Items.Weapons.Melee.ShatterLance>(), false);
                        //}
                        //else if (r == 1)
                        //{
                        //    Main.chest[num2].item[0].SetDefaults(ModContent.ItemType<Items.Weapons.Magic.SacredLyre>(), false);
                        //}
                        Main.chest[num2].item[0].Prefix(-1);
                        Main.chest[num2].item[1].SetDefaults(ModContent.ItemType<Items.Placeable.Tile.CrystalStoneBlock>(), false);
                        Main.chest[num2].item[1].stack = Main.rand.Next(200, 301);
                        int n2 = WorldGen.genRand.Next(5);
                        if (n2 == 0)
                        {
                            //Main.chest[num2].item[2].SetDefaults(ModContent.ItemType<Items.Potions.SupersonicPotion>(), false);
                            //Main.chest[num2].item[2].stack = WorldGen.genRand.Next(2, 4);
                        }
                        if (n2 == 1)
                        {
                            //Main.chest[num2].item[2].SetDefaults(ModContent.ItemType<Items.Potions.CloverPotion>(), false);
                            //Main.chest[num2].item[2].stack = WorldGen.genRand.Next(2, 4);
                        }
                        if (n2 == 2)
                        {
                            //Main.chest[num2].item[2].SetDefaults(ModContent.ItemType<Items.Potions.WisdomPotion>(), false);
                            //Main.chest[num2].item[2].stack = WorldGen.genRand.Next(2, 4);
                        }
                        if (n2 == 3)
                        {
                            //Main.chest[num2].item[2].SetDefaults(ModContent.ItemType<Items.Potions.RoguePotion>(), false);
                            //Main.chest[num2].item[2].stack = WorldGen.genRand.Next(2, 4);
                        }
                        if (n2 == 4)
                        {
                            //Main.chest[num2].item[2].SetDefaults(ModContent.ItemType<Items.Potions.GauntletPotion>(), false);
                            //Main.chest[num2].item[2].stack = WorldGen.genRand.Next(2, 4);
                        }
                        if (WorldGen.genRand.Next(8) == 0)
                        {
                            //Main.chest[num2].item[3].SetDefaults(ModContent.ItemType<Items.Consumables.CrystalFruit>(), false);
                            //Main.chest[num2].item[4].SetDefaults(ItemID.PlatinumCoin, false);
                            //Main.chest[num2].item[4].stack = WorldGen.genRand.Next(1, 5);
                        }
                        else
                        {
                            Main.chest[num2].item[3].SetDefaults(ItemID.PlatinumCoin, false);
                            Main.chest[num2].item[3].stack = WorldGen.genRand.Next(1, 5);
                        }
                        num3++;
                    }
                    return true;
                }
                return false;
            }
            else
            {
                k++;
            }
        }
        return false;
    }

    private int SortBiomeResults(Tuple<BuildData, int> item1, Tuple<BuildData, int> item2)
    {
        return item2.Item2.CompareTo(item1.Item2);
    }

    public override bool Place(Point origin, StructureMap structures)
    {
        if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(200), new Conditions.IsSolid()), out var result) || result == origin)
        {
            return false;
        }
        Rectangle room = GetRoom(result);
        Rectangle rectangle = GetRoom(new Point(room.Center.X, room.Y + 1));
        Rectangle rectangle2 = GetRoom(new Point(room.Center.X, room.Y + room.Height + 10));
        rectangle2.Y = room.Y + room.Height - 1;
        float num = RoomSolidPrecentage(rectangle);
        float num13 = RoomSolidPrecentage(rectangle2);
        room.Y += 3;
        rectangle.Y += 3;
        rectangle2.Y += 3;
        List<Rectangle> list = new List<Rectangle>();
        if (_random.NextFloat() > num + 0.2f)
        {
            list.Add(rectangle);
        }
        else
        {
            rectangle = room;
        }
        list.Add(room);
        if (_random.NextFloat() > num13 + 0.2f)
        {
            list.Add(rectangle2);
        }
        else
        {
            rectangle2 = room;
        }
        foreach (Rectangle item12 in list)
        {
            if (item12.Y + item12.Height > Main.maxTilesY - 220)
            {
                return false;
            }
        }
        Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
        foreach (Rectangle item13 in list)
        {
            WorldUtils.Gen(new Point(item13.X - 10, item13.Y - 10), new Shapes.Rectangle(item13.Width + 20, item13.Height + 20), new Actions.TileScanner(0, TileID.CrystalBlock, (ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>()).Output(dictionary));
        }
        List<Tuple<BuildData, int>> list6 = new List<Tuple<BuildData, int>>();
        list6.Add(Tuple.Create(BuildData.Crystal, dictionary[(ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>()] + dictionary[TileID.CrystalBlock]));
        list6.Sort(SortBiomeResults);
        BuildData item = list6[0].Item1;
        //foreach (Rectangle item14 in list)
        //{
        //    if (!structures.CanPlace(item14, _blacklistedTiles, 5))
        //    {
        //        return false;
        //    }
        //}

        for (int left = room.X; left < room.X + room.Width - 1; left++)
        {
            for (int top = room.Y; top < room.Y + room.Height - 1; top++)
            {
                if (blacklistedWalls.Contains(Main.tile[left, top].WallType))
                {
                    return false;
                }
            }
        }
        int num24 = room.X;
        int num33 = room.X + room.Width - 1;
        List<Rectangle> list2 = new List<Rectangle>();
        foreach (Rectangle item15 in list)
        {
            num24 = Math.Min(num24, item15.X);
            num33 = Math.Max(num33, item15.X + item15.Width - 1);
        }
        int num34 = 6;
        while (num34 > 4 && (num33 - num24) % num34 != 0)
        {
            num34--;
        }
        for (int i = num24; i <= num33; i += num34)
        {
            for (int j = 0; j < list.Count; j++)
            {
                Rectangle rectangle3 = list[j];
                if (i < rectangle3.X || i >= rectangle3.X + rectangle3.Width)
                {
                    continue;
                }
                int num35 = rectangle3.Y + rectangle3.Height;
                int num36 = 50;
                for (int k = j + 1; k < list.Count; k++)
                {
                    if (i >= list[k].X && i < list[k].X + list[k].Width)
                    {
                        num36 = Math.Min(num36, list[k].Y - num35);
                    }
                }
                if (num36 > 0)
                {
                    Point result2;
                    bool flag = WorldUtils.Find(new Point(i, num35), Searches.Chain(new Searches.Down(num36), new Conditions.IsSolid()), out result2);
                    if (num36 < 50)
                    {
                        flag = true;
                        result2 = new Point(i, num35 + num36);
                    }
                    if (flag)
                    {
                        list2.Add(new Rectangle(i, num35, 1, result2.Y - num35));
                    }
                }
            }
        }
        List<Point> list3 = new List<Point>();
        foreach (Rectangle item16 in list)
        {
            if (FindSideExit(new Rectangle(item16.X + item16.Width, item16.Y + 1, 1, item16.Height - 2), isLeft: false, out var exitY))
            {
                list3.Add(new Point(item16.X + item16.Width - 1, exitY));
            }
            if (FindSideExit(new Rectangle(item16.X, item16.Y + 1, 1, item16.Height - 2), isLeft: true, out exitY))
            {
                list3.Add(new Point(item16.X, exitY));
            }
        }
        List<Tuple<Point, Point>> list4 = new List<Tuple<Point, Point>>();
        for (int l = 1; l < list.Count; l++)
        {
            Rectangle rectangle4 = list[l];
            Rectangle rectangle5 = list[l - 1];
            int num38 = rectangle5.X - rectangle4.X;
            int num37 = rectangle4.X + rectangle4.Width - (rectangle5.X + rectangle5.Width);
            if (num38 > num37)
            {
                list4.Add(new Tuple<Point, Point>(new Point(rectangle4.X + rectangle4.Width - 1, rectangle4.Y + 1), new Point(rectangle4.X + rectangle4.Width - rectangle4.Height + 1, rectangle4.Y + rectangle4.Height - 1)));
            }
            else
            {
                list4.Add(new Tuple<Point, Point>(new Point(rectangle4.X, rectangle4.Y + 1), new Point(rectangle4.X + rectangle4.Height - 1, rectangle4.Y + rectangle4.Height - 1)));
            }
        }
        List<Point> list5 = new List<Point>();
        if (FindVerticalExit(new Rectangle(rectangle.X + 2, rectangle.Y, rectangle.Width - 4, 1), isUp: true, out var exitX))
        {
            list5.Add(new Point(exitX, rectangle.Y));
        }
        if (FindVerticalExit(new Rectangle(rectangle2.X + 2, rectangle2.Y + rectangle2.Height - 1, rectangle2.Width - 4, 1), isUp: false, out exitX))
        {
            list5.Add(new Point(exitX, rectangle2.Y + rectangle2.Height - 1));
        }
        foreach (Rectangle item17 in list)
        {
            WorldUtils.Gen(new Point(item17.X, item17.Y), new Shapes.Rectangle(item17.Width, item17.Height), Actions.Chain(new Actions.SetTile(item.Tile), new Actions.SetFrames(frameNeighbors: true)));
            WorldUtils.Gen(new Point(item17.X + 1, item17.Y + 1), new Shapes.Rectangle(item17.Width - 2, item17.Height - 2), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(item.Wall)));
        }
        foreach (Tuple<Point, Point> item18 in list4)
        {
            Point item10 = item18.Item1;
            Point item11 = item18.Item2;
            int num2 = ((item11.X > item10.X) ? 1 : (-1));
            ShapeData shapeData = new ShapeData();
            for (int m = 0; m < item11.Y - item10.Y; m++)
            {
                shapeData.Add(num2 * (m + 1), m);
            }
            WorldUtils.Gen(item10, new ModShapes.All(shapeData), Actions.Chain(new Actions.PlaceTile(19, item.PlatformStyle), new Actions.SetSlope((num2 == 1) ? 1 : 2), new Actions.SetFrames(frameNeighbors: true)));
            WorldUtils.Gen(new Point(item10.X + ((num2 == 1) ? 1 : (-4)), item10.Y - 1), new Shapes.Rectangle(4, 1), Actions.Chain(new Actions.Clear(), new Actions.PlaceWall(item.Wall), new Actions.PlaceTile(19, item.PlatformStyle), new Actions.SetFrames(frameNeighbors: true)));
        }
        foreach (Point item2 in list3)
        {
            WorldUtils.Gen(item2, new Shapes.Rectangle(1, 3), new Actions.ClearTile(frameNeighbors: true));
            WorldGen.PlaceTile(item2.X, item2.Y, ModContent.TileType<Tiles.CrystalMines.ClosedCrystalDoor>(), true, true, -1, 0);
            WorldGeneration.Utils.SquareTileFrameArea(item2.X, item2.Y, 2);
        }
        foreach (Point item19 in list5)
        {
            WorldUtils.Gen(item19, new Shapes.Rectangle(3, 1), Actions.Chain(new Actions.ClearMetadata(), new Actions.PlaceTile(19, item.PlatformStyle), new Actions.SetFrames(frameNeighbors: true)));
        }
        foreach (Rectangle item3 in list2)
        {
            if (item3.Height > 1 && _tiles[item3.X, item3.Y - 1].TileType != 19)
            {
                WorldUtils.Gen(new Point(item3.X, item3.Y), new Shapes.Rectangle(item3.Width, item3.Height), Actions.Chain(new Actions.SetTile((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalColumn>()), new Actions.SetFrames(frameNeighbors: true)));
                Tile tile = _tiles[item3.X, item3.Y + item3.Height];
                tile.Slope = SlopeType.Solid;
                tile.IsHalfBlock = false;
            }
        }
        Point[] choices = new Point[7]
        {
        new Point(469, item.TableStyle),
        new Point(16, 0),
        new Point(18, item.WorkbenchStyle),
        new Point(86, 0),
        new Point(87, item.PianoStyle),
        new Point(94, 0),
        new Point(101, item.BookcaseStyle)
        };
        foreach (Rectangle item4 in list)
        {
            int num3 = item4.Width / 8;
            int num4 = item4.Width / (num3 + 1);
            int num5 = GenBase._random.Next(2);
            for (int n = 0; n < num3; n++)
            {
                int num6 = (n + 1) * num4 + item4.X;
                switch (n + num5 % 2)
                {
                    case 0:
                        {
                            //int num7 = item4.Y + Math.Min(item4.Height / 2, item4.Height - 5);
                            //Vector2 vector = WorldGen.RandHousePicture();
                            //int type = (int)vector.X;
                            //int style = (int)vector.Y;
                            //if (!WorldGen.nearPicture(num6, num7))
                            //{
                            //    WorldGen.PlaceTile(num6, num7, type, mute: true, forced: false, -1, style);
                            //}
                            break;
                        }
                    case 1:
                        {
                            int num8 = item4.Y + 1;
                            WorldGen.PlaceTile(num6, num8, 34, mute: true, forced: false, -1, GenBase._random.Next(6));
                            for (int num9 = -1; num9 < 2; num9++)
                            {
                                for (int num10 = 0; num10 < 3; num10++)
                                {
                                    GenBase._tiles[num9 + num6, num10 + num8].TileFrameX += 54;
                                }
                            }
                            break;
                        }
                }
            }
            for (int num11 = item4.Width / 8 + 3; num11 > 0; num11--)
            {
                int num12 = GenBase._random.Next(item4.Width - 3) + 1 + item4.X;
                int num14 = item4.Y + item4.Height - 2;
                switch (GenBase._random.Next(4))
                {
                    case 0:
                        WorldGen.PlaceSmallPile(num12, num14, GenBase._random.Next(31, 34), 1, (ushort)ModContent.TileType<Tiles.CrystalMines.CrystalBits>()); // PlaceTile(num12, num14, TileID.Crystals);
                        break;
                    case 1:
                        WorldGen.PlaceTile(num12, num14, ModContent.TileType<Tiles.CrystalMines.GiantCrystalShard>(), true, false, -1, _random.Next(3));
                        break;
                    case 2:
                        {
                            //int num15 = GenBase._random.Next(2, WorldGen.statueList.Length);
                            //WorldGen.PlaceTile(num12, num14, WorldGen.statueList[num15].X, mute: true, forced: false, -1, WorldGen.statueList[num15].Y);
                            //if (WorldGen.StatuesWithTraps.Contains(num15))
                            //{
                            //    WorldGen.PlaceStatueTrap(num12, num14);
                            //}
                            break;
                        }
                    case 3:
                        {
                            Point point = Terraria.Utils.SelectRandom(GenBase._random, choices);
                            WorldGen.PlaceTile(num12, num14, point.X, mute: true, forced: false, -1, point.Y);
                            break;
                        }
                }
            }
        }
        foreach (Rectangle item5 in list)
        {
            item.ProcessRoom(item5);
        }
        bool flag2 = false;
        foreach (Rectangle item6 in list)
        {
            int num16 = item6.Height - 1 + item6.Y;
            int style2 = ((num16 > (int)Main.worldSurface) ? item.ChestStyle : 0);
            for (int num17 = 0; num17 < 10; num17++)
            {
                if (flag2 = AddCrystalChest(GenBase._random.Next(2, item6.Width - 2) + item6.X, num16, 0, notNearOtherChests: false, 0))
                {
                    break;
                }
            }
            if (flag2)
            {
                break;
            }
            for (int num18 = item6.X + 2; num18 <= item6.X + item6.Width - 2; num18++)
            {
                if (flag2 = AddCrystalChest(num18, num16, 0, notNearOtherChests: false, 0))
                {
                    break;
                }
            }
            if (flag2)
            {
                break;
            }
        }
        if (!flag2)
        {
            foreach (Rectangle item7 in list)
            {
                int num19 = item7.Y - 1;
                int style3 = ((num19 > (int)Main.worldSurface) ? item.ChestStyle : 0);
                for (int num20 = 0; num20 < 10; num20++)
                {
                    if (flag2 = AddCrystalChest(GenBase._random.Next(2, item7.Width - 2) + item7.X, num19, 0, notNearOtherChests: false, style3))
                    {
                        break;
                    }
                }
                if (flag2)
                {
                    break;
                }
                for (int num21 = item7.X + 2; num21 <= item7.X + item7.Width - 2; num21++)
                {
                    if (flag2 = AddCrystalChest(num21, num19, 0, notNearOtherChests: false, style3))
                    {
                        break;
                    }
                }
                if (flag2)
                {
                    break;
                }
            }
        }
        if (!flag2)
        {
            for (int num22 = 0; num22 < 1000; num22++)
            {
                int i2 = GenBase._random.Next(list[0].X - 30, list[0].X + 30);
                int num23 = GenBase._random.Next(list[0].Y - 30, list[0].Y + 30);
                int style4 = ((num23 > (int)Main.worldSurface) ? item.ChestStyle : 0);
                if (flag2 = AddCrystalChest(i2, num23, 0, notNearOtherChests: false, style4))
                {
                    break;
                }
            }
        }
        return true;
    }

    public static void AgeDefaultRoom(Rectangle room)
    {
        for (int i = 0; i < room.Width * room.Height / 16; i++)
        {
            int x = GenBase._random.Next(1, room.Width - 1) + room.X;
            int y = GenBase._random.Next(1, room.Height - 1) + room.Y;
            WorldUtils.Gen(new Point(x, y), new Shapes.Rectangle(2, 2), Actions.Chain(new Modifiers.Dither(), new Modifiers.Blotches(2, 2.0), new Modifiers.IsEmpty(), new Actions.SetTile(51, setSelfFrames: true)));
        }
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.85000002384185791), new Modifiers.Blotches(), new Modifiers.OnlyWalls(BuildData.Default.Wall), ((double)room.Y > Main.worldSurface) ? ((GenAction)new Actions.ClearWall(frameNeighbors: true)) : ((GenAction)new Actions.PlaceWall(2))));
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.949999988079071), new Modifiers.OnlyTiles(30, 321, 158), new Actions.ClearTile(frameNeighbors: true)));
    }

    public static void AgeCrystalRoom(Rectangle room)
    {
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.60000002384185791), new Modifiers.Blotches(2, 0.60000002384185791), new Modifiers.OnlyTiles(BuildData.Crystal.Tile), new Actions.SetTile((ushort)ModContent.TileType<Tiles.CrystalMines.CrystalStone>(), setSelfFrames: true), new Modifiers.Dither(0.8), new Actions.SetTile(TileID.CrystalBlock, setSelfFrames: true)));
        WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new Modifiers.Dither(), new Modifiers.OnlyTiles(161), new Modifiers.Offset(0, 1), new ActionStalagtite()));
        WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new Modifiers.Dither(), new Modifiers.OnlyTiles(161), new Modifiers.Offset(0, 1), new ActionStalagtite()));
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.85000002384185791), new Modifiers.Blotches(2, 0.8), ((double)room.Y > Main.worldSurface) ? ((GenAction)new Actions.ClearWall(frameNeighbors: true)) : ((GenAction)new Actions.PlaceWall(0))));
    }
}
