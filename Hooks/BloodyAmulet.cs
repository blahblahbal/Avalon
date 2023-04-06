using Avalon.Common;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace Avalon.Hooks;

internal class BloodyAmulet : ModHook
{
    protected override void Apply()
    {
        On_Player.ItemCheck_CheckCanUse += OnItemCheck_CheckCanUse;
        On_Player.ItemCheck_UseEventItems += OnItemCheck_UseEventItems;
    }
    private static bool OnItemCheck_CheckCanUse(On_Player.orig_ItemCheck_CheckCanUse orig, Player self, Item sItem)
    {
        if (sItem.type == ItemID.BloodMoonStarter)
        {
            return true;
        }
        return orig(self, sItem);
    }
    private static void OnItemCheck_UseEventItems(On_Player.orig_ItemCheck_UseEventItems orig, Player self, Item sItem)
    {
        if (self.ItemTimeIsZero && self.itemAnimation > 0 && sItem.type == ItemID.BloodMoonStarter)
        {
            SoundEngine.PlaySound(SoundID.Roar, self.position);
            self.ApplyItemTime(sItem);
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                AchievementsHelper.NotifyProgressionEvent(4);
                Main.dayTime = false;
                Main.time = 0;
                Main.bloodMoon = true;
                Main.NewText(Lang.misc[8].Value, 50, byte.MaxValue, 130);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, self.whoAmI, -10f);
            }
        }
        else orig(self, sItem);
    }
}
