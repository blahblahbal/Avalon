using System;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;

namespace Avalon.Projectiles.Melee; 

public class VertexSlash : EnergySlashTemplate
{
    public override bool PreDraw(ref Color lightColor)
    {
        DrawSlash(Color.White * 0.5f, Color.Black * 0.5f, Color.Black * 0.5f, Color.White * 0.5f, 0, 1f, 0f, -MathHelper.Pi / 12, -MathHelper.Pi / 16, true);
        DrawSlash(Color.Gold * 0.5f, Color.Purple * 0.5f, Color.Red * 0.5f, Color.Wheat * 0.5f, 0, 0.8f, 0f, -MathHelper.Pi / 12, -MathHelper.Pi / 16, true);

        //DrawSlash(Color.Lerp(Color.White, Color.Red, Main.masterColor), Color.Gray, Color.Lerp(Color.Black, Color.DarkRed, Main.masterColor), Color.White, 0, 1f, 0f, -MathHelper.Pi / 12, -MathHelper.Pi / 16, true);

        //DrawSlash(Color.Lerp(Color.Gold, new Color(120, 109, 204),Main.masterColor),
        //    Color.Lerp(Color.Purple, new Color(61, 58, 126), Main.masterColor),
        //    Color.Lerp(Color.Red, new Color(27, 25, 57), Main.masterColor), Color.Wheat, 0, 0.8f, 0f, -MathHelper.Pi / 12, -MathHelper.Pi / 16, false);

        //DrawSlash(Color.Pink,Color.Red,Color.Purple,Color.Wheat,0,0.8f,0f,-MathHelper.Pi / 16, -MathHelper.Pi / 16, false);
        //DrawSlash(new Color(120,109,204),new Color(61,58,126),new Color(27,25,57), Color.Transparent, 0, 0.6f, 0f, -MathHelper.Pi / 16, -MathHelper.Pi / 16, false);
        return false;
    }
    public override void AI()
    {
        base.AI();
        float num = Projectile.localAI[0] / Projectile.ai[1];
        float num2 = Projectile.ai[0];
        if (Math.Abs(num2) < 0.2f)
        {
                
            Projectile.rotation += (float)Math.PI * 4f * num2 * 10f * num;
            float num7 = Utils.Remap(Projectile.localAI[0], 10f, Projectile.ai[1] - 5f, 0f, 1f);
            Projectile.position += Projectile.velocity.SafeNormalize(Vector2.Zero) * (80f * num7);
            Projectile.scale += num7 * 0.4f;
        }

        float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, vector3 * 0.5f, 100, Color.Lerp(Color.Purple, Color.White, Main.rand.NextFloat()), 0.4f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
            dust2.noGravity = true;
        }
        if (Main.rand.NextFloat() * 3f < Projectile.Opacity)
        {
            Dust.NewDustPerfect(vector2, 43, vector3 * 0.1f, 100, Color.White * Projectile.Opacity, 1.2f * Projectile.Opacity);
        }
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
