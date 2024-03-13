using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class OilBottle : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(16);
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.alpha = 240;
        Projectile.scale = 1f;
        Projectile.tileCollide = true;
        DrawOriginOffsetY = -10;
    }
    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.alpha == 240) SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
        if(Projectile.alpha > 0)
        {
            Projectile.alpha -= 10;
        }
        Projectile.rotation += 0.3f * Projectile.direction;
        if (Projectile.ai[0] > 10)
            Projectile.velocity.Y += 0.15f;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Oiled, 60 * 8);
    }
    public override void OnKill(int timeLeft)
    {
        Projectile.maxPenetrate = -1;
        Projectile.penetrate = -1;
        Projectile.position = Projectile.Center;
        Projectile.Size += new Vector2(75);
        Projectile.Center = Projectile.position;

        Projectile.tileCollide = false;
        Projectile.velocity *= 0.01f;
        Projectile.Damage();

        Projectile.position = Projectile.Center;
        Projectile.Size = new Vector2(10);
        Projectile.Center = Projectile.position;

        for(int i = 0; i < 20; i++)
        {
            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TintableDust, Main.rand.NextVector2Circular(5, 5), 128, Color.Black, 1);
            d.noGravity = Main.rand.NextBool(5);
            if (d.noGravity)
                d.fadeIn = 1;
        }
        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<OilExplosion>(), 22, 2);
    }
}
