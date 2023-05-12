using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PhantasmalBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        AIType = ProjectileID.CursedBullet;
        Projectile.penetrate = 2;
        Projectile.alpha = 255;
        Projectile.scale = 1.2f;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        Projectile.MaxUpdates = 2;
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 75 / 255f, 15 / 255f, 35 / 255f);
        return true;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.VampireHeal);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        for (int i = 0; i < 15; i++)
        {
            int d = Dust.NewDust(Projectile.position, 8, 8, DustID.VampireHeal);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 1.5f;
            Main.dust[d].scale *= 0.7f;
        }
    }
    public override void Kill(int timeLeft)
    {
        if (Projectile.penetrate == 1)
        {
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            int explosionArea = 60;
            Vector2 oldSize = Projectile.Size;
            Projectile.position = Projectile.Center;
            Projectile.Size += new Vector2(explosionArea);
            Projectile.Center = Projectile.position;

            Projectile.tileCollide = false;
            Projectile.velocity *= 0.01f;
            //Projectile.Damage();
            Projectile.scale = 0.01f;

            Projectile.position = Projectile.Center;
            Projectile.Size = new Vector2(10);
            Projectile.Center = Projectile.position;
        }

        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/NPC_Killed_6") with { Volume = 0.5f, Pitch = -0.5f }, Projectile.position);
        for (int i = 0; i < 10; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.VampireHeal, 0, 0, 100, Color.Black, 0.8f);
            dust.noGravity = true;
            dust.velocity *= 1.5f;
            dust.scale *= 0.7f;
            Dust.NewDustDirect(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, Color.Black, 0.5f);
        }
        for (int i = 0; i < 10; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, default(Color), 3f);
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].velocity *= 1.5f;
            Main.dust[dustIndex].scale *= 0.7f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.VampireHeal, 0f, 0f, 100, default(Color), 2f);
            Main.dust[dustIndex].velocity *= 1.5f;
            Main.dust[dustIndex].scale *= 0.7f;
        }

        Projectile.position.X += Projectile.width / 2;
        Projectile.position.Y += Projectile.height / 2;
        Projectile.width = 80;
        Projectile.height = 80;
        Projectile.position.X -= Projectile.width / 2;
        Projectile.position.Y -= Projectile.height / 2;
        Projectile.active = false;
    }
    public bool CurveDirectionStart = true;
    public bool CurveDirection;
    public int maxSpeed = 15;
    public override void AI()
    {
        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 2;
        }
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }

        int num = 0;
        Projectile.localAI[num]++;
        Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
        float x = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(Projectile.localAI[num] * ((float)Math.PI / Main.rand.Next(10, 15))).X;
        Vector2 value = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866);
        Projectile.position += value * x * Main.rand.NextFloat(1f, 1.5f);
        //Projectile.position += value * x * Main.rand.NextFloat(5f, 10.5f); //exaggerated sine wave to test trail
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Trail").Value;
        Texture2D bulletTex = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2(Projectile.width / 2 - frameOrigin.X, Projectile.height - frameOrigin.Y - 4);
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

        for (int i = 0; i < 3; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * -i, Projectile.velocity.Y * -i), frame, new Color(255 - 255 / 7 * i, 0, 0, 100), Projectile.rotation, frameOrigin, Projectile.scale - 0.01f * i, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(bulletTex, drawPos, frame, Color.White, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
