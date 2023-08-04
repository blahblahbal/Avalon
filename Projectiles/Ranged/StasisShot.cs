using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class StasisShot : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(6);
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 60 * 2;
        //Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Projectile.ai[1] == 0f)
        {
            Projectile.oldPosition = Projectile.position;
            Projectile.ai[1] = 1f;
        }
        Projectile.velocity.Y += 0.1f;
        for (float i = 0; i < 1; i += 0.2f)
        {
            var dust = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, Projectile.oldPosition + (Projectile.Size / 2), i), DustID.IceTorch, Vector2.Zero);
            dust.noGravity = true;
            dust.scale = 1;
        }
    }
    public override void Kill(int timeLeft)
    {
        base.Kill(timeLeft);
    }
}
