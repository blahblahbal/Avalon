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
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
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
        int num = 0;
        Projectile.localAI[num]++;
        Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
        float x = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(Projectile.localAI[num] * ((float)Math.PI / Main.rand.Next(10, 15))).X;
        Vector2 value = Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866);
        Projectile.position += value * x * Main.rand.NextFloat(1f, 1.5f);
        //Projectile.position += value * x * Main.rand.NextFloat(5f, 10.5f); //exaggerated sine wave to test trail
    }
    public override bool PreDraw(ref Color lightColor) // theft v2? (from enchanted shuriken)
    {
        Rectangle dims = this.GetDims();
        Vector2 drawOrigin = new Vector2(Projectile.width, Projectile.height);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = new Color(255, 255, 255, 0) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, dims.Height * Projectile.frame, dims.Width, dims.Height), color, Projectile.rotation, drawOrigin, Projectile.scale * 0.9f, SpriteEffects.None, 0);
        }
        return false;
    }
}
