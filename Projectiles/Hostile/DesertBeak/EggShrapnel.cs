using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace Avalon.Projectiles.Hostile.DesertBeak;

public class EggShrapnel : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Width;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.tileCollide = false;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.timeLeft = 40;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 1;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
        if (Main.rand.NextBool(3))
        {
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DesertTorch);
            d.noGravity = true;
            d.velocity = Projectile.velocity * 0.5f;
            d.scale = Main.rand.NextFloat(2);
        }

        if (Projectile.timeLeft <= 30)
            Projectile.alpha += 255 / 30;
    }
}
