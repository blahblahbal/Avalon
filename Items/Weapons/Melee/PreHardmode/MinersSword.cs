using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;
public class MinersSword : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 20;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Orange;
        Item.Size = new Vector2(28);
        Item.useTime = 23;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 23;
        Item.UseSound = SoundID.Item1;
    }
}

// Earlier rework, need to do something better
//class MinersSword : ModItem
//{
//    public override void SetStaticDefaults()
//    {
//        Item.ResearchUnlockCount = 1;
//    }

//    public override void SetDefaults()
//    {
//        Item.damage = 20;
//        Item.rare = ItemRarityID.LightRed;
//        Item.Size = new Vector2(28);
//        Item.useTime = 13;
//        Item.knockBack = 5.5f;
//        Item.DamageType = DamageClass.Melee;
//        Item.useStyle = ItemUseStyleID.Swing;
//        Item.value = Item.sellPrice(0, 1, 0, 0);
//        Item.useAnimation = 13;
//        Item.UseSound = SoundID.Item1;
//    }

//    public override bool? CanAutoReuseItem(Player player)
//    {
//        return false;
//    }
//    public override bool? UseItem(Player player)
//    {
//        if (player.ItemAnimationEndingOrEnded)
//        {
//            player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus = 0;
//        }
//        return base.UseItem(player);
//    }

//    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
//    {
//        if (player.velocity.Y > 0)
//        {
//            for (int i = 0; i < (int)(player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus * 5) ; i++)
//            {
//                Dust d = Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(player.Center),DustID.GemDiamond);
//                d.noGravity = true;
//                d.velocity *= 3;
//            }
//            ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
//            particleOrchestraSettings.PositionInWorld = target.Hitbox.ClosestPointInRect(player.Center);
//            ParticleOrchestraSettings settings = particleOrchestraSettings;
//            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.SilverBulletSparkle, settings, player.whoAmI);
//        }
//        base.OnHitNPC(player, target, hit, damageDone);
//    }
//    public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
//    {
//        if(player.velocity.Y > 0)
//        {

//        }
//        base.OnHitPvp(player, target, hurtInfo);
//    }
//    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
//    {
//        damage = damage * player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus;
//    }
//    public override void ModifyWeaponCrit(Player player, ref float crit)
//    {
//        if (player.velocity.Y > 0)
//            crit *= 6f;
//        crit *= player.GetModPlayer<MinersSwordDamage>().MinersSwordDamageBonus / 2;
//    }
//}
//public class MinersSwordDamage : ModPlayer
//{
//    public float MinersSwordDamageBonus;
//    bool Notify = false;
//    public override void ResetEffects()
//    {
//        if(Player.HeldItem.type != ModContent.ItemType<MinersSword>())
//        {
//            MinersSwordDamageBonus = 0;
//        }
//        if(MinersSwordDamageBonus == 2)
//        {
//            if (Main.myPlayer == Player.whoAmI && !Notify)
//            {
//                Notify = true;
//                SoundEngine.PlaySound(SoundID.MaxMana);
//                for (int i = 0; i < 5; i++)
//                {
//                    int num2 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.ManaRegeneration, 0f, 0f, 255, default(Color), Main.rand.Next(20, 26) * 0.1f);
//                    Main.dust[num2].noLight = true;
//                    Main.dust[num2].noGravity = true;
//                    Main.dust[num2].velocity *= 0.5f;
//                }
//            }
//        }
//        else
//        {
//            Notify = false;
//        }
//        //CombatText.NewText(Player.Hitbox, Color.Red, $"{MinersSwordDamageBonus}");
//        MinersSwordDamageBonus = MathHelper.Clamp(MinersSwordDamageBonus += 0.04f, 0, 2);
//    }
//}
