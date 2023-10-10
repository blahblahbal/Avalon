using Terraria;
using Avalon.Common;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class ChestHook : ModHook
    {
        protected override void Apply()
        {
            On_Chest.IsLocked_int_int_Tile += OnIsLocked;
        }
        private static bool OnIsLocked(On_Chest.orig_IsLocked_int_int_Tile orig, int i, int j, Tile t)
        {
            if (t != null)
            {
                if (t.TileType == ModContent.TileType<Tiles.Furniture.LockedChests>()) return true;
            }
            return orig(i, j, t);
        }
    }
}
