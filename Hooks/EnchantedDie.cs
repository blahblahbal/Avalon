using Avalon.Common;
using System;
using Terraria;

namespace Avalon.Hooks
{
    internal class EnchantedDie : ModHook
    {
        protected override void Apply()
        {
            On_Main.DamageVar_float_int_float += OnDamageVarFIF;
        }
        private static int OnDamageVarFIF(On_Main.orig_DamageVar_float_int_float orig, float dmg, int percent, float luck)
        {
            if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().EnchantedDie)
            {
                float num = dmg * (1f + Main.rand.Next(-45, 46) * 0.01f);
                if (luck > 0f)
                {
                    if (Main.rand.NextFloat() < luck)
                    {
                        float num2 = dmg * (1f + Main.rand.Next(-45, 46) * 0.01f);
                        if (num2 > num)
                        {
                            num = num2;
                        }
                    }
                }
                else if (luck < 0f && Main.rand.NextFloat() < 0f - luck)
                {
                    float num3 = dmg * (1f + Main.rand.Next(-45, 46) * 0.01f);
                    if (num3 < num)
                    {
                        num = num3;
                    }
                }
                return (int)Math.Round(num);
            }
            return orig(dmg, percent, luck);
        }
    }
}
