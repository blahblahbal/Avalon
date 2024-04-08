using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class WorldGenHelper
{
    public delegate bool GrowMoreVinesDelegate(int x, int y);
    public static readonly GrowMoreVinesDelegate GrowMoreVines = Utilities.CacheStaticMethod<GrowMoreVinesDelegate>(typeof(WorldGen).GetMethod("GrowMoreVines", BindingFlags.Static | BindingFlags.NonPublic)!);
}
