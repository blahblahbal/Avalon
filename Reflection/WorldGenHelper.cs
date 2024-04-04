using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class WorldGenHelper
{
    public delegate bool GrowMoreVinesDelegate(int x, int y);
    public static readonly GrowMoreVinesDelegate GrowMoreVines = Utilities.CacheStaticMethod<GrowMoreVinesDelegate>(typeof(WorldGen).GetMethod("GrowMoreVines", BindingFlags.Static | BindingFlags.NonPublic)!);

    //public delegate bool SpawnFallingBlockProjectileDelegate(int i, int j, Tile tileCache, Tile tileTopCache, Tile tileBottomCache, int type);
    //public static readonly SpawnFallingBlockProjectileDelegate SpawnFallingBlockProjectile = Utilities.CacheStaticMethod<SpawnFallingBlockProjectileDelegate>(typeof(WorldGen).GetMethod("SpawnFallingBlockProjectile", BindingFlags.Static | BindingFlags.NonPublic)!);
}
