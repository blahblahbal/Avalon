using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Graphics.CameraModifiers;
using System.Linq;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Avalon.Common
{
    public class AddRecoilToVanillaGunsAndSwords : GlobalItem
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.netID <= 5455 || !entity.noMelee;
        }

        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            if (item.netID <= 5455)
            {
                if (item.useAmmo == AmmoID.Rocket)
                {
                    UseStyles.ShotgunStyle(player, 0.02f, 3f, 0.3f);
                }
                else if (item.type == ItemID.Xenopopper)
                {
                    UseStyles.ShotgunStyle(player, 0.020f, -2f, 0);
                }
                else if (item.UseSound == SoundID.Item36 || item.UseSound == SoundID.Item38)
                {
                    UseStyles.ShotgunStyle(player, 0.1f, 3f, 3f);
                }
                else if (item.UseSound == SoundID.Item11 || item.type == ItemID.DartPistol || item.type == ItemID.DartRifle)
                {
                    UseStyles.ShotgunStyle(player, 0.01f, 3f, 0f);
                }
                else if (item.UseSound == SoundID.Item41)
                {
                    UseStyles.ShotgunStyle(player, 0.015f, 3f, 0f);
                }
                else if (item.useAmmo == AmmoID.Bullet && item.type != ItemID.ClockworkAssaultRifle)
                {
                    UseStyles.ShotgunStyle(player, 0.010f, 2f, 0f);
                }
            }
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //if (player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            //{
            //    PunchCameraModifier modifier = new PunchCameraModifier(target.Center, Main.rand.NextVector2Circular(1, 1), 1f, 3f, 15, 300f, player.name);
            //    Main.instance.CameraModifiers.Add(modifier);
            //}
        }
        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            //if (player.whoAmI == Main.myPlayer && ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            //{
            //    PunchCameraModifier modifier = new PunchCameraModifier(target.MountedCenter, Main.rand.NextVector2Circular(1, 1), 1f, 3f, 15, 300f, player.name);
            //    Main.instance.CameraModifiers.Add(modifier);
            //}
        }
    }
    public class AddScreenshakeToVanillaProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return (entity.type <= 1021 && entity.DamageType == DamageClass.Ranged) || entity.aiStyle == ProjAIStyleID.NightsEdge || entity.aiStyle == ProjAIStyleID.Spear || entity.aiStyle == ProjAIStyleID.ShortSword || entity.aiStyle == ProjAIStyleID.Whip;
        }

        public bool IsRocket;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //if (ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            //{
            //    if (Main.player[projectile.owner].whoAmI == Main.myPlayer && (projectile.DamageType == DamageClass.Melee || projectile.DamageType == DamageClass.SummonMeleeSpeed))
            //    {
            //        PunchCameraModifier modifier = new PunchCameraModifier(target.Center, Main.rand.NextVector2Circular(1, 1), 1f, 3f, 15, 300f, Main.player[projectile.owner].name);
            //        Main.instance.CameraModifiers.Add(modifier);
            //    }
            //}
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            //if (ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            //{
            //    if (Main.player[projectile.owner].whoAmI == Main.myPlayer && (projectile.DamageType == DamageClass.Melee || projectile.DamageType == DamageClass.SummonMeleeSpeed))
            //    {
            //        PunchCameraModifier modifier = new PunchCameraModifier(target.MountedCenter, Main.rand.NextVector2Circular(1, 1), 1f, 3f, 15, 300f, Main.player[projectile.owner].name);
            //        Main.instance.CameraModifiers.Add(modifier);
            //    }
            //}
                
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            if (source is EntitySource_ItemUse_WithAmmo parent && (parent.Item.useAmmo == AmmoID.Rocket || parent.Item.useAmmo == AmmoID.JackOLantern))
            {
                projectile.GetGlobalProjectile<AddScreenshakeToVanillaProjectiles>().IsRocket = true;
            }
            //Main.myPlayer == Main.player[projectile.owner].whoAmI
            else if (player.HeldItem.type == ItemID.VortexBeater)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(Main.player[projectile.owner].MountedCenter, new Vector2(Main.rand.NextFloat(-1.5f, -0.7f), 0).RotatedBy(Main.player[projectile.owner].MountedCenter.DirectionTo(Main.MouseWorld).ToRotation() + Main.rand.NextFloat(-0.1f, 0.1f)), 0.3f, 6f, 8, 200f, Main.player[projectile.owner].name);
                Main.instance.CameraModifiers.Add(modifier);
            }
        }
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (ModContent.GetInstance<AvalonClientConfig>().AdditionalScreenshakes)
            {
                int[] grenades = { ProjectileID.Grenade, ProjectileID.BouncyGrenade, ProjectileID.StickyGrenade, ProjectileID.Beenade };
                if (grenades.Contains<int>(projectile.type))
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(projectile.Center, Main.rand.NextVector2Circular(1, 1), 12, 15f, 15, 1200f, projectile.Name);
                    Main.instance.CameraModifiers.Add(modifier);
                }
                else if (projectile.GetGlobalProjectile<AddScreenshakeToVanillaProjectiles>().IsRocket || projectile.type == ProjectileID.VortexBeaterRocket)
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(projectile.Center, Main.rand.NextVector2Circular(1, 1), 12, 10f, 15, 500f, projectile.Name);
                    Main.instance.CameraModifiers.Add(modifier);
                }
                else if (projectile.type == ProjectileID.ExplosiveBullet || projectile.type == ProjectileID.HellfireArrow)
                {
                    PunchCameraModifier modifier = new PunchCameraModifier(projectile.Center, Main.rand.NextVector2Circular(1, 1), 12, 10f, 15, 300f, projectile.Name);
                    Main.instance.CameraModifiers.Add(modifier);
                }
            }
        }
    }
}
