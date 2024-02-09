using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Deadeye : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<DeadeyePlayer>().Deadeye = true;
    }
}
public class DeadeyePlayer : ModPlayer
{
    public bool Deadeye = false;
    public float DeadeyeTimer;
    public override void ResetEffects()
    {
        if (Deadeye)
        {
            DeadeyeTimer = MathHelper.Clamp(DeadeyeTimer + 0.002f, 0, 1);
        }
        else
        {
            DeadeyeTimer = MathHelper.Clamp(DeadeyeTimer - 0.02f, 0, 1);
        }
        Deadeye = false;
        base.ResetEffects();
    }
    public override float UseSpeedMultiplier(Item item)
    {
        //if (item.DamageType == DamageClass.Ranged)
        {
            return 1 + (DeadeyeTimer * 0.5f);
        }
        return base.UseSpeedMultiplier(item);
    }
    public override void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (DeadeyeTimer > 0) // && item.DamageType == DamageClass.Ranged)
        {
            velocity *= 1 + (DeadeyeTimer * 1f);
        }
        base.ModifyShootStats(item, ref position, ref velocity, ref type, ref damage, ref knockback);
    }
    public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
    {
        if (DeadeyeTimer > 0) // && item.DamageType == DamageClass.Ranged)
        {
            damage *= 1 + (DeadeyeTimer * 0.5f);
        }
        base.ModifyWeaponDamage(item, ref damage);
    }
}
