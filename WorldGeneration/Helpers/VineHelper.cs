using System.Linq.Expressions;
using System.Reflection;
using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Helpers;

// TODO: ChlorophyteDefense, hardUpdateWorld spread, RandomUpdate spread from wall
public class VineHelper
{
    private delegate bool WorldGenGrowMoreVinesDelegate(int x, int y);

    private static WorldGenGrowMoreVinesDelegate CacheWorldGenGrowMoreVinesMethod()
    {
        var methodInfo = typeof(WorldGen).GetMethod("GrowMoreVines", BindingFlags.Static | BindingFlags.NonPublic)!;

        var xParameter = Expression.Parameter(typeof(int), "x");
        var yParameter = Expression.Parameter(typeof(int), "y");

        var methodCallExpression = Expression.Call(methodInfo, xParameter, yParameter);

        return Expression.Lambda<WorldGenGrowMoreVinesDelegate>(methodCallExpression, xParameter, yParameter).Compile();
    }

    private static readonly WorldGenGrowMoreVinesDelegate WorldGenGrowMoreVines = CacheWorldGenGrowMoreVinesMethod();

    public static void VineRandomUpdate(int x, int y, int oneInChanceVine, int oneInChanceOther)
    {
        if (!Main.tile[x, y].HasUnactuatedTile)
            return;

        if (Main.tile[x, y].TileType != ModContent.TileType<Ickgrass>() &&
            Main.tile[x, y].TileType != ModContent.TileType<ContagionJungleGrass>() &&
            Main.tile[x, y].TileType != ModContent.TileType<ContagionVines>() ||
            !WorldGenGrowMoreVines(x, y))
            return;

        var oneInChance = oneInChanceOther;
        if (Main.tile[x, y].TileType == ModContent.TileType<ContagionVines>())
        {
            oneInChance = oneInChanceVine;
        }

        if (!WorldGen.genRand.NextBool(oneInChance) || Main.tile[x, y + 1].HasTile || Main.tile[x, y + 1].LiquidType == LiquidID.Lava)
            return;

        var canGrow = false;

        const int floorDistance = 10;
        for (var j = y; j > y - floorDistance; j--)
        {
            if (Main.tile[x, j].BottomSlope)
            {
                canGrow = false;
                break;
            }
            if (Main.tile[x, j].HasTile && (Main.tile[x, j].TileType == ModContent.TileType<Ickgrass>() || Main.tile[x, j].TileType == ModContent.TileType<ContagionJungleGrass>()) && !Main.tile[x, j].BottomSlope)
            {
                canGrow = true;
                break;
            }
        }

        if (!canGrow)
            return;

        var yBelow = y + 1;
        Main.tile[x, yBelow].TileType = (ushort)ModContent.TileType<ContagionVines>();
        Main.tile[x, yBelow].Active(true);
        Main.tile[x, yBelow].CopyPaintAndCoating(Main.tile[x, y]);
        WorldGen.SquareTileFrame(x, yBelow);

        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendTileSquare(-1, x, yBelow);
        }
    }
}
