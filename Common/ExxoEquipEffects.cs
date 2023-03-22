using Avalon.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common; 

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