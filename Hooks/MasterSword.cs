using Avalon.Common;
using Terraria.GameContent.Achievements;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    public class MasterSword : ModHook
    {
        protected override void Apply()
        {
            On_AchievementsHelper.NotifyProgressionEvent += OnNotifyProgressionEvent;
        }

        private static void OnNotifyProgressionEvent(On_AchievementsHelper.orig_NotifyProgressionEvent orig, int eventID)
        {
            if (eventID == 21)
            {
                int i = Item.NewItem(Player.GetSource_NaturalSpawn(), Main.LocalPlayer.position, ModContent.ItemType<Items.Weapons.Melee.Hardmode.DarklightLance>());
                Main.item[i].playerIndexTheItemIsReservedFor = Main.LocalPlayer.whoAmI;
            }
            orig(eventID);
        }
    }
}
