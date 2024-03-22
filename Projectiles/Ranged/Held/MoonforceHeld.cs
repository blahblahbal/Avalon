using Avalon.Common.Templates;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Held
{
    public class MoonforceHeld : LongbowTemplate
    {
        public override void PostAI()
        {
            if (Main.player[Projectile.owner].channel)
            {
                float spin = Main.rand.NextFloat(0, MathHelper.TwoPi);
                Dust d = Dust.NewDustPerfect(Projectile.Center + new Vector2(30 * Power, 0).RotatedBy(spin) + new Vector2(10 + (4 * (3 - Projectile.frame)), 0).RotatedBy(Projectile.rotation), Main.rand.Next(DustID.CorruptTorch, DustID.JungleTorch), new Vector2(-3 * Power, 0).RotatedBy(spin));
                d.noGravity = true;
                d.scale = 0.2f;
                d.scale += Power;
            }
        }
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
        {
            SoundEngine.PlaySound(SoundID.Item14,Projectile.position);
            float ScaleMod = 1 + (Power * 5);
            Projectile P = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Projectile.owner);
            if (Power > 0.3f)
            {
                SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
                P.usesLocalNPCImmunity = true;
                P.localNPCHitCooldown = 30;
                P.extraUpdates++;
                P.Resize((int)(P.width * ScaleMod), (int)(P.height * ScaleMod));
                P.GetGlobalProjectile<MoonlightArrowVisuals>().Moonlight = true;
                if (P.penetrate > 0)
                    P.penetrate += (int)(Power * 6);
                P.usesLocalNPCImmunity = true;
                P.localNPCHitCooldown = 30;
                P.netUpdate = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            DefaultBowDraw(new Color(200,200,200,128), Vector2.Zero);
            if (FullPowerGlow > 0 && Main.myPlayer == Projectile.owner)
            {
                DefaultBowDraw(NotificationColor * FullPowerGlow, Vector2.Zero);
            }
            if (Main.player[Projectile.owner].channel)
            {
                DrawArrow(lightColor, new Vector2(0,-1));
                for (int i = 0; i < 4; i++)
                {
                    Color arrowColor = Color.Lerp(Color.Blue, Color.Red, Main.masterColor) * 0.6f;
                    arrowColor.A = 0;
                    DrawArrow(arrowColor * Power, new Vector2(0, -1) + new Vector2(Power * 2,0).RotatedBy((i * MathHelper.PiOver2) + Main.timeForVisualEffects * 0.1f),true);
                }
            }
            return false;
        }
    }
    public class MoonlightArrowVisuals : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool Moonlight;

        public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            if (Moonlight)
            {
                width = 16;
                height = 16;
            }
            return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override void PostAI(Projectile projectile)
        {
            if (Moonlight)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, Main.rand.Next(DustID.CorruptTorch, DustID.JungleTorch));
                d.velocity = projectile.velocity.RotatedByRandom(0.1f) * 0.5f * (projectile.extraUpdates + 1);
                d.scale = MathHelper.Clamp(projectile.width / 30, 0.2f, 100);
                d.fadeIn = 1;
                d.noGravity = true;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Moonlight)
            {
                ParticleSystem.AddParticle(new MoonforceParticle(), Main.rand.NextVector2FromRectangle(target.Hitbox), projectile.velocity * 0.2f, default, (projectile.width * 2f) / 128f);
            }
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (Moonlight)
            {
                ParticleSystem.AddParticle(new MoonforceParticle(), Main.rand.NextVector2FromRectangle(target.Hitbox), projectile.velocity * 0.2f, default, (projectile.width * 2f) / 128f);
            }
        }
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (Moonlight)
            {
                SoundEngine.PlaySound(SoundID.Item110,projectile.position);
                for (int i = 0; i < projectile.width / 2; i++)
                {
                    Dust d = Dust.NewDustDirect(projectile.Center, 0, 0, Main.rand.Next(DustID.CorruptTorch,DustID.JungleTorch));
                    d.velocity *= Main.rand.NextFloat(3,8);
                    d.velocity -= projectile.oldVelocity * Main.rand.NextFloat(0,1);
                    d.scale = MathHelper.Clamp(projectile.width / 40,0.2f,100);
                    d.noGravity = true;
                    d.fadeIn = Main.rand.NextFloat(0, 2);
                }
                ParticleSystem.AddParticle(new MoonforceParticle(), projectile.Center, projectile.velocity * 0.2f, default,1.4f);
            }
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (Moonlight)
            {
                SpriteEffects Flip = projectile.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Vector2 drawPos = projectile.Center - Main.screenPosition;

                //Texture2D texture = TextureAssets.Extra[91].Value;
                //int frameHeight = texture.Height;
                //Rectangle frame = new Rectangle(0, frameHeight * 0, texture.Width, frameHeight);
                //Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255,64,200,0) * 0.6f, projectile.velocity.ToRotation() + MathHelper.PiOver2, new Vector2(texture.Width, frameHeight / 2f) / 2, projectile.width / 34f, Flip, 0);
                //Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255, 255, 255, 0) * 0.4f, projectile.velocity.ToRotation() + MathHelper.PiOver2, new Vector2(texture.Width, frameHeight / 2f) / 2, projectile.width / 34f * 0.8f, Flip, 0);

                Color alpha3 = projectile.GetAlpha(lightColor);
                Texture2D value21 = TextureAssets.Extra[91].Value;
                Rectangle value22 = value21.Frame();
                Vector2 origin10 = new Vector2((float)value22.Width / 2f, 10f);
                Vector2 vector34 = Vector2.Normalize(projectile.velocity) * -4;
                Vector2 spinningpoint = new Vector2(0f, -6f);
                float num189 = (float)Main.timeForVisualEffects / 60f;
                Vector2 vector35 = projectile.Center + projectile.velocity;
                Color color44 = Color.Lerp(Color.Purple,Color.Red,Main.masterColor) * 0.3f;
                Color color45 = Color.White * 0.5f;
                color45.A = 0;
                float FlameScale = projectile.width / 34f * 1.2f;
                Color color46 = color44;
                color46.A = 0;
                Color color47 = color44;
                color47.A = 0;
                Color color48 = color44;
                color48.A = 0;
                Main.EntitySpriteDraw(value21, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy((float)Math.PI * 2f * num189), value22, color46, projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin10, 1.1f * FlameScale, SpriteEffects.None);
                Main.EntitySpriteDraw(value21, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy((float)Math.PI * 2f * num189 + (float)Math.PI * 2f / 3f), value22, color47, projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin10, 0.8f * FlameScale, SpriteEffects.None);
                Main.EntitySpriteDraw(value21, vector35 - Main.screenPosition + vector34 + spinningpoint.RotatedBy((float)Math.PI * 2f * num189 + 4.18879032f), value22, color48, projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin10, 1f * FlameScale, SpriteEffects.None);
                for (float num191 = 0f; num191 < 1f; num191 += 0.5f)
                {
                    float num192 = num189 % 0.5f / 0.5f;
                    num192 = (num192 + num191) % 1f;
                    float num193 = num192 * 2f;
                    if (num193 > 1f)
                    {
                        num193 = 2f - num193;
                    }
                    Main.EntitySpriteDraw(value21, vector35 - Main.screenPosition + vector34, value22, color45 * num193, projectile.velocity.ToRotation() + (float)Math.PI / 2f, origin10, 0.3f + num192 * 0.9f, SpriteEffects.None);
                }

                Texture2D textureArrow = TextureAssets.Projectile[projectile.type].Value;
                int frameHeightArrow = textureArrow.Height;
                Rectangle frameArrow = new Rectangle(0, frameHeightArrow * 0, textureArrow.Width, frameHeightArrow);

                Main.EntitySpriteDraw(textureArrow, drawPos, frameArrow, projectile.GetAlpha(lightColor), projectile.velocity.ToRotation() + MathHelper.PiOver2, new Vector2(textureArrow.Width, frameHeightArrow / 2f) / 2, projectile.scale, Flip, 0);
                return false;
            }
            return base.PreDraw(projectile, ref lightColor);
        }
    }
}
