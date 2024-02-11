using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Structures;

public class TropicsHouseBuilder : HouseBuilder
{
    public TropicsHouseBuilder(IEnumerable<Rectangle> rooms)
        : base(HouseType.Jungle, rooms)
    {
        TileType = (ushort)ModContent.TileType<Tiles.BleachedEbony>();
        WallType = (ushort)ModContent.WallType<Walls.BleachedEbonyWall>();
        BeamType = (ushort)ModContent.TileType<Tiles.BleachedEbonyBeam>();
        PlatformStyle = 2;
        DoorStyle = 2;
        TableStyle = 2;
        WorkbenchStyle = 2;
        PianoStyle = 2;
        BookcaseStyle = 12;
        ChairStyle = 3;
        ChestStyle = 8;
    }

    protected override void AgeRoom(Rectangle room)
    {
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.6), new Modifiers.Blotches(2, 0.6), new Modifiers.OnlyTiles(TileType), new Actions.SetTileKeepWall((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>(), setSelfFrames: true), new Modifiers.Dither(0.8), new Actions.SetTileKeepWall((ushort)ModContent.TileType<Tiles.Tropics.Loam>(), setSelfFrames: true)));
        WorldUtils.Gen(new Point(room.X + 1, room.Y), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new Modifiers.Dither(), new Modifiers.OnlyTiles((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()), new Modifiers.Offset(0, 1), new Modifiers.IsEmpty(), new ActionVines(3, room.Height, ModContent.TileType<Tiles.Tropics.TropicalVines>())));
        WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain(new Modifiers.Dither(), new Modifiers.OnlyTiles((ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()), new Modifiers.Offset(0, 1), new Modifiers.IsEmpty(), new ActionVines(3, room.Height, ModContent.TileType<Tiles.Tropics.TropicalVines>())));
        WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new Modifiers.Dither(0.85), new Modifiers.Blotches(), new Actions.PlaceWall((ushort)ModContent.WallType<Walls.TropicalGrassWall>())));
    }
}
public class TropicsCaveHouseHook : ModHook
{
    protected override void Apply()
    {
        On_HouseUtils.GetHouseType += On_HouseUtils_GetHouseType;
        //On_HouseUtils.AreRoomsValid += On_HouseUtils_AreRoomsValid;
        On_HouseUtils.CreateBuilder += On_HouseUtils_CreateBuilder;
    }

    //private bool On_HouseUtils_AreRoomsValid(On_HouseUtils.orig_AreRoomsValid orig, IEnumerable<Rectangle> rooms, StructureMap structures, HouseType style)
    //{
    //    if (ModContent.GetInstance<AvalonWorld>().WorldJungle == Enums.WorldJungle.Tropics)
    //    {
    //        foreach (Rectangle room in rooms)
    //        {
    //            if (!structures.CanPlace(room, BlacklistedTiles, 5))
    //            {
    //                return false;
    //            }
    //        }
    //    }

    //    return orig.Invoke(rooms, structures, style);
    //}

