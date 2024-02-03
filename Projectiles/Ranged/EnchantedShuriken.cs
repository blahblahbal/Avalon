using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class EnchantedShuriken : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 3;
        Projectile.DamageType = DamageClass.Ranged;
    }

    public override void AI()
    {
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 20f)
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
            Projectile.velocity.X = Projectile.velocity.X * 0.99f;
        }
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }

        //ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        //particleOrchestraSettings.PositionInWorld = Projectile.Center;
        //particleOrchestraSettings.MovementVector = Projectile.velocity * 0.5f;
        //ParticleOrchestraSettings settings = particleOrchestraSettings;
        //if (Main.timeForVisualEffects % 16 == 0 && Main.timeForVisualEffects % 32 != 0)
        //{
        //    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
        //}
        //else if (Main.timeForVisualEffects % 32 == 0)
        //{
        //    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
        //}
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        //if (hit.Crit && Main.myPlayer == Projectile.owner)
        //{
        //    ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        //    particleOrchestraSettings.PositionInWorld = Projectile.Center;
        //    particleOrchestraSettings.MovementVector = target.velocity;
        //    ParticleOrchestraSettings settings = particleOrchestraSettings;
        //    ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
        //    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, -6) + target.velocity, Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
        //}
        OnHitAnything();
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        OnHitAnything();
    }
    void OnHitAnything()
    {
        ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        particleOrchestraSettings.PositionInWorld = Projectile.Center;
        particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
        ParticleOrchestraSettings settings = particleOrchestraSettings;
        ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
        particleOrchestraSettings.PositionInWorld = Projectile.Center;
        particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
        settings = particleOrchestraSettings;
        ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        //for (int i = 0; i < 15; i++)
        //{
        //    var Sparkle = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 8, 8, DustID.RedTorch, 0f, 0f, 100, default(Color), 1.25f);
        //    Main.dust[Sparkle].velocity *= 0.8f;
        //}
        for (int i = 0; i < 2; i++)
        {
            ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
            particleOrchestraSettings.PositionInWorld = Projectile.Center;
            particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3,3);
            ParticleOrchestraSettings settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
            particleOrchestraSettings.PositionInWorld = Projectile.Center;
            particleOrchestraSettings.MovementVector = Main.rand.NextVector2Circular(3, 3);
            settings = particleOrchestraSettings;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
        }
        //ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        //particleOrchestraSettings.PositionInWorld = Projectile.Center;
        //particleOrchestraSettings.MovementVector = Vector2.Lerp(-Projectile.oldVelocity, Projectile.oldVelocity, Main.rand.NextFloat()) * 0.5f;
        //ParticleOrchestraSettings settings = particleOrchestraSettings;
        //ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, settings, Projectile.owner);
        //particleOrchestraSettings.PositionInWorld = Projectile.Center;
        //particleOrchestraSettings.MovementVector = Vector2.Lerp(-Projectile.oldVelocity, Projectile.oldVelocity, Main.rand.NextFloat()) * 0.5f;
        //settings = particleOrchestraSettings;
        //ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override bool PreDraw(ref Color lightColor) // theft
    {
        Vector2 drawOrigin = new Vector2(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = new Color(0,128,255,0) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, Projectile.height * Projectile.frame, Projectile.width, Projectile.height), color, Projectile.rotation, drawOrigin, Projectile.scale * 0.9f, SpriteEffects.None, 0);
        }
        return true;
    }
}
