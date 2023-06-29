using System;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace Avalon.Projectiles.Melee; 

public class VertexSlash : EnergySlashTemplate
{
    public override bool PreDraw(ref Color lightColor)
    {
        if (Math.Abs(Projectile.ai[0]) > 0.2f)
        {
            DrawSlash(new Color(255, 255, 255, 0) * 0.7f, new Color(0, 0, 0, 200) * 0.6f, new Color(0, 0, 0, 200) * 0.5f, new Color(255, 255, 255, 0) * 0.2f, 0, 1f, 0.78f, -MathHelper.Pi / 24, 0, true);

            DrawSlash(new Color(255,215,64,255), new Color(200,0,180,200), new Color(255, 128, 0, 200), Color.Wheat * 0.2f, 0, 0.8f, 0, -MathHelper.Pi / 24, 0, true);

            DrawSlash(new Color(255, 255, 255, 0) * 0.2f, new Color(255, 255, 255, 0) * 0.2f, new Color(255, 255, 255, 0) * 0.1f, new Color(255, 255, 255, 0) * 0.2f, 0, 0.8f, 0.78f, -MathHelper.Pi / 6, 0, true);
        }
        else
        {
            //float num = Projectile.localAI[0] / Projectile.ai[1];
            //DrawSlash(new Color(0, 0, 0, 255), new Color(0, 0, 0, 200), new Color(0, 0, 0, 200), new Color(0, 0, 0, 128), 128, 1f, 0.78f, -MathHelper.Pi / 6, 0, true);
            //DrawSlash(new Color(0, 0, 0, 255), new Color(0, 0, 0, 200), new Color(0, 0, 0, 200), new Color(0, 0, 0, 128), 128, 1.1f, 0f, -MathHelper.Pi / 6, 0, true);

            DrawSlash(new Color(255, 255, 255, 0) * 0.7f, new Color(0, 0, 0, 200) * 0.6f, new Color(0, 0, 0, 200) * 0.5f, new Color(255, 255, 255, 0) * 0.2f, 0, 1f, 0.78f, -MathHelper.Pi / 24, 0, true);

            DrawSlash(new Color(255, 215, 64, 255), new Color(200, 0, 180, 200), new Color(255, 128, 0, 200), Color.Wheat * 0.2f, 0, 0.8f, 0, -MathHelper.Pi / 24, 0, true);

            DrawSlash(new Color(255, 255, 255, 0) * 0.2f, new Color(255, 255, 255, 0) * 0.2f, new Color(255, 255, 255, 0) * 0.1f, new Color(255, 255, 255, 0) * 0.2f, 0, 0.8f, 0.78f, -MathHelper.Pi / 6, 0, true);
        }
        //if (Math.Abs(Projectile.ai[0]) > 0.2f)
        //{
        //    DrawSlash(Color.White * 0.5f, Color.White * 0.5f, Color.White * 0.5f, Color.White * 0.5f, 0, 1f, 0f, -MathHelper.Pi / 12, 0, true);
        //    DrawSlash(Color.Gold * 0.5f, Color.Purple * 0.5f, Color.Red * 0.5f, Color.Wheat * 0.5f, 0, 0.8f, 0f, -MathHelper.Pi / 12, 0, false);
        //}

        return false;
    }
    public override void AI()
    {
        base.AI();
        float num = Projectile.localAI[0] / Projectile.ai[1];
        float num2 = Projectile.ai[0];
        if (Math.Abs(num2) < 0.2f)
        {
                
            Projectile.rotation += (float)Math.PI * 6f * num2 * 10f * num;
            float num7 = Utils.Remap(Projectile.localAI[0], 10f, Projectile.ai[1] - 5f, 0f, 1f);
            Projectile.position += Projectile.velocity.SafeNormalize(Vector2.Zero) * (240 * num7);
            Projectile.scale += num7 * 0.7f;
        }
        else
        {
            float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
            Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
            if (Main.rand.NextFloat() * 0.5f < Projectile.Opacity)
            {
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), DustID.DemonTorch, vector3 * 3f, 255, default, 1f);
                dust1.noGravity = true;
                int[] Dusts = { DustID.CorruptTorch, DustID.HallowedTorch, DustID.BoneTorch,DustID.DemonTorch,DustID.WhiteTorch,DustID.DesertTorch,DustID.IchorTorch};
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 120f * Projectile.scale + 20f * Projectile.scale), Dusts[Main.rand.Next(Dusts.Length)], vector3 * 2f, 0, Color.White, 1f);
                dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.5f;
                dust2.noGravity = true;
                dust2.noLightEmittence = true;
            }
        }

        //if (Main.rand.NextFloat() * 20f < Projectile.Opacity)
        //{
        //    Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, vector3 * 0.5f, 100, Color.Lerp(Color.Purple, Color.White, Main.rand.NextFloat()), 0.4f);
        //    dust2.noGravity = true;
        //}
        //if (Main.rand.NextFloat() * 3f < Projectile.Opacity)
        //{
        //    Dust.NewDustPerfect(vector2, 43, vector3 * 0.1f, 100, Color.White * Projectile.Opacity, 1.2f * Projectile.Opacity);
        //}
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
        ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        particleOrchestraSettings.PositionInWorld = positionInWorld;
        ParticleOrchestraSettings settings = particleOrchestraSettings;
        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);
    }
    
    public override void OnHitPlayer(Player target, Player.HurtInfo info) {
        if (!info.PvP) {
            return;
        }
        
        Vector2 positionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
        ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
        particleOrchestraSettings.PositionInWorld = positionInWorld;
        ParticleOrchestraSettings settings = particleOrchestraSettings;
        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
        ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        int debuffCount = 0;
        for (int i = 0; i < target.buffType.Length; i++)
        {
            if (Main.debuff[target.buffType[i]])
            {
                debuffCount++;
            }
        }
        if (debuffCount > 0) {
            
            if (target.boss)
            {
                modifiers.FinalDamage *= 1.2f * debuffCount;
            }
            else
            {
                modifiers.FinalDamage *= 1.45f * debuffCount;
            }
        }
    }
}
