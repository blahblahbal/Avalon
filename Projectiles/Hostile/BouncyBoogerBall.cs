using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class BouncyBoogerBall : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.alpha = 128;
        Projectile.friendly = false;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.scale = 1f;
        Projectile.tileCollide = false;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {
        int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128, default, 2);
        Main.dust[d].noGravity = true;

        Projectile.frameCounter++;
        if(Projectile.frameCounter >= 10) 
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
        }
        if(Projectile.frame > 3)
        {
            Projectile.frame = 0;
        }
        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.tileCollide = true;
        }
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
        for (int i = 0; i< 10; i++) 
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128, default, 2);
            Main.dust[d].velocity *= 5;
            Main.dust[d].noGravity = true;
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Weak, 5 * 16);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor * Projectile.Opacity, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.Center);
        if(Projectile.velocity.X != Projectile.oldVelocity.X)
            Projectile.velocity.X = -Projectile.oldVelocity.X;
        if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
            Projectile.velocity.Y = -Projectile.oldVelocity.Y;
        return false;
    }
}
