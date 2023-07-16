using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode; 

class MinersSword : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new Vector2(28);
        Item.useTime = 8;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 8;
        Item.channel = true;
        Item.UseSound = SoundID.Item1;
    }
    public override bool? UseItem(Player player)
    {
        if (player.ItemAnimationEndingOrEnded)
        {
            player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus = 0;
        }
        return base.UseItem(player);
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (player.velocity.Y > 0)
        {
            for (int i = 0; i < (int)(player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus * 5) ; i++)
            {
                Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.Center),DustID.GemDiamond);
                d.noGravity = true;
                d.velocity *= 3;
            }
        }
        base.OnHitNPC(player, target, hit, damageDone);
    }
    public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
    {
        if(player.velocity.Y > 0)
        {

        }
        base.OnHitPvp(player, target, hurtInfo);
    }
    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
        damage = damage * player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus;
    }
    public override void ModifyWeaponCrit(Player player, ref float crit)
    {
        if (player.velocity.Y > 0)
            crit *= 6f;
        crit *= player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus;
    }
}
public class MinersSwordDamage : ModPlayer
{
    public float MinersSwordDamageBonus;
    public override void ResetEffects()
    {
        //CombatText.NewText(Player.Hitbox, Color.Red, $"{MinersSwordDamageBonus}");
        MinersSwordDamageBonus = MathHelper.Clamp(MinersSwordDamageBonus += 0.04f, 0, 2);
    }
}
