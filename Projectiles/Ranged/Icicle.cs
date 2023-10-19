using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class Icicle : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height * 12 / 18 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 2;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
        for (int i = 0; i < 10; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, -Projectile.velocity.X / 3, -Projectile.velocity.Y / 3, Projectile.alpha);
            Main.dust[d].noGravity = !Main.rand.NextBool(3);
            if (Main.dust[d].noGravity)
                Main.dust[d].fadeIn = 1f;
        }
    }
    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 20f)
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
            Projectile.velocity.X = Projectile.velocity.X * 0.97f;
        }
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
