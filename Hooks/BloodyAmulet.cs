using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks;

internal class BloodyAmulet : ModHook
{
    protected override void Apply()
    {
        //On_Player.ItemCheck_CheckCanUse += OnItemCheck_CheckCanUse;
        //On_Player.ItemCheck_UseEventItems += OnItemCheck_UseEventItems;
        On_Item.NewItem_Inner += On_Item_NewItem_Inner;
    }

    private int On_Item_NewItem_Inner(On_Item.orig_NewItem_Inner orig, Terraria.DataStructures.IEntitySource source, int X, int Y, int Width, int Height, Item itemToClone, int Type, int Stack, bool noBroadcast, int pfix, bool noGrabDelay, bool reverseLookup)
    {
        if (Type == ItemID.BloodMoonStarter)
        {
            Type = ModContent.ItemType<Items.Consumables.BloodyAmulet>();
        }
        return orig.Invoke(source, X, Y, Width, Height, itemToClone, Type, Stack, noBroadcast, pfix, noGrabDelay, reverseLookup);
    }

    // left in, in case we want to try fixing later

    //private static bool OnItemCheck_CheckCanUse(On_Player.orig_ItemCheck_CheckCanUse orig, Player self, Item sItem)
    //{
    //    if (sItem.type == ItemID.BloodMoonStarter)
    //    {
    //        return true;
    //    }
    //    return orig.Invoke(self, sItem);
    //}
    //private static void OnItemCheck_UseEventItems(On_Player.orig_ItemCheck_UseEventItems orig, Player self, Item sItem)
    //{
    //    if (self.ItemTimeIsZero && self.itemAnimation > 0 && sItem.type == ItemID.BloodMoonStarter)
    //    {
    //        if (Main.myPlayer == self.whoAmI)
    //        {
    //            Projectile.NewProjectile(self.GetSource_FromThis(), self.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Tools.BloodyAmulet>(), 0, 0, self.whoAmI);
    //        }
    //    }
    //    else orig.Invoke(self, sItem);
    //}
}
