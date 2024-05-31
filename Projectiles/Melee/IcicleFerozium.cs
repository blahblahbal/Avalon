using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class IcicleFerozium : ModProjectile
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

    public override void AI()
    {
        if (Projectile.type == ProjectileID.MagicDagger && Main.rand.NextBool(5))
        {
            var num58 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X * 0.2f + Projectile.direction * 3, Projectile.velocity.Y * 0.2f, 100, default(Color), 0.3f);
            var dust7 = Main.dust[num58];
            dust7.velocity.X = dust7.velocity.X * 0.3f;
            var dust8 = Main.dust[num58];
            dust8.velocity.Y = dust8.velocity.Y * 0.3f;
        }
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
