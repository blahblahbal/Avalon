using System.Linq.Expressions;
using System.Reflection;
using Terraria;

namespace Avalon.WorldGeneration.Helpers;

public static class PlantsHelper
{
    public delegate void WorldGenPlantCheckDelegate(int x, int y);

    private static WorldGenPlantCheckDelegate CacheWorldGenPlantCheckMethod()
    {
        var methodInfo = typeof(WorldGen).GetMethod("PlantCheck", BindingFlags.Static | BindingFlags.Public)!;

        var xParameter = Expression.Parameter(typeof(int), "x");
        var yParameter = Expression.Parameter(typeof(int), "y");

        var methodCallExpression = Expression.Call(methodInfo, xParameter, yParameter);

        return Expression.Lambda<WorldGenPlantCheckDelegate>(methodCallExpression, xParameter, yParameter).Compile();
    }

    public static readonly WorldGenPlantCheckDelegate WorldGenPlantCheck = CacheWorldGenPlantCheckMethod();
}
