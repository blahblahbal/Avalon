using System.Linq.Expressions;
using System.Reflection;
using Terraria;

namespace Avalon.WorldGeneration.Helpers;

public static class WorldGenHelper
{
    public delegate bool WorldGenGrowMoreVinesDelegate(int x, int y);

    private static WorldGenGrowMoreVinesDelegate CacheWorldGenGrowMoreVinesMethod()
    {
        var methodInfo = typeof(WorldGen).GetMethod("GrowMoreVines", BindingFlags.Static | BindingFlags.NonPublic)!;

        var xParameter = Expression.Parameter(typeof(int), "x");
        var yParameter = Expression.Parameter(typeof(int), "y");

        var methodCallExpression = Expression.Call(methodInfo, xParameter, yParameter);

        return Expression.Lambda<WorldGenGrowMoreVinesDelegate>(methodCallExpression, xParameter, yParameter).Compile();
    }

    public static readonly WorldGenGrowMoreVinesDelegate WorldGenGrowMoreVines = CacheWorldGenGrowMoreVinesMethod();
}
