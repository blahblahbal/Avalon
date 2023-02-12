using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common;

public class AvalonPlayer : ModPlayer
{
    public Vector2 MousePosition;
    public float MagicCritDamage = 1f;
    public float MeleeCritDamage = 1f;
    public float RangedCritDamage = 1f;
    public bool AdjShimmer;
    public bool oldAdjShimmer;
    public override void ResetEffects()
    {
        MagicCritDamage = 1f;
        MeleeCritDamage = 1f;
        RangedCritDamage = 1f;
    }
    public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyMeleeCritDamage(damage);
        }
    }

    public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (crit)
        {
            if (proj.DamageType == DamageClass.Magic)
            {
                damage += MultiplyMagicCritDamage(damage);
            }
            if (proj.DamageType == DamageClass.Melee)
            {
                damage += MultiplyMeleeCritDamage(damage);
            }
            if (proj.DamageType == DamageClass.Ranged)
            {
                damage += MultiplyRangedCritDamage(damage);
            }
        }
    }
    public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
    {
        if (crit)
        {
            damage += MultiplyMeleeCritDamage(damage);
        }
    }
    public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
    {
        if (crit)
        {
            if (proj.DamageType == DamageClass.Magic)
            {
                damage += MultiplyMagicCritDamage(damage);
            }
            if (proj.DamageType == DamageClass.Melee)
            {
                damage += MultiplyMeleeCritDamage(damage);
            }
            if (proj.DamageType == DamageClass.Ranged)
            {
                damage += MultiplyRangedCritDamage(damage);
            }
        }
    }

    #region crit dmg stuff
    public int MultiplyMagicCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? MagicCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }

    public int MultiplyMeleeCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? MeleeCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }
    public int MultiplyRangedCritDamage(int dmg, float mult = 0f) // dmg = damage before crit application
    {
        int bonusDmg = -dmg;
        bonusDmg += (int)(dmg * ((mult == 0f ? RangedCritDamage : mult) + 1f) / 2);
        return bonusDmg;
    }
    public void AllCritDamage(float value)
    {
        MagicCritDamage += value;
        MeleeCritDamage += value;
        RangedCritDamage += value;
    }
    #endregion crit dmg stuff
}
