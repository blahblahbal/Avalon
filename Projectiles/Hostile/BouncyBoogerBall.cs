using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
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
        //if (Projectile.ai[0] == 1)
        //{
        //    Item.NewItem(Projectile.GetSource_Death(), Projectile.Hitbox, ModContent.ItemType<Booger>(), Main.rand.Next(1, 3));
        //}
        //if (Projectile.ai[0] == 2)
        //{
        //    Item.NewItem(Projectile.GetSource_Death(), Projectile.Hitbox, ModContent.ItemType<BacciliteOre>(), Main.rand.Next(1, 4));
        //}
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Weak, 5 * 60);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        int frameHeight = texture.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        //Texture2D textureBooger = ModContent.Request<Texture2D>("Avalon/Items/Material/Booger").Value;
        //int frameHeightBooger = textureBooger.Height;
        //Rectangle frameBooger = new Rectangle(0,0,textureBooger.Width,textureBooger.Height);
        //Vector2 drawPosBooger = Projectile.Center - Main.screenPosition;
        //if (Projectile.ai[0] == 1)
        //Main.EntitySpriteDraw(textureBooger, drawPosBooger, frameBooger, lightColor, (Main.masterColor - 0.5f) * 0.4f, new Vector2(textureBooger.Width, frameHeightBooger) / 2, Projectile.scale, SpriteEffects.None, 0);

        //Texture2D textureBaccilite = ModContent.Request<Texture2D>("Avalon/Items/Material/Ores/BacciliteOre").Value;
        //int frameHeightBaccilite = textureBaccilite.Height;
        //Rectangle frameBaccilite = new Rectangle(0, 0, textureBaccilite.Width, textureBaccilite.Height);
        //Vector2 drawPosBaccilite = Projectile.Center - Main.screenPosition;
        //if (Projectile.ai[0] == 2)
        //    Main.EntitySpriteDraw(textureBaccilite, drawPosBaccilite, frameBaccilite, lightColor, (Main.masterColor - 0.5f) * 0.4f, new Vector2(textureBaccilite.Width, frameHeightBaccilite) / 2, Projectile.scale, SpriteEffects.None, 0);

        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor * Projectile.Opacity, (Main.masterColor - 0.5f) * 0.2f, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
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
