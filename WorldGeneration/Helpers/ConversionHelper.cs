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

                // Check walls
                if (WallID.Sets.Conversion.Stone[wall])
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (WallID.Sets.Conversion.Grass[wall])
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ContagionGrassWall>();
                    WorldGen.SquareWallFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }

                // Check tiles
                if (type == TileID.Stalactite && (Main.tile[k, l].TileFrameX >= 54 && Main.tile[k, l].TileFrameX <= 90 || Main.tile[k, l].TileFrameX >= 216 && Main.tile[k, l].TileFrameX <= 360))
                {
                    if (Main.tile[k, l].TileFrameX >= 54 && Main.tile[k, l].TileFrameX <= 90)
                    {
                        Main.tile[k, l].TileFrameX -= 54;
                    }
                    if (Main.tile[k, l].TileFrameX >= 216 && Main.tile[k, l].TileFrameX <= 252)
                    {
                        Main.tile[k, l].TileFrameX -= 216;
                    }
                    if (Main.tile[k, l].TileFrameX >= 270 && Main.tile[k, l].TileFrameX <= 306)
                    {
                        Main.tile[k, l].TileFrameX -= 270;
                    }
                    if (Main.tile[k, l].TileFrameX >= 324 && Main.tile[k, l].TileFrameX <= 360)
                    {
                        Main.tile[k, l].TileFrameX -= 324;
                    }
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
                }
                //else 
                if (Tile.Conversion.Vines[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionVines>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                if (TileID.Sets.Conversion.Stone[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Chunkstone>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Grass[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Ickgrass>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Ice[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<YellowIce>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Sand[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsand>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.Sandstone[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (TileID.Sets.Conversion.HardenedSand[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
                else if (Tile.Conversion.ShortGrass[type])
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }

                if (type is TileID.Plants2 or TileID.HallowedPlants2)
                {
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                    WorldGen.SquareTileFrame(k, l);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }
}
