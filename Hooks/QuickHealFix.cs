using Avalon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Avalon.Items.Potions.Buff;

namespace Avalon.Hooks
{
    internal class QuickHealFix : ModHook
    {
        protected override void Apply()
        {
            On_Player.QuickHeal_GetItemToUse += On_Player_QuickHeal_GetItemToUse;
        }

        private Item On_Player_QuickHeal_GetItemToUse(On_Player.orig_QuickHeal_GetItemToUse orig, Player self)
        {
            int currentHealth = self.statLifeMax2 - self.statLife;
            Item result = null;
            int negativeMaxLife = -self.statLifeMax2;
            int num3 = 58;
            if (self.useVoidBag())
            {
                num3 = 98;
            }
            for (int i = 0; i < num3; i++)
            {
                Item item = ((i >= 58) ? self.bank4.item[i - 58] : self.inventory[i]);
                if (item.type == ModContent.ItemType<RejuvenationPotion>()) goto thingy;
                if (item.stack <= 0 || item.type <= ItemID.None || !item.potion || item.healLife <= 0)
                {
                    continue;
                }
                thingy:
                int num4 = item.healLife - currentHealth;
                if (item.type == ItemID.RestorationPotion && num4 < 0)
                {
                    num4 += 30;
                    if (num4 > 0)
                    {
                        num4 = 0;
                    }
                }
                //else if (item.type == ModContent.ItemType<RejuvenationPotion>() && num4 < 0)
                //{
                //    num4 += 30;
                //    if (num4 > 0)
                //    {
                //        num4 = 0;
                //    }
                //}
                if (negativeMaxLife < 0)
                {
                    if (num4 > negativeMaxLife)
                    {
                        result = item;
                        negativeMaxLife = num4;
                    }
                }
                else if (num4 < negativeMaxLife && num4 >= 0)
                {
                    result = item;
                    negativeMaxLife = num4;
                }
            }
            return result;
        }
    }
}
