using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Projectiles.Hostile.DesertBeak;

namespace Avalon.Projectiles.Ranged;

public class ExplosiveEggShrapnel : EggShrapnel
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(8);
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.tileCollide = true;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.timeLeft = 25;
        Projectile.penetrate = 3;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 1;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
}
