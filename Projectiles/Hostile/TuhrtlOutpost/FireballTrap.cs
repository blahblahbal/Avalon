using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Hostile.TuhrtlOutpost;

public class FireballTrap : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.ForcePlateDetection[Projectile.type] = false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 12 / 16;
        Projectile.height = dims.Height * 12 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.alpha = 255;
        Projectile.light = 0.6f;
        Projectile.penetrate = 12;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 420;
        Projectile.ignoreWater = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 4f)
        {
            Projectile.position += Projectile.velocity;
            Projectile.Kill();
        }
        else
        {
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
        }
        return false;
    }

    public override void AI()
    {
        for (var num917 = 0; num917 < 5; num917++)
        {
            var num918 = Projectile.velocity.X / 3f * num917;
            var num919 = Projectile.velocity.Y / 3f * num917;
            var num920 = 4;
            var num921 = Dust.NewDust(new Vector2(Projectile.position.X + num920, Projectile.position.Y + num920), Projectile.width - num920 * 2, Projectile.height - num920 * 2, DustID.Torch, 0f, 0f, 100, default(Color), 1.2f);
            Main.dust[num921].noGravity = true;
            Main.dust[num921].velocity *= 0.1f;
            Main.dust[num921].velocity += Projectile.velocity * 0.1f;
            var dust105 = Main.dust[num921];
            dust105.position.X = dust105.position.X - num918;
            var dust106 = Main.dust[num921];
            dust106.position.Y = dust106.position.Y - num919;
        }
        if (Main.rand.NextBool(5))
        {
            var num922 = 4;
            var num923 = Dust.NewDust(new Vector2(Projectile.position.X + num922, Projectile.position.Y + num922), Projectile.width - num922 * 2, Projectile.height - num922 * 2, DustID.Flare, 0f, 0f, 100, default(Color), 0.6f);
            Main.dust[num923].velocity *= 0.25f;
            Main.dust[num923].velocity += Projectile.velocity * 0.5f;
        }
        if (Projectile.ai[1] >= 20f)
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
        }
        Projectile.rotation += 0.3f * Projectile.direction;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
            return;
        }
    }
}
