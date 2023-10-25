using System.Reflection;
using Terraria;

namespace Avalon.Reflection;

public static class PlayerHelper
{
    private delegate void ClearMiningCacheAtDelegate(Player self, int x, int y, int hitTileCacheType);
    private static readonly ClearMiningCacheAtDelegate CachedClearMiningCacheAt =
        Utilities.CacheInstanceMethod<ClearMiningCacheAtDelegate>(typeof(Player).GetMethod("ClearMiningCacheAt", BindingFlags.Instance | BindingFlags.NonPublic)!);
    public static void ClearMiningCacheAt(this Player player, int x, int y, int hitTileCacheType) => CachedClearMiningCacheAt(player, x, y, hitTileCacheType);


    private delegate void TryFloatingOnWaterDelegate(Player self);
    private static readonly TryFloatingOnWaterDelegate CachedTryFloatingOnWater =
        Utilities.CacheInstanceMethod<TryFloatingOnWaterDelegate>(typeof(Player).GetMethod("TryFloatingInFluid", BindingFlags.Instance | BindingFlags.NonPublic)!);
    public static void TryFloatingInFluid(this Player player) => CachedTryFloatingOnWater(player);

    private delegate bool IsBottomOfTreeTrunkNoRootsDelegate(Player self, int x, int y);
    private static readonly IsBottomOfTreeTrunkNoRootsDelegate CachedIsBottomOfTreeTrunkNoRoots =
        Utilities.CacheInstanceMethod<IsBottomOfTreeTrunkNoRootsDelegate>(typeof(Player).GetMethod("IsBottomOfTreeTrunkNoRoots", BindingFlags.Instance | BindingFlags.NonPublic)!);
    public static bool IsBottomOfTreeTrunkNoRoots(this Player player, int x, int y) => CachedIsBottomOfTreeTrunkNoRoots(player, x, y);
    
    private delegate void TryReplantingTreeDelegate(Player self, int x, int y);
    private static readonly TryReplantingTreeDelegate CachedTryReplantingTree =
        Utilities.CacheInstanceMethod<TryReplantingTreeDelegate>(typeof(Player).GetMethod("TryReplantingTree", BindingFlags.Instance | BindingFlags.NonPublic)!);
    public static void TryReplantingTree(this Player player, int x, int y) => CachedTryReplantingTree(player, x, y);
}