    private HouseType On_HouseUtils_GetHouseType(On_HouseUtils.orig_GetHouseType orig, IEnumerable<Rectangle> rooms)
    {
        Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
        foreach (Rectangle room in rooms)
        {
            WorldUtils.Gen(new Point(room.X - 10, room.Y - 10), new Shapes.Rectangle(room.Width + 20, room.Height + 20), new Actions.TileScanner(0, 59, 147, 1, 161, 53, 396, 397, 368, 367, 60, 70, (ushort)ModContent.TileType<Tiles.Tropics.Loam>(), (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()).Output(dictionary));
        }

        List<Tuple<HouseType, int>> list = new List<Tuple<HouseType, int>>();
        list.Add(Tuple.Create(HouseType.Wood, dictionary[0] + dictionary[1]));
        if (ModContent.GetInstance<AvalonWorld>().WorldJungle == Enums.WorldJungle.Tropics)
        {
            list.Add(Tuple.Create(HouseType.Jungle, dictionary[(ushort)ModContent.TileType<Tiles.Tropics.Loam>()] + dictionary[(ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()] * 10));
        }
        else
        {
            list.Add(Tuple.Create(HouseType.Jungle, dictionary[59] + dictionary[60] * 10));
        }
        list.Add(Tuple.Create(HouseType.Mushroom, dictionary[59] + dictionary[70] * 10));
        list.Add(Tuple.Create(HouseType.Ice, dictionary[147] + dictionary[161]));
        list.Add(Tuple.Create(HouseType.Desert, dictionary[397] + dictionary[396] + dictionary[53]));
        list.Add(Tuple.Create(HouseType.Granite, dictionary[368]));
        list.Add(Tuple.Create(HouseType.Marble, dictionary[367]));
        list.Sort(SortBiomeResults);
        return list[0].Item1;
    }
    private static HouseType GetHouseType(IEnumerable<Rectangle> rooms)
    {
        Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
        foreach (Rectangle room in rooms)
        {
            WorldUtils.Gen(new Point(room.X - 10, room.Y - 10), new Shapes.Rectangle(room.Width + 20, room.Height + 20), new Actions.TileScanner(0, 59, 147, 1, 161, 53, 396, 397, 368, 367, 60, 70, (ushort)ModContent.TileType<Tiles.Tropics.Loam>(), (ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()).Output(dictionary));
        }

        List<Tuple<HouseType, int>> list = new List<Tuple<HouseType, int>>();
        list.Add(Tuple.Create(HouseType.Wood, dictionary[0] + dictionary[1]));
        if (ModContent.GetInstance<AvalonWorld>().WorldJungle == Enums.WorldJungle.Tropics)
        {
            list.Add(Tuple.Create(HouseType.Jungle, dictionary[(ushort)ModContent.TileType<Tiles.Tropics.Loam>()] + dictionary[(ushort)ModContent.TileType<Tiles.Tropics.TropicalGrass>()] * 10));
        }
        else
        {
            list.Add(Tuple.Create(HouseType.Jungle, dictionary[59] + dictionary[60] * 10));
        }
        list.Add(Tuple.Create(HouseType.Mushroom, dictionary[59] + dictionary[70] * 10));
        list.Add(Tuple.Create(HouseType.Ice, dictionary[147] + dictionary[161]));
        list.Add(Tuple.Create(HouseType.Desert, dictionary[397] + dictionary[396] + dictionary[53]));
        list.Add(Tuple.Create(HouseType.Granite, dictionary[368]));
        list.Add(Tuple.Create(HouseType.Marble, dictionary[367]));
        list.Sort(SortBiomeResults);
        return list[0].Item1;
    }
    private static int SortBiomeResults(Tuple<HouseType, int> item1, Tuple<HouseType, int> item2) => item2.Item2.CompareTo(item1.Item2);
    private static int SortBiomeResults(Tuple<int, int> item1, Tuple<int, int> item2) => item2.Item2.CompareTo(item1.Item2);
    private HouseBuilder On_HouseUtils_CreateBuilder(On_HouseUtils.orig_CreateBuilder orig, Point origin, StructureMap structures)
    {
        MethodInfo? CreateRooms = typeof(HouseUtils).GetMethod("CreateRooms", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo? AreRoomLocationsValid = typeof(HouseUtils).GetMethod("AreRoomLocationsValid", BindingFlags.NonPublic | BindingFlags.Static);

        List<Rectangle> list = (List<Rectangle>)CreateRooms.Invoke(null, new object[] { origin });
        if (list.Count == 0 || !(bool)AreRoomLocationsValid.Invoke(null, new object[] { list }))
            return HouseBuilder.Invalid;

        HouseType houseType = GetHouseType(list);

        if (ModContent.GetInstance<AvalonWorld>().WorldJungle == Enums.WorldJungle.Tropics && houseType == HouseType.Jungle)
        {
            return new TropicsHouseBuilder(list);
            //MethodInfo? CreateRooms = typeof(HouseUtils).GetMethod("CreateRooms", BindingFlags.NonPublic | BindingFlags.Static);
            //MethodInfo? AreRoomLocationsValid = typeof(HouseUtils).GetMethod("AreRoomLocationsValid", BindingFlags.NonPublic | BindingFlags.Static);
            //MethodInfo? GetHouseType = typeof(HouseUtils).GetMethod("GetHouseType", BindingFlags.NonPublic | BindingFlags.Static);
            //MethodInfo? AreRoomsValid = typeof(HouseUtils).GetMethod("AreRoomsValid", BindingFlags.NonPublic | BindingFlags.Static);

            //List<Rectangle> list = (List<Rectangle>)CreateRooms.Invoke(null, new object[] { origin });
            //if (list.Count == 0 || !(bool)AreRoomLocationsValid.Invoke(null, new object[] { list }))
            //    return HouseBuilder.Invalid;

            //HouseType houseType = GetHouseType(list);
            //(HouseType)GetHouseType.Invoke(null, new object[] { list });

            //if (houseType == HouseType.Wood)
            //{
            //    return new WoodHouseBuilder(list);
            //}
            //else if (houseType == HouseType.Ice)
            //{
            //    return new IceHouseBuilder(list);
            //}
            //else if (houseType == HouseType.Desert)
            //{
            //    return new DesertHouseBuilder(list);
            //}
            //else if (houseType == HouseType.Jungle)
            //{
                
            //}
            //else if (houseType == HouseType.Mushroom)
            //{
            //    return new MushroomHouseBuilder(list);
            //}
            //else if (houseType == HouseType.Granite)
            //{
            //    return new GraniteHouseBuilder(list);
            //}
            //else if (houseType == HouseType.Marble)
            //{
            //    return new MarbleHouseBuilder(list);
            //}
            //else
            //{
            //    return new TropicsHouseBuilder(list);
            //}



            //switch (houseType)
            //{
            //    case HouseType.Wood:
            //        return new WoodHouseBuilder(list);
            //    case HouseType.Desert:
            //        return new DesertHouseBuilder(list);
            //    case HouseType.Granite:
            //        return new GraniteHouseBuilder(list);
            //    case HouseType.Ice:
            //        return new IceHouseBuilder(list);
            //    case 3: // HouseType.Jungle:
            //        return new JungleHouseBuilder(list);
            //    case 6: // HouseType.Marble:
            //        return new MarbleHouseBuilder(list);
            //    case 4: // HouseType.Mushroom:
            //        return new MushroomHouseBuilder(list);
            //    case 7:
            //        return new TropicsHouseBuilder(list);
            //    default:
            //        return new WoodHouseBuilder(list);
            //}
        }
        return orig.Invoke(origin, structures);
    }
}
