using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Hostile.TuhrtlOutpost;

public class CannonballTrap : ModProjectile
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
        //AIType = ProjectileID.Boulder;
        Projectile.tileCollide = true;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.alpha = 0;
        Projectile.light = 0.6f;
        Projectile.penetrate = 12;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.timeLeft = 600;
        Projectile.ignoreWater = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 5f)
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
        Projectile.ai[1]++;
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
