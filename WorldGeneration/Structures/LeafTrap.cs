using Avalon.Tiles.Savanna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures;

internal class LeafTrap
{


    public static void CreateLargeLeafTrap(int x, int y)
    {
        ushort loam = (ushort)ModContent.TileType<Loam>();
        ushort grass = (ushort)ModContent.TileType<SavannaGrass>();
        ushort bramble = (ushort)ModContent.TileType<Bramble>();

        for (int i = x; i < x + 13; i++)
        {
            for (int j = y; j < y + 6; j++)
            {
                if ((i == x + 4 || i == x + 8) && j == y + 4)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = loam;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if (i == x || i == x + 12)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = loam;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if (j == y + 5)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = loam;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if (((i >= x && i <= x + 2) || (i >= x + 10 && i <= x + 12) || i == x + 6) && j == y)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = grass;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if ((i == x + 1 || i == x + 11) && j >= y + 1 && j <= y + 4)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = bramble;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if (i >= x + 1 && i <= x + 11 && j == y + 4 && i != x + 4 && i != x + 8)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = bramble;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if (i == x + 6 && j >= y + 1 && j <= y + 3)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = bramble;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
                if ((i == x + 2 || i == x + 10) && j == y + 3)
                {
                    Tile t = Main.tile[i, j];
                    t.TileType = bramble;
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(i, j);
                }
            }
        }
        Place3x4(x + 4, y + 3, (ushort)ModContent.TileType<PlatformLeaf>(), 0);
        Place3x4(x + 8, y + 3, (ushort)ModContent.TileType<PlatformLeaf>(), 0);
    }
    public static void CreateLeafTrap(int x, int y)
    {
        ushort loam = (ushort)ModContent.TileType<Loam>();
        ushort grass = (ushort)ModContent.TileType<SavannaGrass>();
        ushort bramble = (ushort)ModContent.TileType<Bramble>();
        for (int i = x - 2; i <= x + 2; i++)
        {
            for (int j = y - 3; j < y + 6; j++)
            {
                WorldGen.KillTile(i, j, noItem: true);
                Tile t = Main.tile[i, j];
                t.Slope = SlopeType.Solid;
            }
        }
        for (int i = x - 2; i <= x + 2; i++)
        {
            for (int j = y; j < y + 6; j++)
            {
                if (i == x - 2 || i == x + 2)
                {
                    if (j == y)
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = grass;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                    else
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = loam;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                }

                if (j >= y + 4)
                {
                    if (j == y + 4 && (i == x + 1 || i == x - 1))
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = bramble;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                    else
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = loam;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
            }
        }
        Place3x4(x, y + 3, (ushort)ModContent.TileType<PlatformLeaf>(), 0);
    }

    public static void Place3x4(int x, int y, ushort type, int style)
    {
        if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
            return;

        bool flag = true;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 3; j < y + 1; j++)
            {
                if (Main.tile[i, j].HasTile)
                    flag = false;
            }

            if (!WorldGen.SolidTile2(x, y + 1))
                flag = false;
        }

        if (flag)
        {
            int num = 0;
            for (int k = -3; k <= 0; k++)
            {
                short frameY = (short)((3 + k) * 18);
                Tile t = Main.tile[x - 1, y + k];
                Main.tile[x - 1, y + k].Active(true);
                Main.tile[x - 1, y + k].TileFrameY = frameY;
                Main.tile[x - 1, y + k].TileFrameX = (short)num;
                Main.tile[x - 1, y + k].TileType = type;
                Main.tile[x, y + k].Active(true);
                Main.tile[x, y + k].TileFrameY = frameY;
                Main.tile[x, y + k].TileFrameX = (short)(num + 26);
                Main.tile[x, y + k].TileType = type;
                Main.tile[x + 1, y + k].Active(true);
                Main.tile[x + 1, y + k].TileFrameY = frameY;
                Main.tile[x + 1, y + k].TileFrameX = (short)(num + 52);
                Main.tile[x + 1, y + k].TileType = type;
            }
        }
    }
}
