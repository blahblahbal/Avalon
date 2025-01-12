using System;
using Avalon.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Helpers;

// TODO: ChlorophyteDefense, hardUpdateWorld spread, RandomUpdate spread from wall
public static class VinesHelper
{
    public static void VinesRandomUpdate<TVines>(int x, int y, int oneInChanceVine, int oneInChanceOther, Func<int, bool> canGrowFromTile) where TVines : ModTile
    {
        if (!Main.tile[x, y].HasUnactuatedTile ||
            Main.tile[x, y].TileType != ModContent.TileType<TVines>() &&
            !canGrowFromTile(Main.tile[x, y].TileType) ||
            !WorldGen.GrowMoreVines(x, y))
        {
            return;
        }

        var oneInChance = oneInChanceOther;
        if (Main.tile[x, y].TileType == ModContent.TileType<TVines>())
        {
            oneInChance = oneInChanceVine;
        }

        if (!WorldGen.genRand.NextBool(oneInChance) || Main.tile[x, y + 1].HasTile || Main.tile[x, y + 1].LiquidType == LiquidID.Lava)
            return;

        var canGrow = false;

        const int floorDistance = 10;
        for (var j = y; j > y - floorDistance; j--)
        {
            if (Main.tile[x, j].BottomSlope || Main.tile[x, j].HasTile && canGrowFromTile(Main.tile[x, j].TileType) && !Main.tile[x, j].BottomSlope)
            {
                canGrow = true;
                break;
            }
        }

        if (!canGrow)
            return;

        var yBelow = y + 1;
        Main.tile[x, yBelow].TileType = (ushort)ModContent.TileType<TVines>();
        Main.tile[x, yBelow].Active(true);
        Main.tile[x, yBelow].CopyPaintAndCoating(Main.tile[x, y]);
        WorldGen.SquareTileFrame(x, yBelow);

        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendTileSquare(-1, x, yBelow);
        }
    }
}
