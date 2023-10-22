using System.Reflection;
using Avalon.Hooks;
using Terraria;

namespace Avalon.WorldGeneration.Helpers;

public static class WorldGenHelper
{
    public delegate bool WorldGenGrowMoreVinesDelegate(int x, int y);
    public static readonly WorldGenGrowMoreVinesDelegate WorldGenGrowMoreVines = Utilities.CacheStaticMethod<WorldGenGrowMoreVinesDelegate>(typeof(WorldGen).GetMethod("GrowMoreVines", BindingFlags.Static | BindingFlags.NonPublic)!);
}
