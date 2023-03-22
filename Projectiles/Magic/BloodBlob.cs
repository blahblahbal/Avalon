using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class BloodBlob : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = 1;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 300;
        Projectile.scale = 1f;
        Projectile.alpha = 255;
    }
    private Player player => Main.player[Projectile.owner];
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 30 / 255f, 20 / 255f, 0);
        Projectile.spriteDirection = Projectile.direction;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile.ai[0]++;
        if (Projectile.ai[0] == 4)
        {
            Projectile.alpha = 0;
            for (int num257 = 0; num257 < 15; num257++)
            {
                int newDust = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, default(Color), 1.25f);
                Main.dust[newDust].position = (Main.dust[newDust].position + Projectile.Center) / 2f;
                Main.dust[newDust].noGravity = true;
                Main.dust[newDust].velocity *= 1.5f;
            }
        }
        if (Projectile.ai[0] > 10)
        {
            Projectile.velocity.Y += Projectile.ai[0] / 100;
        }

        if(Projectile.velocity.Length() >= 20f)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 20f;
        }

        if(Projectile.ai[0] >= 4)
        {
            int blood = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f, 100, default(Color), 1f);
            Main.dust[blood].noGravity = true;
            Main.dust[blood].fadeIn = 0.75f;
        }

        Projectile.frameCounter++;
        if (Projectile.frameCounter > 4)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame >= 4)
        {
            Projectile.frame = 0;
            Projectile.frameCounter = 0;
        }
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (target.type != NPCID.TargetDummy)
        {
            int healAmount = Main.rand.Next(4) + 4;
            player.HealEffect(healAmount, true);
            player.statLife += healAmount;
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("ExxoAvalonOrigins/Projectiles/Magic/BloodBlob").Value;
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        Main.EntitySpriteDraw(texture, drawPos, frame, Color.White * 0.25f, Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale * 1.2f, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

        Main.EntitySpriteDraw(texture, drawPos, frame, Color.Lerp(Color.White, lightColor, 0.6f), Projectile.rotation, new Vector2(texture.Width, frameHeight) / 2, Projectile.scale, Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

        return false;
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        int size = Projectile.width * 2;
        hitbox.X -= size / 2;
        hitbox.Y -= size / 2;
        hitbox.Width += size;
        hitbox.Height += size;
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);
        /*for (int num237 = 0; num237 < 20; num237++)
        {
            Vector2 randv = new Vector2(Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f).RotatedByRandom(MathHelper.PiOver2);
            int num239 = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, -randv.X * Main.rand.NextFloat(-0.5f, 1.5f), -randv.Y * Main.rand.NextFloat(-0.5f, 1.5f), 50, default(Color), 0.5f * Main.rand.NextFloat(0, 3));
            Main.dust[num239].noGravity = false;
            if (Main.rand.NextBool(2))
            {
                Main.dust[num239].fadeIn = 0.1f;
            }
            else
            {
                Main.dust[num239].fadeIn = 1.25f;
            }
        }*/
        for (int num237 = 0; num237 < 30; num237++)
        {
            int num239 = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, 0f, 0f, 50, default(Color), 0.5f * Main.rand.NextFloat(0, 3));
            Main.dust[num239].noGravity = true;
            Main.dust[num239].velocity *= 1.5f;
            Main.dust[num239].fadeIn = 0.5f;
        }
        for (int num237 = 0; num237 < 40; num237++)
        {
            int num239 = Dust.NewDust(Projectile.position - new Vector2(Projectile.width / 2f, Projectile.height / 2f), Projectile.width * 2, Projectile.height * 2, DustID.Blood, -Projectile.oldVelocity.X * Main.rand.NextFloat(-0.1f, -0.3f), Projectile.oldVelocity.Y * Main.rand.NextFloat(-0.1f, -0.4f), 50, default(Color), 0.5f * Main.rand.NextFloat(0, 3));
            Main.dust[num239].noGravity = false;
            Main.dust[num239].fadeIn = 0.75f;
        }
    }
}
