using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace Avalon.Projectiles.Hostile;

public class DesertBeakSandstorm : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.tileCollide = false;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.alpha = 255;
        Projectile.timeLeft = 540;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
        Projectile.alpha = 255;
        float vR = .2f;
        int radius = 72;
        int xMinWholeCookie = ((int)Projectile.Center.X) - radius;
        int xMaxWholeCookie = ((int)Projectile.Center.X) + radius;
        int yMinWholeCookie = ((int)Projectile.Center.Y) - radius * 2;
        int yMaxWholeCookie = ((int)Projectile.Center.Y) + radius * 2;

        for (int q = (int)Projectile.position.X; q < Projectile.position.X + Projectile.width; q += Main.rand.Next(2, 4))
        {
            for (int z = (int)Projectile.position.Y; z < Projectile.position.Y + Projectile.height; z += Main.rand.Next(2, 4))
            {
                if (Main.rand.NextBool(30))
                {
                    int D2 = Dust.NewDust(new Vector2(q, z), 0, 0, DustID.SandstormInABottle, 0, 0, 100, new Color(), 1.5f);
                    Main.dust[D2].noGravity = true;
                    //Main.dust[D2].color = Color.Goldenrod;
                    Main.dust[D2].fadeIn = 0.5f;
                    Main.dust[D2].velocity.X = vR * (Main.dust[D2].position.X - (q + 12));
                    Main.dust[D2].velocity.Y = vR * (Main.dust[D2].position.Y - (z + 8));
                }
            }
        }
    }
}
