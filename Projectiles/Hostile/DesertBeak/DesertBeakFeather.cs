using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using System;

namespace Avalon.Projectiles.Hostile.DesertBeak;

public class DesertBeakFeather : ModProjectile
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
        Projectile.timeLeft = 540;
        Projectile.alpha = 250;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 1;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.tileCollide = true;
            Projectile.ai[2]++;
        }
        if (Projectile.ai[1] == 0)
        {
            Projectile.velocity.Y *= 0.5f;
            Projectile.ai[1]++;
        }

        if(Projectile.alpha > 0)
        {
            Projectile.alpha -= 10;
        }
        if (Projectile.ai[2] > 70)
        {
            Projectile.velocity.Y += 0.03f;
        }
        else if (Projectile.ai[2] > 40)
        {
            Projectile.velocity *= 0.995f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 3; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand);
        }
    }
}
