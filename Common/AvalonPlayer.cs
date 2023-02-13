using ExxoAvalonOrigins.Items.Other;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
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
    private int gemCount;
    public bool[] OwnedLargeGems = new bool[10];
    public override void ResetEffects()
    {
        MagicCritDamage = 1f;
        MeleeCritDamage = 1f;
        RangedCritDamage = 1f;
    }

    public override void PostUpdate()
    {
        // Large gem inventory checking
        Player.gemCount = 0;
        gemCount++;
        if (gemCount >= 10)
        {
            Player.gem = -1;
            OwnedLargeGems = new bool[10];
            gemCount = 0;
            for (int num27 = 0; num27 <= 58; num27++)
            {
                if (Player.inventory[num27].type == ItemID.None || Player.inventory[num27].stack == 0)
                {
                    Player.inventory[num27].TurnToAir();
                }

                // Vanilla gems
                if (Player.inventory[num27].type >= ItemID.LargeAmethyst &&
                    Player.inventory[num27].type <= ItemID.LargeDiamond)
                {
                    Player.gem = Player.inventory[num27].type - 1522;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ItemID.LargeAmber)
                {
                    Player.gem = 6;
                    OwnedLargeGems[Player.gem] = true;
                }
                // Modded gems
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeZircon>())
                {
                    Player.gem = 7;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargeTourmaline>())
                {
                    Player.gem = 8;
                    OwnedLargeGems[Player.gem] = true;
                }
                else if (Player.inventory[num27].type == ModContent.ItemType<LargePeridot>())
                {
                    Player.gem = 9;
                    OwnedLargeGems[Player.gem] = true;
                }
            }
        }
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
