using Avalon.Items.Accessories;
using Avalon.Items.Potions;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Potions.Buff;
using Avalon.Items.Accessories.Vanity;

namespace Avalon.WorldGeneration.Structures;

class LavaShrine
{
    public static void AddLavaShrine(int x, int y)
    {
        ushort LA = TileID.Hellstone;
        ushort LB = TileID.HellstoneBrick;
        ushort MO = (ushort)ModContent.TileType<Tiles.BrimstoneBlock>();
        ushort backWall = WallID.ObsidianBackEcho;
        WorldGen.PlaceTile(x + 2, y, LB, forced: true);
        WorldGen.PlaceTile(x + 5, y, LB, forced: true);
        WorldGen.PlaceTile(x + 6, y, LB, forced: true);
        WorldGen.PlaceTile(x + 9, y, LB, forced: true);
        Utils.ResetSlope(x + 2, y);
        Utils.ResetSlope(x + 5, y);
        Utils.ResetSlope(x + 6, y);
        Utils.ResetSlope(x + 9, y);
        for (int xoff = x + 2; xoff <= x + 9; xoff++)
        {
            WorldGen.PlaceTile(xoff, y + 1, LB, forced: true);
            Utils.ResetSlope(xoff, y + 1);
        }
        Main.tile[x + 3, y + 2].Active(true);
        Main.tile[x + 3, y + 3].Active(true);
        Main.tile[x + 8, y + 2].Active(true);
        Main.tile[x + 8, y + 3].Active(true);

        Main.tile[x + 3, y + 2].TileType = LB;
        Main.tile[x + 3, y + 3].TileType = LB;
        Main.tile[x + 8, y + 2].TileType = LB;
        Main.tile[x + 8, y + 3].TileType = LB;
        for (int xoff = x + 4; xoff <= x + 7; xoff++)
        {
            for (int yoff = y + 2; yoff <= y + 3; yoff++)
            {
                Main.tile[xoff, yoff].Active(true);
                Main.tile[xoff, yoff].TileType = LA;
            }
        }
        Main.tile[x + 4, y + 4].Active(true);
        Main.tile[x + 5, y + 4].Active(true);
        Main.tile[x + 6, y + 4].Active(true);
        Main.tile[x + 7, y + 4].Active(true);
        Main.tile[x + 5, y + 5].Active(true);
        Main.tile[x + 6, y + 5].Active(true);
        Main.tile[x + 7, y + 5].Active(true);
        Main.tile[x + 5, y + 6].Active(true);
        Main.tile[x + 5, y + 7].Active(true);
        Main.tile[x + 5, y + 8].Active(true);
        Main.tile[x + 4, y + 8].Active(true);
        Main.tile[x + 3, y + 8].Active(true);
        Main.tile[x + 3, y + 9].Active(true);
        Main.tile[x + 3, y + 10].Active(true);
        Main.tile[x + 2, y + 10].Active(true);
        Main.tile[x + 1, y + 10].Active(true);
        Main.tile[x + 1, y + 11].Active(true);
        Main.tile[x, y + 11].Active(true);
        Main.tile[x, y + 12].Active(true);
        Main.tile[x, y + 13].Active(true);
        Main.tile[x, y + 14].Active(true);
        Main.tile[x + 1, y + 14].Active(true);
        Main.tile[x + 1, y + 15].Active(true);
        Main.tile[x + 2, y + 15].Active(true);
        Main.tile[x + 2, y + 16].Active(true);
        Main.tile[x + 3, y + 16].Active(true);
        Main.tile[x + 3, y + 17].Active(true);
        Main.tile[x + 4, y + 17].Active(true);
        Main.tile[x + 5, y + 17].Active(true);
        Main.tile[x + 6, y + 17].Active(true);
        Main.tile[x + 6, y + 16].Active(true);
        Main.tile[x + 7, y + 16].Active(true);
        Main.tile[x + 8, y + 16].Active(true);
        Main.tile[x + 8, y + 15].Active(true);
        Main.tile[x + 9, y + 15].Active(true);
        Main.tile[x + 9, y + 14].Active(true);
        Main.tile[x + 10, y + 14].Active(true);
        Main.tile[x + 10, y + 13].Active(true);
        Main.tile[x + 11, y + 13].Active(true);
        Main.tile[x + 12, y + 13].Active(true);
        Main.tile[x + 13, y + 13].Active(true);
        Main.tile[x + 13, y + 14].Active(true);
        Main.tile[x + 14, y + 14].Active(true);
        Main.tile[x + 14, y + 15].Active(true);
        Main.tile[x + 15, y + 15].Active(true);
        Main.tile[x + 16, y + 15].Active(true);
        Main.tile[x + 16, y + 16].Active(true);
        Main.tile[x + 17, y + 16].Active(true);
        Main.tile[x + 18, y + 16].Active(true);
        Main.tile[x + 19, y + 16].Active(true);
        Main.tile[x + 19, y + 17].Active(true);
        Main.tile[x + 20, y + 17].Active(true);
        Main.tile[x + 20, y + 18].Active(true);

        Main.tile[x + 4, y + 4].TileType = LB;
        Main.tile[x + 5, y + 4].TileType = LB;
        Main.tile[x + 6, y + 4].TileType = LB;
        Main.tile[x + 7, y + 4].TileType = LB;
        Main.tile[x + 5, y + 5].TileType = LB;
        Main.tile[x + 6, y + 5].TileType = LB;
        Main.tile[x + 7, y + 5].TileType = LB;
        Main.tile[x + 5, y + 6].TileType = LB;
        Main.tile[x + 5, y + 7].TileType = LB;
        Main.tile[x + 5, y + 8].TileType = LB;
        Main.tile[x + 4, y + 8].TileType = LB;
        Main.tile[x + 3, y + 8].TileType = LB;
        Main.tile[x + 3, y + 9].TileType = LB;
        Main.tile[x + 3, y + 10].TileType = LB;
        Main.tile[x + 2, y + 10].TileType = LB;
        Main.tile[x + 1, y + 10].TileType = LB;
        Main.tile[x + 1, y + 11].TileType = LB;
        Main.tile[x, y + 11].TileType = LB;
        Main.tile[x, y + 12].TileType = LB;
        Main.tile[x, y + 13].TileType = LB;
        Main.tile[x, y + 14].TileType = LB;
        Main.tile[x + 1, y + 14].TileType = LB;
        Main.tile[x + 1, y + 15].TileType = LB;
        Main.tile[x + 2, y + 15].TileType = LB;
        Main.tile[x + 2, y + 16].TileType = LB;
        Main.tile[x + 3, y + 16].TileType = LB;
        Main.tile[x + 3, y + 17].TileType = LB;
        Main.tile[x + 4, y + 17].TileType = LB;
        Main.tile[x + 5, y + 17].TileType = LB;
        Main.tile[x + 6, y + 17].TileType = LB;
        Main.tile[x + 6, y + 16].TileType = LB;
        Main.tile[x + 7, y + 16].TileType = LB;
        Main.tile[x + 8, y + 16].TileType = LB;
        Main.tile[x + 8, y + 15].TileType = LB;
        Main.tile[x + 9, y + 15].TileType = LB;
        Main.tile[x + 9, y + 14].TileType = LB;
        Main.tile[x + 10, y + 14].TileType = LB;
        Main.tile[x + 10, y + 13].TileType = LB;
        Main.tile[x + 11, y + 13].TileType = LB;
        Main.tile[x + 12, y + 13].TileType = LB;
        Main.tile[x + 13, y + 13].TileType = LB;
        Main.tile[x + 13, y + 14].TileType = LB;
        Main.tile[x + 14, y + 14].TileType = LB;
        Main.tile[x + 14, y + 15].TileType = LB;
        Main.tile[x + 15, y + 15].TileType = LB;
        Main.tile[x + 16, y + 15].TileType = LB;
        Main.tile[x + 16, y + 16].TileType = LB;
        Main.tile[x + 17, y + 16].TileType = LB;
        Main.tile[x + 18, y + 16].TileType = LB;
        Main.tile[x + 19, y + 16].TileType = LB;
        Main.tile[x + 19, y + 17].TileType = LB;
        Main.tile[x + 20, y + 17].TileType = LB;
        Main.tile[x + 20, y + 18].TileType = LB;

        //------------------------------------------------------------
        for (int xoff = x + 8; xoff <= x + 36; xoff++)
        {
            Main.tile[xoff, y + 5].Active(true);
            Main.tile[xoff, y + 5].TileType = LB;
        }


        Main.tile[x + 11, y + 4].Active(true);
        Main.tile[x + 12, y + 4].Active(true);
        Main.tile[x + 13, y + 4].Active(true);

        Main.tile[x + 14, y + 4].Active(true); // LA

        Main.tile[x + 15, y + 4].Active(true);
        Main.tile[x + 16, y + 4].Active(true);

        Main.tile[x + 17, y + 4].Active(true); // LA
        Main.tile[x + 18, y + 4].Active(true); // LA

        Main.tile[x + 13, y + 3].Active(true);
        Main.tile[x + 14, y + 3].Active(true);
        Main.tile[x + 15, y + 3].Active(true);

        Main.tile[x + 16, y + 3].Active(true); // LA

        Main.tile[x + 17, y + 3].Active(true);

        Main.tile[x + 18, y + 3].Active(true); // LA

        Main.tile[x + 14, y + 2].Active(true);
        Main.tile[x + 15, y + 2].Active(true);
        Main.tile[x + 16, y + 2].Active(true);
        Main.tile[x + 17, y + 2].Active(true);
        Main.tile[x + 18, y + 2].Active(true);

        for (int xoff = x + 19; xoff <= x + 25; xoff++)
        {
            for (int yoff = y + 2; yoff <= y + 4; yoff++)
            {
                Main.tile[xoff, yoff].Active(true);
                Main.tile[xoff, yoff].TileType = LA;
            }
        }
        for (int xoff = x + 16; xoff <= x + 28; xoff++)
        {
            Main.tile[xoff, y + 1].Active(true);
            Main.tile[xoff, y + 1].TileType = LB;
        }

        Main.tile[x + 26, y + 2].Active(true);
        Main.tile[x + 27, y + 2].Active(true);
        Main.tile[x + 28, y + 2].Active(true);
        Main.tile[x + 29, y + 2].Active(true);
        Main.tile[x + 30, y + 2].Active(true);

        Main.tile[x + 26, y + 3].Active(true); // LA

        Main.tile[x + 27, y + 3].Active(true);

        Main.tile[x + 28, y + 3].Active(true); // LA

        Main.tile[x + 29, y + 3].Active(true);
        Main.tile[x + 30, y + 3].Active(true);
        Main.tile[x + 31, y + 3].Active(true);

        Main.tile[x + 26, y + 4].Active(true); // LA
        Main.tile[x + 27, y + 4].Active(true); // LA

        Main.tile[x + 28, y + 4].Active(true);
        Main.tile[x + 29, y + 4].Active(true);

        Main.tile[x + 30, y + 4].Active(true); // LA



        Main.tile[x + 11, y + 4].TileType = LB;
        Main.tile[x + 12, y + 4].TileType = LB;
        Main.tile[x + 13, y + 4].TileType = LB;

        Main.tile[x + 14, y + 4].TileType = LA; // LA

        Main.tile[x + 15, y + 4].TileType = LB;
        Main.tile[x + 16, y + 4].TileType = LB;

        Main.tile[x + 17, y + 4].TileType = LA; // LA
        Main.tile[x + 18, y + 4].TileType = LA; // LA

        Main.tile[x + 13, y + 3].TileType = LB;
        Main.tile[x + 14, y + 3].TileType = LB;
        Main.tile[x + 15, y + 3].TileType = LB;

        Main.tile[x + 16, y + 3].TileType = LA; // LA

        Main.tile[x + 17, y + 3].TileType = LB;

        Main.tile[x + 18, y + 3].TileType = LA; // LA

        Main.tile[x + 14, y + 2].TileType = LB;
        Main.tile[x + 15, y + 2].TileType = LB;
        Main.tile[x + 16, y + 2].TileType = LB;
        Main.tile[x + 17, y + 2].TileType = LB;
        Main.tile[x + 18, y + 2].TileType = LB;

        Main.tile[x + 26, y + 2].TileType = LB;
        Main.tile[x + 27, y + 2].TileType = LB;
        Main.tile[x + 28, y + 2].TileType = LB;
        Main.tile[x + 29, y + 2].TileType = LB;
        Main.tile[x + 30, y + 2].TileType = LB;

        Main.tile[x + 26, y + 3].TileType = LA; // LA

        Main.tile[x + 27, y + 3].TileType = LB;

        Main.tile[x + 28, y + 3].TileType = LA; // LA

        Main.tile[x + 29, y + 3].TileType = LB;
        Main.tile[x + 30, y + 3].TileType = LB;
        Main.tile[x + 31, y + 3].TileType = LB;

        Main.tile[x + 26, y + 4].TileType = LA; // LA
        Main.tile[x + 27, y + 4].TileType = LA; // LA

        Main.tile[x + 28, y + 4].TileType = LB;
        Main.tile[x + 29, y + 4].TileType = LB;

        Main.tile[x + 30, y + 4].TileType = LA; // LA

        Main.tile[x + 24, y + 18].Active(true);
        Main.tile[x + 24, y + 17].Active(true);
        Main.tile[x + 25, y + 17].Active(true);
        Main.tile[x + 25, y + 16].Active(true);
        Main.tile[x + 26, y + 16].Active(true);
        Main.tile[x + 27, y + 16].Active(true);
        Main.tile[x + 28, y + 16].Active(true);
        Main.tile[x + 28, y + 15].Active(true);
        Main.tile[x + 29, y + 15].Active(true);
        Main.tile[x + 30, y + 15].Active(true);
        Main.tile[x + 30, y + 14].Active(true);
        Main.tile[x + 31, y + 14].Active(true);
        Main.tile[x + 31, y + 13].Active(true);
        Main.tile[x + 32, y + 13].Active(true);
        Main.tile[x + 33, y + 13].Active(true);
        Main.tile[x + 34, y + 13].Active(true);
        Main.tile[x + 34, y + 14].Active(true);
        Main.tile[x + 35, y + 14].Active(true);
        Main.tile[x + 35, y + 15].Active(true);
        Main.tile[x + 36, y + 15].Active(true);
        Main.tile[x + 36, y + 16].Active(true);
        Main.tile[x + 37, y + 16].Active(true);
        Main.tile[x + 38, y + 16].Active(true);
        Main.tile[x + 38, y + 17].Active(true);
        Main.tile[x + 39, y + 17].Active(true);
        Main.tile[x + 40, y + 17].Active(true);
        Main.tile[x + 41, y + 17].Active(true);
        Main.tile[x + 41, y + 16].Active(true);
        Main.tile[x + 42, y + 16].Active(true);
        Main.tile[x + 42, y + 15].Active(true);
        Main.tile[x + 43, y + 15].Active(true);
        Main.tile[x + 43, y + 14].Active(true);
        Main.tile[x + 44, y + 14].Active(true);
        Main.tile[x + 44, y + 13].Active(true);
        Main.tile[x + 44, y + 12].Active(true);
        Main.tile[x + 44, y + 11].Active(true);
        Main.tile[x + 43, y + 11].Active(true);
        Main.tile[x + 43, y + 10].Active(true);
        Main.tile[x + 42, y + 10].Active(true);
        Main.tile[x + 41, y + 10].Active(true);
        Main.tile[x + 41, y + 9].Active(true);
        Main.tile[x + 41, y + 8].Active(true);
        Main.tile[x + 40, y + 8].Active(true);
        Main.tile[x + 39, y + 8].Active(true);
        Main.tile[x + 39, y + 7].Active(true);
        Main.tile[x + 39, y + 6].Active(true);
        Main.tile[x + 39, y + 5].Active(true);
        Main.tile[x + 38, y + 5].Active(true);
        Main.tile[x + 37, y + 5].Active(true);
        Main.tile[x + 37, y + 4].Active(true);
        Main.tile[x + 38, y + 4].Active(true);
        Main.tile[x + 39, y + 4].Active(true);
        Main.tile[x + 40, y + 4].Active(true);
        Main.tile[x + 36, y + 3].Active(true);
        Main.tile[x + 36, y + 2].Active(true);
        Main.tile[x + 41, y + 3].Active(true);
        Main.tile[x + 41, y + 2].Active(true);
        Main.tile[x + 35, y + 1].Active(true);
        Main.tile[x + 36, y + 1].Active(true);
        Main.tile[x + 37, y + 1].Active(true);
        Main.tile[x + 38, y + 1].Active(true);
        Main.tile[x + 39, y + 1].Active(true);
        Main.tile[x + 40, y + 1].Active(true);
        Main.tile[x + 41, y + 1].Active(true);
        Main.tile[x + 42, y + 1].Active(true);
        Main.tile[x + 35, y].Active(true);
        Main.tile[x + 38, y].Active(true);
        Main.tile[x + 39, y].Active(true);
        Main.tile[x + 42, y].Active(true);
        for (int xoff = x + 37; xoff <= x + 40; xoff++)
        {
            for (int yoff = y + 2; yoff <= y + 3; yoff++)
            {
                Main.tile[xoff, yoff].Active(true);
                Main.tile[xoff, yoff].TileType = LA;
            }
        }
        Main.tile[x + 24, y + 18].TileType = LB;
        Main.tile[x + 24, y + 17].TileType = LB;
        Main.tile[x + 25, y + 17].TileType = LB;
        Main.tile[x + 25, y + 16].TileType = LB;
        Main.tile[x + 26, y + 16].TileType = LB;
        Main.tile[x + 27, y + 16].TileType = LB;
        Main.tile[x + 28, y + 16].TileType = LB;
        Main.tile[x + 28, y + 15].TileType = LB;
        Main.tile[x + 29, y + 15].TileType = LB;
        Main.tile[x + 30, y + 15].TileType = LB;
        Main.tile[x + 30, y + 14].TileType = LB;
        Main.tile[x + 31, y + 14].TileType = LB;
        Main.tile[x + 31, y + 13].TileType = LB;
        Main.tile[x + 32, y + 13].TileType = LB;
        Main.tile[x + 33, y + 13].TileType = LB;
        Main.tile[x + 34, y + 13].TileType = LB;
        Main.tile[x + 34, y + 14].TileType = LB;
        Main.tile[x + 35, y + 14].TileType = LB;
        Main.tile[x + 35, y + 15].TileType = LB;
        Main.tile[x + 36, y + 15].TileType = LB;
        Main.tile[x + 36, y + 16].TileType = LB;
        Main.tile[x + 37, y + 16].TileType = LB;
        Main.tile[x + 38, y + 16].TileType = LB;
        Main.tile[x + 38, y + 17].TileType = LB;
        Main.tile[x + 39, y + 17].TileType = LB;
        Main.tile[x + 40, y + 17].TileType = LB;
        Main.tile[x + 41, y + 17].TileType = LB;
        Main.tile[x + 41, y + 16].TileType = LB;
        Main.tile[x + 42, y + 16].TileType = LB;
        Main.tile[x + 42, y + 15].TileType = LB;
        Main.tile[x + 43, y + 15].TileType = LB;
        Main.tile[x + 43, y + 14].TileType = LB;
        Main.tile[x + 44, y + 14].TileType = LB;
        Main.tile[x + 44, y + 13].TileType = LB;
        Main.tile[x + 44, y + 12].TileType = LB;
        Main.tile[x + 44, y + 11].TileType = LB;
        Main.tile[x + 43, y + 11].TileType = LB;
        Main.tile[x + 43, y + 10].TileType = LB;
        Main.tile[x + 42, y + 10].TileType = LB;
        Main.tile[x + 41, y + 10].TileType = LB;
        Main.tile[x + 41, y + 9].TileType = LB;
        Main.tile[x + 41, y + 8].TileType = LB;
        Main.tile[x + 40, y + 8].TileType = LB;
        Main.tile[x + 39, y + 8].TileType = LB;
        Main.tile[x + 39, y + 7].TileType = LB;
        Main.tile[x + 39, y + 6].TileType = LB;
        Main.tile[x + 39, y + 5].TileType = LB;
        Main.tile[x + 38, y + 5].TileType = LB;
        Main.tile[x + 37, y + 5].TileType = LB;
        Main.tile[x + 37, y + 4].TileType = LB;
        Main.tile[x + 38, y + 4].TileType = LB;
        Main.tile[x + 39, y + 4].TileType = LB;
        Main.tile[x + 40, y + 4].TileType = LB;
        Main.tile[x + 36, y + 3].TileType = LB;
        Main.tile[x + 36, y + 2].TileType = LB;
        Main.tile[x + 41, y + 3].TileType = LB;
        Main.tile[x + 41, y + 2].TileType = LB;
        Main.tile[x + 35, y + 1].TileType = LB;
        Main.tile[x + 36, y + 1].TileType = LB;
        Main.tile[x + 37, y + 1].TileType = LB;
        Main.tile[x + 38, y + 1].TileType = LB;
        Main.tile[x + 39, y + 1].TileType = LB;
        Main.tile[x + 40, y + 1].TileType = LB;
        Main.tile[x + 41, y + 1].TileType = LB;
        Main.tile[x + 42, y + 1].TileType = LB;
        Main.tile[x + 35, y].TileType = LB;
        Main.tile[x + 38, y].TileType = LB;
        Main.tile[x + 39, y].TileType = LB;
        Main.tile[x + 42, y].TileType = LB;

        for (int xoff = x + 6; xoff <= x + 38; xoff++)
        {
            for (int yoff = y + 6; yoff <= y + 12; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 17; xoff <= x + 27; xoff++)
        {
            for (int yoff = y + 13; yoff <= y + 15; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 15; xoff <= x + 29; xoff++)
        {
            for (int yoff = y + 13; yoff <= y + 14; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 20; xoff <= x + 24; xoff++)
        {
            Main.tile[xoff, y + 16].Active(false);
            Main.tile[xoff, y + 16].WallType = backWall;
        }

        Main.tile[x + 14, y + 13].Active(true);
        Main.tile[x + 15, y + 14].Active(true);
        Main.tile[x + 16, y + 14].Active(true);
        Main.tile[x + 17, y + 15].Active(true);
        Main.tile[x + 18, y + 15].Active(true);
        Main.tile[x + 26, y + 15].Active(true);
        Main.tile[x + 27, y + 15].Active(true);
        Main.tile[x + 28, y + 14].Active(true);
        Main.tile[x + 29, y + 14].Active(true);
        Main.tile[x + 30, y + 13].Active(true);

        Main.tile[x + 14, y + 13].TileType = 19;
        Main.tile[x + 15, y + 14].TileType = 19;
        Main.tile[x + 16, y + 14].TileType = 19;
        Main.tile[x + 17, y + 15].TileType = 19;
        Main.tile[x + 18, y + 15].TileType = 19;
        Main.tile[x + 26, y + 15].TileType = 19;
        Main.tile[x + 27, y + 15].TileType = 19;
        Main.tile[x + 28, y + 14].TileType = 19;
        Main.tile[x + 29, y + 14].TileType = 19;
        Main.tile[x + 30, y + 13].TileType = 19;

        Main.tile[x + 14, y + 13].WallType = backWall;
        Main.tile[x + 30, y + 13].WallType = backWall;

        WorldGen.PlaceTile(x + 12, y + 12, 4, true, true, -1, 2);
        WorldGen.PlaceTile(x + 32, y + 12, 4, true, true, -1, 2);

        for (int xoff = x + 2; xoff <= x + 8; xoff++)
        {
            for (int yoff = y + 11; yoff <= y + 14; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 36; xoff <= x + 42; xoff++)
        {
            for (int yoff = y + 11; yoff <= y + 14; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 3; xoff <= x + 7; xoff++)
        {
            Main.tile[xoff, y + 15].Active(false);
            Main.tile[xoff, y + 15].WallType = backWall;
        }
        for (int xoff = x + 37; xoff <= x + 41; xoff++)
        {
            Main.tile[xoff, y + 15].Active(false);
            Main.tile[xoff, y + 15].WallType = backWall;
        }
        Main.tile[x + 4, y + 16].Active(false);
        Main.tile[x + 5, y + 16].Active(false);
        Main.tile[x + 39, y + 16].Active(false);
        Main.tile[x + 40, y + 16].Active(false);
        Main.tile[x + 9, y + 13].Active(false);
        Main.tile[x + 35, y + 13].Active(false);

        Main.tile[x + 4, y + 16].WallType = backWall;
        Main.tile[x + 5, y + 16].WallType = backWall;
        Main.tile[x + 39, y + 16].WallType = backWall;
        Main.tile[x + 40, y + 16].WallType = backWall;
        Main.tile[x + 9, y + 13].WallType = backWall;
        Main.tile[x + 35, y + 13].WallType = backWall;

        for (int xoff = x + 4; xoff <= x + 5; xoff++)
        {
            for (int yoff = y + 9; yoff <= y + 10; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 39; xoff <= x + 40; xoff++)
        {
            for (int yoff = y + 9; yoff <= y + 10; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 6; xoff <= x + 10; xoff++)
        {
            Main.tile[xoff, y + 8].Active(true);
            Main.tile[xoff, y + 8].TileType = MO;
        }
        for (int xoff = x + 34; xoff <= x + 38; xoff++)
        {
            Main.tile[xoff, y + 8].Active(true);
            Main.tile[xoff, y + 8].TileType = MO;
        }
        Main.tile[x + 10, y + 9].Active(true);
        Main.tile[x + 34, y + 9].Active(true);
        Main.tile[x + 10, y + 9].TileType = MO;
        Main.tile[x + 34, y + 9].TileType = MO;

        WorldGen.PlaceTile(x + 6, y + 6, 4, true, true, -1, 2);
        WorldGen.PlaceTile(x + 38, y + 6, 4, true, true, -1, 2);

        for (int xoff = x + 21; xoff <= x + 23; xoff++)
        {
            for (int yoff = y + 18; yoff <= y + 21; yoff++)
            {
                Main.tile[xoff, yoff].Active(false);
                Main.tile[xoff, yoff].WallType = backWall;
            }
        }
        for (int xoff = x + 20; xoff <= x + 24; xoff++)
        {
            Main.tile[xoff, y + 22].Active(true);
            Main.tile[xoff, y + 22].TileType = LB;
        }
        for (int xoff = x + 21; xoff <= x + 23; xoff++)
        {
            Main.tile[xoff, y + 23].Active(true);
            Main.tile[xoff, y + 23].TileType = LB;
        }

        WorldGen.PlaceTile(x + 22, y + 21, 4, true, true, -1, 2);

        WorldGen.PlaceDoor(x + 20, y + 20, 10, 19);
        WorldGen.PlaceDoor(x + 24, y + 20, 10, 19);
        WorldGen.PlaceDoor(x + 10, y + 11, 10, 19);
        WorldGen.PlaceDoor(x + 34, y + 11, 10, 19);
        for (int xoff = x + 21; xoff <= x + 23; xoff++)
        {
            Main.tile[xoff, y + 17].Active(true);
            Main.tile[xoff, y + 17].TileType = 19;
            Main.tile[xoff, y + 17].WallType = backWall;
        }
        for (int xoff = x + 31; xoff <= x + 33; xoff++)
        {
            Main.tile[xoff, y + 4].Active(true);
            Main.tile[xoff, y + 4].TileType = LB;
        }
        Main.tile[x + 43, y + 12].Active(false);
        Main.tile[x + 43, y + 13].Active(false);
        Main.tile[x + 1, y + 12].Active(false);
        Main.tile[x + 1, y + 13].Active(false);
        Main.tile[x + 43, y + 12].WallType = backWall;
        Main.tile[x + 43, y + 13].WallType = backWall;
        Main.tile[x + 1, y + 12].WallType = backWall;
        Main.tile[x + 1, y + 13].WallType = backWall;

        WorldGen.PlaceTile(x + 43, y + 12, 4, true, true, -1, 2);
        WorldGen.PlaceTile(x + 1, y + 12, 4, true, true, -1, 2);
        //WorldGen.SquareTileFrameArea(x, y, 44);
        AddLavaChest(x + 5, y + 15, 0, false, 1);
        AddLavaChest(x + 40, y + 17, 0, false, 1);
    }

    public static bool AddLavaChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
    {
        //if (WorldGen.genRand == null)
        //    WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
        int k = j;
        while (k < Main.maxTilesY)
        {
            if (Main.tile[i, k].HasTile && Main.tileSolid[(int)Main.tile[i, k].TileType])
            {
                int num = k;
                int num2 = WorldGen.PlaceChest(i - 1, num - 1, (ushort)ModContent.TileType<Tiles.Furniture.HellfireChest>(), notNearOtherChests, 0);
                if (num2 >= 0)
                {
                    int num3 = 0;
                    while (num3 == 0)
                    {
                        int rN = WorldGen.genRand.Next(42);
                        if (rN >= 0 && rN <= 20)
                        {
                            Main.chest[num2].item[0].SetDefaults(174, false);
                            Main.chest[num2].item[0].stack = WorldGen.genRand.Next(41, 68);
                        }
                        else if (rN >= 21 && rN <= 41)
                        {
                            Main.chest[num2].item[0].SetDefaults(175, false);
                            Main.chest[num2].item[0].stack = WorldGen.genRand.Next(2, 7);
                        }
                        int rand = WorldGen.genRand.Next(51);
                        if (rand >= 0 && rand <= 20)
                        {
                            int r = WorldGen.genRand.Next(4);
                            if (r == 0) r = ItemID.HellwingBow;
                            else if (r == 1) r = ItemID.Flamelash;
                            else if (r == 2) r = ItemID.FlowerofFire;
                            else r = ItemID.Sunfury;
                            Main.chest[num2].item[1].SetDefaults(r, false);
                            Main.chest[num2].item[1].Prefix(-2);
                        }
                        else if (rand >= 21 && rand <= 40)
                        {
                            Main.chest[num2].item[1].SetDefaults(ModContent.ItemType<AuraPotion>(), false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(3) + 1;
                        }
                        else if (rand >= 41 && rand <= 50)
                        {
                            Main.chest[num2].item[1].SetDefaults(ModContent.ItemType<ShockwavePotion>(), false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(3) + 1;
                        }
                        int rand2 = WorldGen.genRand.Next(27);
                        if (rand2 >= 0 && rand2 <= 25)
                        {
                            Main.chest[num2].item[2].SetDefaults(73, false);
                            Main.chest[num2].item[2].stack = WorldGen.genRand.Next(20, 31);
                        }
                        else if (rand2 == 26)
                        {
                            Main.chest[num2].item[2].SetDefaults(ItemID.LavaWaders, false);
                            Main.chest[num2].item[2].Prefix(-2);
                        }
                        int rand3 = WorldGen.genRand.Next(27);
                        if (rand3 >= 0 && rand2 <= 10)
                        {
                            Main.chest[num2].item[3].SetDefaults(ItemID.LavaCharm, false);
                            Main.chest[num2].item[3].Prefix(-2);
                        }
                        else if (rand3 >= 11 && rand2 <= 25)
                        {
                            Main.chest[num2].item[3].SetDefaults(ModContent.ItemType<BagofFire>(), false);
                        }
                        else if (rand3 == 26)
                        {
                            Main.chest[num2].item[3].SetDefaults(73, false);
                            Main.chest[num2].item[3].stack = WorldGen.genRand.Next(20, 34);
                        }
                        num3++;
                    }
                    return true;
                }
                return false;
            }
            else k++;
        }
        return false;
    }
}
