using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ExxoAvalonOrigins.Projectiles.Magic;

public class IceNote : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.penetrate = 3;
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 180;
        Projectile.scale = 1f;
        Projectile.coldDamage = true;
        DrawOriginOffsetY -= 2;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 10);
    }
    public int Timer;
    public override void AI()
    {
        Projectile.spriteDirection = -Projectile.direction;
        if (Main.rand.NextBool(2))
        {
            int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 92, 0f, 0f, default, default, 1.2f);
            Dust dust2 = Main.dust[dust1];
            dust2.velocity *= 0f;
            dust2.noGravity = true;
        }
        Timer++;
        if (Timer <= 10)
        {
            Projectile.scale *= 1.03f;
        }
        if (Timer >= 10)
        {
            Projectile.scale *= 0.97f;
        }
        if (Timer == 20)
        {
            Timer = 0;
            Projectile.scale = 1f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.penetrate--;
        if (Projectile.penetrate <= 0)
        {
            Projectile.Kill();
        }
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X;
        }
        if (Projectile.velocity.Y != oldVelocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y;
        }
        return false;
    }
    public SoundStyle Icenote = new SoundStyle("Terraria/Sounds/Item_27")
    {
        Volume = 0.8f,
        Pitch = -0.25f,
        PitchVariance = 0f,
        MaxInstances = 10,
    };
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(Icenote, Projectile.Center);

        for (int num237 = 0; num237 < 5; num237++)
        {
            int num239 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, default, default, 1.2f);
            Dust dust30 = Main.dust[num239];
            dust30.noGravity = true;
        }
    }
}
