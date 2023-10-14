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
        for (var k = i - size; k <= i + size; k++)
        {
            for (var l = j - size; l <= j + size; l++)
            {
                if (!WorldGen.InWorld(k, l, 1) || Math.Abs(k - i) + Math.Abs(l - j) >= Math.Sqrt(size * size + size * size))
                    continue;

                int type = Main.tile[k, l].TileType;
                int wall = Main.tile[k, l].WallType;

                if (type > TileLoader.TileCount || wall > WallLoader.WallCount)
                    continue;

                // Walls
                if (WallID.Sets.Conversion.Grass[wall] && wall != ModContent.WallType<ContagionGrassWall>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionGrassWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.Stone[wall] && wall != ModContent.WallType<ChunkstoneWall>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != ModContent.WallType<HardenedSnotsandWallUnsafe>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<HardenedSnotsandWallUnsafe>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.Sandstone[wall] && wall != ModContent.WallType<SnotsandstoneWallUnsafe>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<SnotsandstoneWallUnsafe>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.NewWall1[wall] && wall != ModContent.WallType<ContagionLumpWall>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionLumpWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.NewWall2[wall] && wall != ModContent.WallType<ContagionMouldWall>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionMouldWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.NewWall3[wall] && wall != ModContent.WallType<ContagionCystWallUnsafe>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionCystWallUnsafe>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (WallID.Sets.Conversion.NewWall4[wall] && wall != ModContent.WallType<ContagionBoilWall>())
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionBoilWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }

                // Tiles
                if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != ModContent.TileType<Chunkstone>())
                {
                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, ModContent.TileType<Chunkstone>());
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Chunkstone>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.JungleGrass[type] && type != ModContent.TileType<ContagionJungleGrass>())
                {
                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, ModContent.TileType<ContagionJungleGrass>());
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<Ickgrass>())
                {
                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, ModContent.TileType<Ickgrass>());
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Ickgrass>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.Ice[type] && type != ModContent.TileType<YellowIce>())
                {
                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, ModContent.TileType<YellowIce>());
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<YellowIce>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.Sand[type] && type != ModContent.TileType<Snotsandstone>())
                {
                    WorldGen.TryKillingTreesAboveIfTheyWouldBecomeInvalid(k, l, ModContent.TileType<Snotsandstone>());
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.HardenedSand[type] && type != ModContent.TileType<HardenedSnotsand>())
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.Sandstone[type] && type != ModContent.TileType<Snotsandstone>())
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }
                else if (TileID.Sets.Conversion.Thorn[type] && type != ModContent.TileType<ContagionThornyBushes>())
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionThornyBushes>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l);
                }

                // if (Tile.Conversion.Vines[type])
                // {
                //     Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionVines>();
                //     WorldGen.SquareTileFrame(k, l);
                //     NetMessage.SendTileSquare(-1, k, l, 1);
                // }
                // else if (Tile.Conversion.ShortGrass[type])
                // {
                //     Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                //     WorldGen.SquareTileFrame(k, l);
                //     NetMessage.SendTileSquare(-1, k, l, 1);
                // }
                //
                // if (type is TileID.Plants2 or TileID.HallowedPlants2)
                // {
                //     Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                //     WorldGen.SquareTileFrame(k, l);
                //     NetMessage.SendTileSquare(-1, k, l, 1);
                // }
            }
        }
    }
}
