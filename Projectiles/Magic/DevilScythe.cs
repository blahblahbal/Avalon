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
        Projectile.alpha = 50;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.tileCollide = true;
        Projectile.scale = 0.9f;
        Projectile.DamageType = DamageClass.Magic;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int dustAmount = 0; dustAmount < 30; dustAmount++)
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.7f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1.1f;
            int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X, Projectile.velocity.Y, 100);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 1.2f;
        }
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

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = Projectile.width - 36;
        height = Projectile.height - 36;
        return true;
    }
}
