using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class DevilScythe : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.alpha = 100;
        Projectile.light = 0.5f;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.tileCollide = true;
        Projectile.scale = 0.9f;
        Projectile.DamageType = DamageClass.Magic;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        int num234 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.7f);
        Main.dust[num234].noGravity = true;
        Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1f);
        return true;
    }
    public override void AI()
    {
        Projectile.rotation += Projectile.direction * 0.8f;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 30f)
        {
            if (Projectile.ai[0] < 100f)
            {
                Projectile.velocity *= 1.06f;
            }
            else
            {
                Projectile.ai[0] = 200f;
            }
        }
        for (var num305 = 0; num305 < 2; num305++)
        {
            if (Projectile.type == ModContent.ProjectileType<DevilScythe>())
            {
                var num307 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num307].noGravity = true;
            }
        }
    }
}
