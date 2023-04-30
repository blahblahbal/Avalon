using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class BacteriumGas : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = 1;
        Projectile.alpha = 254;
        Projectile.friendly = false;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.scale = 1f;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        if (Projectile.ai[2] > 1)
        Projectile.alpha += 1;
        else
        Projectile.alpha -= 7;

        if (Projectile.alpha <= 100)
            Projectile.ai[2]++;

        if (Projectile.alpha == 255) Projectile.Kill();

        Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.985f;
        Projectile.rotation += MathHelper.Clamp(Projectile.velocity.Length() * 0.1f, -0.3f, 0.3f);
        Projectile.scale *= 1.003f;

        //if (Main.rand.NextBool(3))
        //{
        //    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Venom, 0, 0, (int)(Projectile.alpha * 1.4f), default, 0.5f);
        //    Main.dust[d].velocity *= 0.5f;
        //    Main.dust[d].fadeIn = 2f;
        //    Main.dust[d].noGravity = true;
        //}
    }
    public override bool CanHitPlayer(Player target)
    {
        return Projectile.alpha < 220;
    }
    //public override void OnHitPlayer(Player target, Player.HurtInfo info)
    //{

    //}

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);

        for(int i = 0; i < 6; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(0,Projectile.width / 4 * ((float)Projectile.alpha) / 128).RotatedBy(i * (MathHelper.TwoPi) / 6), frame, lightColor * Projectile.Opacity * 0.4f, Projectile.rotation + ((float)Projectile.alpha / 128) * (i / 128), new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.FlipVertically, 0);
        }
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = oldVelocity * Main.rand.NextFloat(-0.2f,0.2f);
        Projectile.tileCollide = false;
        return false;
    }
}
