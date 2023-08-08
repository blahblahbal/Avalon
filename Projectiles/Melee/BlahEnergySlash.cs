using System;
using System.Runtime.CompilerServices;
using Avalon.Common.Templates;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace Avalon.Projectiles.Melee; 

public class BlahEnergySlash : EnergySlashTemplate
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.penetrate = -1;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Color[] Colors = { new Color(255, 66, 0), new Color(249, 201, 77), new Color(247, 255, 177), new Color(216, 131, 0) };
        Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60,0) * 0.5f;
        Color1.A = 64;
        //DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.5f), 0, 1.1f, 0, -0.2f, -0.3f, true);
        ////DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.5f), 0, 1.1f, 0, -0.2f, -0.3f, true);
        //DrawSlash(Color1, Color1 * 0.3f, Color1 * 0.6f, Color.Lerp(Color1, new Color(255, 66, 0), 0.5f), 0, 0.9f, 0, -0.2f, -0.3f, false);

        DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.5f), 0, 1f, 0.78f, -MathHelper.Pi / 24, 0, true);
        Color1 = ClassExtensions.CycleThroughColors(Colors, 60,40) * 0.5f;
        DrawSlash(Color1, Color1 * 0.3f, Color1 * 0.6f, Color.Lerp(Color1, new Color(255, 66, 0), 0.4f), 0, 0.8f, 0, -MathHelper.Pi / 24, 0, true);
        Color1 = ClassExtensions.CycleThroughColors(Colors, 60,80) * 0.5f;
        DrawSlash(Color1, Color1 * 0.8f, Color1 * 0.6f, Color.Lerp(Color1, Color.White, 0.3f), 0, 0.8f, 0.78f, -MathHelper.Pi / 6, 0, true);

        return false;
    }
    public override void AI()
    {
        float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        int[] Dusts = { DustID.Torch, DustID.HallowedTorch, DustID.SolarFlare };
        if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), Dusts[Main.rand.Next(3)], vector3 * 3f, 0, default, 1f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
            dust2.noGravity = true;
        }
    }
    
    //public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    //{
    //    ParticleOrchestraSettings settings = new ParticleOrchestraSettings();
    //    settings.PositionInWorld = target.Hitbox.ClosestPointInRect(Main.player[Projectile.owner].Center);
    //    //for (int i = 0; i < 2; i++)
    //    //{
    //    //    settings.MovementVector = Main.rand.NextVector2Circular(6,6);
    //    //    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.PrincessWeapon, settings);
    //    //    settings.MovementVector = Main.rand.NextVector2Circular(6, 6);
    //    //    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.StardustPunch, settings);
    //    //    settings.MovementVector = Main.rand.NextVector2Circular(6, 6);
    //    //    ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.SilverBulletSparkle, settings);
    //    //}
    //    Vector2 positionslansdglag = Main.rand.NextVector2FromRectangle(target.Hitbox);
    //    ParticleSystem.AddParticle(new CrystalSparkle(), positionslansdglag, Vector2.Zero, default);
    //    Network.SyncParticles.SendPacket(ParticleSystem.ParticleType.CrystalSparkle, positionslansdglag, Vector2.Zero, default);
    //    for (int i = 0; i < Main.rand.Next(1, 3); i++)
    //    {
    //        settings.MovementVector = Vector2.Zero;
    //        float rand = Main.rand.NextFloat(-0.5f, 0.5f);
    //        int Dist = Main.rand.Next(-1000, -600);
    //        if (Main.myPlayer == Projectile.owner)
    //        {
    //            settings.PositionInWorld = Main.MouseWorld + new Vector2(0, Dist).RotatedBy(rand);
    //            //ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.Excalibur, settings);
    //            ParticleSystem.AddParticle(new CrystalSparkle(), settings.PositionInWorld, Vector2.Zero, default,1);
    //            Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), settings.PositionInWorld, new Vector2(0, 30).RotatedBy(rand), ProjectileID.StarCloakStar, (int)(Projectile.damage * 0.4f), Projectile.knockBack, Projectile.owner, 0, target.position.Y);
    //        }
    //    }
    //}
}
