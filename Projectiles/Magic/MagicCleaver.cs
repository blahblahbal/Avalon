using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class MagicCleaver : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 44;
        Projectile.height = 44;
        Projectile.aiStyle = 2;
        Projectile.friendly = true;
        Projectile.penetrate = 5;
        Projectile.light = 0.15f;
        Projectile.alpha = 0;
        Projectile.scale = 1f;
        Projectile.timeLeft = 3600;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void AI()
    {
        if (Main.rand.Next(4) == 0)
        {
            int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 0.5f);
            Main.dust[num].velocity.X *= 0.1f;
            Main.dust[num].velocity.Y *= 0.1f;
        }
    }
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
        for (int num410 = 0; num410 < 5; num410++)
        {
            int num411 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 100, default(Color), 1f);
            Main.dust[num411].velocity *= 1f;
        }
    }

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        height = 25;
        width = 25;
        return true;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        for (int num410 = 0; num410 < 10; num410++)
        {
            int num411 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 100, default(Color), 1f);
            Main.dust[num411].velocity *= 1f;
        }
    }
}
