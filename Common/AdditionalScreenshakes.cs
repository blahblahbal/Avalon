using Avalon.Common.Templates;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Graphics.CameraModifiers;
using Avalon.Particles;

namespace Avalon.Common
{
    public class AddRecoilToVanillaGuns : GlobalItem
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.netID <= 5455 && entity.DamageType == DamageClass.Ranged;
        }

        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            if (item.UseSound == SoundID.Item36 || item.UseSound == SoundID.Item38)
            {
                UseStyles.ShotgunStyle(player, 0.1f, 3f, 3f);
            }
        }
    }

    public class AddScreenshakeToVanillaProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type <= 1021;
        }
        public override void Kill(Projectile projectile, int timeLeft)
        {
            if(projectile.type == ProjectileID.Grenade)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(projectile.Center, Main.rand.NextVector2Circular(1,1), 12, 15f, 15, 1200f, projectile.Name);
                Main.instance.CameraModifiers.Add(modifier);
            }
            if(projectile.type == ProjectileID.ExplosiveBullet || projectile.type == ProjectileID.HellfireArrow)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(projectile.Center, Main.rand.NextVector2Circular(1, 1), 12, 10f, 15, 300f, projectile.Name);
                Main.instance.CameraModifiers.Add(modifier);
            }
        }
    }
}
