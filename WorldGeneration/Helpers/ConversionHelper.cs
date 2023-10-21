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
        for (var x = i - size; x <= i + size; x++)
        {
            for (var y = j - size; y <= j + size; y++)
            {
                if (!WorldGen.InWorld(x, y, 1) || Math.Abs(x - i) + Math.Abs(y - j) >= Math.Sqrt(size * size + size * size))
                    continue;

                if (Main.tile[x, y].TileType > TileLoader.TileCount || Main.tile[x, y].WallType > WallLoader.WallCount)
                    continue;

                // Walls
                _ = ConvertWall(x, y, type => WallID.Sets.Conversion.Grass[type], WallID.JungleUnsafe) ||
                    ConvertWall(x, y, type => WallID.Sets.Conversion.Dirt[type], WallID.MudUnsafe);

                // Tiles
                _ = ConvertTile(x, y, type => TileID.Sets.Conversion.Stone[type], TileID.Stone) ||
                    ConvertTile(x, y, type => TileID.Sets.Conversion.Grass[type], TileID.JungleGrass) ||
                    ConvertTile<GreenIce>(x, y, type => TileID.Sets.Conversion.Ice[type]) ||
                    ConvertTile(x, y, type => TileID.Sets.Conversion.Sand[type], TileID.Sand) ||
                    ConvertTile(x, y, type => TileID.Sets.Conversion.HardenedSand[type], TileID.HardenedSand, false) ||
                    ConvertTile(x, y, type => TileID.Sets.Conversion.Sandstone[type], TileID.Sandstone, false);

                // TODO: This isn't how plants should be converted
                if (Tile.Conversion.ShortGrass[Main.tile[x, y].TileType])
                {
                    Main.tile[x, y].TileType = TileID.JunglePlants;
                    WorldGen.SquareTileFrame(x, y);
                    NetMessage.SendTileSquare(-1, x, y, 1);
                }
                else if (Main.tile[x, y].TileType is TileID.Plants2 or TileID.HallowedPlants2)
                {
                    Main.tile[x, y].TileType = TileID.JunglePlants2;
                    WorldGen.SquareTileFrame(x, y);
                    NetMessage.SendTileSquare(-1, x, y, 1);
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

    private static bool ConvertWall<TWall>(int x, int y, Func<int, bool> validTypePredicate) where TWall : ModWall
    {
        return ConvertWall(x, y, validTypePredicate, ModContent.WallType<TWall>());
    }

    private static bool ConvertWall(int x, int y, Func<int, bool> validTypePredicate, int wallType)
    {
        if (!validTypePredicate(Main.tile[x, y].WallType) || Main.tile[x, y].WallType == wallType)
            return false;

        Main.tile[x, y].WallType = (ushort)wallType;
        WorldGen.SquareWallFrame(x, y);
        NetMessage.SendTileSquare(-1, x, y);

        return true;
    }

    private static bool ConvertTile<TTile>(int x, int y, Func<int, bool> validTypePredicate, bool tryKillTreeAbove = true) where TTile : ModTile
    {
        return ConvertTile(x, y, validTypePredicate, ModContent.TileType<TTile>(), tryKillTreeAbove);
    }

    private static bool ConvertTile(int x, int y, Func<int, bool> validTypePredicate, int tileType, bool tryKillTreeAbove = true)
    {
        if (!validTypePredicate(Main.tile[x, y].TileType) || Main.tile[x, y].TileType == tileType)
            return false;

        if (tryKillTreeAbove)
            WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(x, y, tileType);

        Main.tile[x, y].TileType = (ushort)tileType;
        if (tileType == ModContent.TileType<Ickgrass>() && Main.tile[x, y - 1].TileType == TileID.Pumpkins)
        {
            WorldGen.KillTile(x, y - 1);
            if (!Main.tile[x, y - 1].HasTile && Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y - 1);
            }
        }
        WorldGen.SquareTileFrame(x, y);
        NetMessage.SendTileSquare(-1, x, y);

        return true;
    }
}
