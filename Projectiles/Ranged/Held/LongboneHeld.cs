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
    public class LongboneHeld : LongbowTemplate
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            //DrawOriginOffsetX = 20;
            DrawOffsetX = -16;
            DrawOriginOffsetY = -25;
        }
        public override float HowFarShouldTheBowBeHeldFromPlayer => 28f;
        public override void Shoot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, float Power)
        {
                Projectile P = Projectile.NewProjectileDirect(source, position, velocity * Main.rand.NextFloat(0.5f + (Power / 2f), 1), type, damage, knockback, Projectile.owner);
                P.GetGlobalProjectile<LongboneCurseArrow>().Longbone = (byte)(Power * 4f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            DefaultBowDraw(lightColor, Vector2.Zero);
            if (FullPowerGlow > 0 && Main.myPlayer == Projectile.owner)
            {
                DefaultBowDraw(NotificationColor * FullPowerGlow, Vector2.Zero);
            }
            if (Main.player[Projectile.owner].channel)
            {
                DrawArrow(lightColor, new Vector2(0, -1));
                for (int i = 0; i < 4; i++)
                {
                    Color arrowColor = Color.Lerp(Color.Blue, Color.Transparent, Main.masterColor) * 0.3f;
                    arrowColor.A = 0;
                    DrawArrow(arrowColor * Power, new Vector2(0, -1) + new Vector2(Power * 2, 0).RotatedBy((i * MathHelper.PiOver2) + Main.timeForVisualEffects * 0.1f), true);
                }
            }
            return false;
        }
    }
    public class LongboneCurseArrow : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public byte Longbone;

        public override void PostAI(Projectile projectile)
        {
            if(Longbone > 0)
            {
                Dust d = Dust.NewDustPerfect(projectile.Center, 172, projectile.velocity * 0.5f);
                d.noGravity = true;
                d.scale = 0.6f + (Longbone / 4f);
            }
        }
        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (projectile.owner == Main.myPlayer && Longbone > 0)
            {
                SoundEngine.PlaySound(SoundID.Item20, projectile.position);
                Particle p = ParticleSystem.AddParticle(new ColorExplosion(), projectile.Center, default, new Color(32, 77, 255, 64), Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(0.1f, 0.35f) + (Longbone * 0.1f));
                ParticleSystem.AddParticle(new ColorExplosion(), p.Position, default, new Color(64, 128, 255, 64), p.ai1 + MathHelper.PiOver2, p.ai2 * 0.8f);
                for (int i = 0; i < Longbone * 7; i++)
                {
                    float rand = Main.rand.NextFloat(0, MathHelper.TwoPi);
                    Dust d = Dust.NewDustPerfect(projectile.Center + new Vector2(0, p.ai2 * Main.rand.Next(24,32)).RotatedBy(rand).RotatedByRandom(0.3),DustID.DungeonWater,new Vector2(0,Main.rand.Next(3, 5)).RotatedBy(rand).RotatedByRandom(0.1f));
                    d.noGravity = !Main.rand.NextBool(6);
                    if (d.noGravity)
                    {
                        d.fadeIn = Main.rand.NextFloat();
                        d.scale = 2;
                    }
                }
                for (int i = 0; i < Longbone; i++)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Normalize((Collision.SolidCollision(projectile.position + projectile.velocity, projectile.width, projectile.height) ? -projectile.oldVelocity : projectile.oldVelocity)).RotatedByRandom(Longbone * 0.03f) * Main.rand.NextFloat(6, 10), ModContent.ProjectileType<LongboneCurse>(), projectile.damage / 3, projectile.knockBack / 2f, projectile.owner);
                }
            }
        }
    }
    public class LongboneCurse : ModProjectile
    { 
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WaterBolt);
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 9 * 60;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            for (int i = 0; i < 3; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonWater);
                d.noGravity = true;
            }
            Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonSpirit);
            d2.noGravity = true;
            d2.scale = 0.5f + (float)Math.Abs(Math.Sin(Projectile.ai[0] * 0.1f));
            d2.velocity = Projectile.velocity * 0.3f;

            Projectile.velocity.Y += 0.1f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10,Projectile.position);
            if(Projectile.velocity.X != Projectile.oldVelocity.X)
            {
                Projectile.velocity.X = -Projectile.oldVelocity.X * Main.rand.NextFloat(0.8f,1.2f);
                Projectile.netUpdate = true;
            }
            if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
            {
                Projectile.velocity.Y = -Projectile.oldVelocity.Y * Main.rand.NextFloat(0.8f, 1.2f);
                Projectile.netUpdate = true;
            }

            return false;
        }
    }
}
