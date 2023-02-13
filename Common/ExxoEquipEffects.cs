using ExxoAvalonOrigins.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common
{
    public class ExxoEquipEffects : ModPlayer
    {
        public bool BloodyWhetstone;
        public override void ResetEffects()
        {
            BloodyWhetstone = false;
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.DamageType == DamageClass.Melee && BloodyWhetstone)
            {
                if (!target.HasBuff<Bleeding>())
                {
                    target.GetGlobalNPC<AvalonGlobalNPCInstance>().BleedStacks = 1;
                }

                target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
            }
        }
    }
}
