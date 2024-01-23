using System;
using Avalon.Common.Templates;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace Avalon.Projectiles.Melee; 

public class SanguineKatanaSlash : EnergySlashTemplate
{
    public override bool PreDraw(ref Color lightColor)
    {
        DrawSlash(Color.Black * 0.2f, Color.Black * 0.2f, Color.Red * 0.2f, Color.Black * 0.5f, 512, 1f, MathHelper.PiOver4, -0.2f, 0, false);
        DrawSlash(new Color(255, 0, 0) * 0.6f, new Color(128, 0, 0) * 0.5f, new Color(0, 0, 0) * 0.5f, Color.Red * 0.2f, 512, 1f, 0, -0.2f, 0, true);
        //DrawSlash(new Color(255, 0, 0), new Color(128, 0, 0), new Color(0, 0, 0), Color.Black, 512, 1f, MathHelper.PiOver4, -MathHelper.Pi / 12, -MathHelper.Pi / 24, true);
        return false;
    }
    public override void AI()
    {
        float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.Blood, vector3 * 3f, 100, default, 1f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
            dust2.noGravity = true;
        }
        if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.RedTorch, vector3 * 3f, 254, default, 1f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
            dust2.noGravity = true;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Projectile.penetrate < 90)
        {
            Player player = Main.player[Projectile.owner];
            if (target.type != NPCID.TargetDummy)
            {
                int healAmount = Main.rand.Next(0, 3) + Main.rand.Next(1, 3) + 3;
                player.HealEffect(healAmount, true);
                player.statLife += healAmount;
                Projectile.penetrate = 100;
            }
        }
        //for (int i = 0; i < 15; i++)
        //{
        //    int num15 = Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, DustID.Blood, 0, 0, 140, default(Color), 2f);
        //    Main.dust[num15].fadeIn = 1.2f;
        //    Main.dust[num15].noGravity = true;
        //    Main.dust[num15].velocity = Main.rand.NextVector2Circular(6, 6);
        //}
        Vector2 vector = Main.rand.NextVector2FromRectangle(target.Hitbox);
        SoundEngine.PlaySound(SoundID.NPCHit1,target.position);
        Vector2 pos = vector - new Vector2(0, 24);
        Vector2 vel = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), 1);
        ParticleSystem.AddParticle(new SanguineCuts(), pos, vel, default,Main.rand.Next(12,24));
        //Network.SyncParticles.SendPacket(ParticleSystem.ParticleType.SanguineCuts, pos, vel, default);
    }
    public override void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawpos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
    {
        Texture2D value = TextureAssets.Extra[98].Value;
        Color color = shineColor * opacity;
        color.A = 0;
        Vector2 origin = value.Size() / 2f;
        Color color2 = drawColor * 0.5f;
        float num = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
        Vector2 vector = new Vector2(fatness.X * 0.5f, scale.X) * num;
        Vector2 vector2 = new Vector2(fatness.Y * 0.5f, scale.Y) * num;
        color *= num;
        color2 *= num;
        for (int i = 0; i < 3; i++)
        {
            Main.EntitySpriteDraw(value, drawpos, null, color, (float)Math.PI / 2f + rotation, origin, vector, dir);
            Main.EntitySpriteDraw(value, drawpos, null, color, 0f + rotation, origin, vector2, dir);
        }
    }
}
