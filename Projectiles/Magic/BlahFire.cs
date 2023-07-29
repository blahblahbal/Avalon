using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class BlahFire : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.aiStyle = -1;
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.tileCollide = true;
        Projectile.penetrate = -1;
        Projectile.hostile = false;
        Projectile.alpha = 255;
        Projectile.timeLeft = 180;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 249 / 255, 201 / 255, 77 / 255);
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity.X = oldVelocity.X * -0.1f;
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = oldVelocity.X * -0.5f;
        }
        if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1f)
        {
            Projectile.velocity.Y = oldVelocity.Y * -0.5f;
        }
        return false;
    }
    public override void AI()
    {
        Projectile.hostile = false;
        Projectile.friendly = true;
        Projectile.ai[0]++;
        if (Projectile.ai[0] > 5f)
        {
            Projectile.ai[0] = 5f;
            if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.netUpdate = true;
                }
            }
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        if (Projectile.type == ModContent.ProjectileType<BlahFire>())
        {
            if (Projectile.ai[1] == 0f && Projectile.type == ModContent.ProjectileType<BlahFire>())
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(Terraria.ID.SoundID.Item13, Projectile.position);
            }
            int num218 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 1f);
            Main.dust[num218].position.X -= 2f;
            Main.dust[num218].position.Y += 2f;
            Main.dust[num218].scale += Main.rand.Next(50) * 0.01f;
            Main.dust[num218].noGravity = true;
            Main.dust[num218].velocity.Y -= 2f;
            if (Main.rand.NextBool(5))
            {
                int num219 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default, 1f);
                Main.dust[num219].position.X -= 2f;
                Main.dust[num219].position.Y += 2f;
                Main.dust[num219].scale += 0.3f + Main.rand.Next(50) * 0.01f;
                Main.dust[num219].noGravity = true;
                Main.dust[num219].velocity *= 0.1f;
            }
            if (Projectile.velocity.Y < 0.25 && Projectile.velocity.Y > 0.15)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.8f;
            }
            Projectile.rotation = -Projectile.velocity.X * 0.05f;
        }
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
