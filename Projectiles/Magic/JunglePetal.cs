using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class JunglePetal : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.alpha = 20;
        Projectile.penetrate = 2;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 60;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Height / 2) - (Projectile.Size.Y / 2));
    }
    public override Color? GetAlpha(Color lightColor)
    {
        int a = Projectile.timeLeft * 4;
        return lightColor * (a / 255f * 3);
    }
    public override void AI()
    {
        int a = 255 - Projectile.timeLeft * 4;
        Lighting.AddLight(Projectile.position, 0.2f * (Projectile.timeLeft * 0.02f), 0.2f * (Projectile.timeLeft * 0.02f), 0.1f * (Projectile.timeLeft * 0.02f));
        Projectile.velocity *= 0.975f;
        Projectile.rotation += 0.3f * Projectile.direction;
        Projectile.alpha += (int)(a * 0.05f);
    }
}
