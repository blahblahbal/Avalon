using Terraria;
using Avalon.Common;
using Terraria.ModLoader;
using Avalon.Tiles.Furniture.OrangeDungeon;
using Avalon.Tiles.Furniture.PurpleDungeon;
using Avalon.Tiles.Furniture.YellowDungeon;

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
                if ((t.TileType == ModContent.TileType<OrangeDungeonChest>() ||
					t.TileType == ModContent.TileType<PurpleDungeonChest>() ||
					t.TileType == ModContent.TileType<YellowDungeonChest>() ||
					t.TileType == ModContent.TileType<Tiles.Contagion.ContagionChest>() ||
					t.TileType == ModContent.TileType<Tiles.Furniture.UnderworldChest>()) && t.TileFrameX > 34) return true;
			}
            return orig(i, j, t);
        }
    }
}
