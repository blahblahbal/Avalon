using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using System;

namespace Avalon.Hooks;

internal class WallWandHook : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.PlaceWall += On_WorldGen_PlaceWall;
    }

    private void On_WorldGen_PlaceWall(On_WorldGen.orig_PlaceWall orig, int i, int j, int type, bool mute)
    {
        orig.Invoke(i, j, type, mute);
        if (Main.wallDungeon[type] && !WorldGen.generatingWorld &&
            Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ModContent.ItemType<Items.Tools.PreHardmode.DungeonWand>() &&
            Main.tile[i, j].WallType == type)
        {
            for (int q = 0; q < Main.LocalPlayer.inventory.Length; q++)
            {
                int itemType = Main.LocalPlayer.inventory[q].type;
                if (Data.Sets.ItemSets.DungeonWallItems[itemType]) // TYPE == ItemID.GreenTiledWall || TYPE == ItemID.GreenBrickWall || TYPE == ItemID.GreenSlabWall)
                {
                    Main.LocalPlayer.inventory[q].stack--;
                    if (Main.LocalPlayer.inventory[q].stack <= 0)
                    {
                        Main.LocalPlayer.inventory[q] = new Item();
                    }
                    break;
                }
            }
        }
    }
}
