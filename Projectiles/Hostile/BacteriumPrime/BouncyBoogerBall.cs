using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.BacteriumPrime;

public class BouncyBoogerBall : ModProjectile
{
	private static Asset<Texture2D> texture;
	private static Asset<Texture2D> textureBooger;
	private static Asset<Texture2D> textureBaccilite;
	public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
        texture = TextureAssets.Projectile[Type];
        textureBooger = ModContent.Request<Texture2D>("Avalon/Items/Material/Booger");
        textureBaccilite = ModContent.Request<Texture2D>("Avalon/Items/Material/Ores/BacciliteOre");
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

        if (Projectile.velocity.Y < 8 && Projectile.position.Y < Main.worldSurface * 16 + 30 * 16)
        {
            Projectile.velocity.Y += 0.1f;
            Projectile.velocity.X *= 0.994f;
        }

        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 10)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
        }
        if (Projectile.frame > 3)
        {
            Projectile.frame = 0;
        }
        if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.tileCollide = true;
        }
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);
        for (int i = 0; i < 10; i++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, 0, 0, 128, default, 2);
            Main.dust[d].velocity *= 5;
            Main.dust[d].noGravity = true;
        }
        if (Projectile.ai[0] == 1)
        {
            Item.NewItem(Projectile.GetSource_Death(), Projectile.Hitbox, ModContent.ItemType<Booger>(), Main.rand.Next(3, 6));
        }
        if (Projectile.ai[0] == 2)
        {
            Item.NewItem(Projectile.GetSource_Death(), Projectile.Hitbox, ModContent.ItemType<BacciliteOre>(), Main.rand.Next(5, 10));
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Weak, 5 * 60);
        Projectile.ai[0] = 0;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        int frameHeight = texture.Value.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Value.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        int frameHeightBooger = textureBooger.Value.Height;
        Rectangle frameBooger = new Rectangle(0, 0, textureBooger.Value.Width, textureBooger.Value.Height);
        Vector2 drawPosBooger = Projectile.Center - Main.screenPosition;
        if (Projectile.ai[0] == 1)
            Main.EntitySpriteDraw(textureBooger.Value, drawPosBooger, frameBooger, lightColor, (Main.masterColor - 0.5f) * 0.4f, new Vector2(textureBooger.Value.Width, frameHeightBooger) / 2, Projectile.scale, SpriteEffects.None, 0);

        int frameHeightBaccilite = textureBaccilite.Value.Height;
        Rectangle frameBaccilite = new Rectangle(0, 0, textureBaccilite.Value.Width, textureBaccilite.Value.Height);
        Vector2 drawPosBaccilite = Projectile.Center - Main.screenPosition;
        if (Projectile.ai[0] == 2)
            Main.EntitySpriteDraw(textureBaccilite.Value, drawPosBaccilite, frameBaccilite, lightColor, (Main.masterColor - 0.5f) * 0.4f, new Vector2(textureBaccilite.Value.Width, frameHeightBaccilite) / 2, Projectile.scale, SpriteEffects.None, 0);

        Main.EntitySpriteDraw(texture.Value, drawPos, frame, lightColor * Projectile.Opacity, (Main.masterColor - 0.5f) * -0.2f, new Vector2(texture.Value.Width, frameHeight) / 2, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.Center);
        if (Projectile.velocity.X != Projectile.oldVelocity.X)
            Projectile.velocity.X = -Projectile.oldVelocity.X;
        if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
            Projectile.velocity.Y = -Projectile.oldVelocity.Y;
        return false;
    }
}
