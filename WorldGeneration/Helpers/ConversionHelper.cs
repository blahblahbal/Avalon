using System;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Walls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tile = Avalon.Data.Sets.Tile;

namespace Avalon.WorldGeneration.Helpers;

public static class ConversionHelper
{
    public static void ConvertToJungle(int i, int j, int size = 4)
    {
        for (var k = i - size; k <= i + size; k++)
        {
            for (var l = j - size; l <= j + size; l++)
            {
                if (!WorldGen.InWorld(k, l, 1) || Math.Abs(k - i) + Math.Abs(l - j) >= Math.Sqrt(size * size + size * size))
                    continue;

                int type = Main.tile[k, l].TileType;
                int wall = Main.tile[k, l].WallType;

                if (WallID.Sets.Conversion.Grass[wall])
                {
                    Main.tile[k, l].WallType = WallID.JungleUnsafe;
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (WallID.Sets.Conversion.Dirt[wall])
                {
                    Main.tile[k, l].WallType = WallID.MudUnsafe;
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }

                if (TileID.Sets.Conversion.Stone[type])
                {
                    Main.tile[k, l].TileType = TileID.Stone;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Grass[type])
                {
                    Main.tile[k, l].TileType = TileID.JungleGrass;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Ice[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<GreenIce>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Sand[type])
                {
                    Main.tile[k, l].TileType = TileID.Sand;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Sandstone[type])
                {
                    Main.tile[k, l].TileType = TileID.Sandstone;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.HardenedSand[type])
                {
                    Main.tile[k, l].TileType = TileID.HardenedSand;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Tile.Conversion.ShortGrass[type])
                {
                    Main.tile[k, l].TileType = TileID.JunglePlants;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }

                if (type is TileID.Plants2 or TileID.HallowedPlants2)
                {
                    Main.tile[k, l].TileType = TileID.JunglePlants2;
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }

    public static void ConvertToContagion(int i, int j, int size = 4)
    {
        for (var x = i - size; x <= i + size; x++)
        {
            for (var y = j - size; y <= j + size; y++)
            {
                if (!WorldGen.InWorld(x, y, 1) || Math.Abs(x - i) + Math.Abs(y - j) >= Math.Sqrt(size * size + size * size))
                    continue;

                if (Main.tile[x, y].TileType > TileLoader.TileCount || Main.tile[x, y].WallType > WallLoader.WallCount)
                    continue;

                // Walls
                _ = ConvertWall<ContagionGrassWall>(x, y, type => WallID.Sets.Conversion.Grass[type]) ||
                    ConvertWall<ChunkstoneWall>(x, y, type => WallID.Sets.Conversion.Stone[type]) ||
                    ConvertWall<HardenedSnotsandWallUnsafe>(x, y, type => WallID.Sets.Conversion.HardenedSand[type]) ||
                    ConvertWall<SnotsandstoneWallUnsafe>(x, y, type => WallID.Sets.Conversion.Sandstone[type]) ||
                    ConvertWall<ContagionLumpWall>(x, y, type => WallID.Sets.Conversion.NewWall1[type]) ||
                    ConvertWall<ContagionMouldWall>(x, y, type => WallID.Sets.Conversion.NewWall2[type]) ||
                    ConvertWall<ContagionCystWallUnsafe>(x, y, type => WallID.Sets.Conversion.NewWall3[type]) ||
                    ConvertWall<ContagionBoilWall>(x, y, type => WallID.Sets.Conversion.NewWall4[type]);

                // Tiles
                _ = ConvertTile<Chunkstone>(x, y, type => Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) ||
                    ConvertTile<ContagionJungleGrass>(x, y, type => TileID.Sets.Conversion.JungleGrass[type]) ||
                    ConvertTile<Ickgrass>(x, y, type => TileID.Sets.Conversion.Grass[type]) ||
                    ConvertTile<YellowIce>(x, y, type => TileID.Sets.Conversion.Ice[type]) ||
                    ConvertTile<Snotsand>(x, y, type => TileID.Sets.Conversion.Sand[type]) ||
                    ConvertTile<HardenedSnotsand>(x, y, type => TileID.Sets.Conversion.HardenedSand[type], false) ||
                    ConvertTile<Snotsandstone>(x, y, type => TileID.Sets.Conversion.Sandstone[type], false) ||
                    ConvertTile<ContagionThornyBushes>(x, y, type => TileID.Sets.Conversion.Thorn[type], false);
            }
        }
    }

    private static bool ConvertWall<T>(int x, int y, Func<int, bool> validTypePredicate) where T : ModWall
    {
        if (!validTypePredicate(Main.tile[x, y].WallType) || Main.tile[x, y].WallType == ModContent.WallType<T>())
            return false;

        Main.tile[x, y].WallType = (ushort)ModContent.WallType<T>();
        WorldGen.SquareWallFrame(x, y);
        NetMessage.SendTileSquare(-1, x, y);

        return true;
    }

    private static bool ConvertTile<T>(int x, int y, Func<int, bool> validTypePredicate, bool tryKillTreeAbove = true) where T : ModTile
    {
        if (!validTypePredicate(Main.tile[x, y].TileType) || Main.tile[x, y].TileType == ModContent.TileType<T>())
            return false;

        if (tryKillTreeAbove)
            WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(x, y, ModContent.TileType<T>());

        Main.tile[x, y].TileType = (ushort)ModContent.TileType<T>();
        WorldGen.SquareTileFrame(x, y);
        NetMessage.SendTileSquare(-1, x, y);

        return true;
    }
}
