using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class WallOfFleshDeathAnimation : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.WallofFlesh;
        }
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if(npc.life <= 0)
            {
                SoundStyle ExplodyOverlapEdition = new SoundStyle("Terraria/Sounds/Item_14")
                {
                    Volume = Main.rand.NextFloat(2f, 3),
                    PitchVariance = 1f,
                    MaxInstances = 50,
                };
                SoundEngine.PlaySound(ExplodyOverlapEdition, npc.position);
                ParticleSystem.AddParticle(new ExplosionParticle(), npc.Center, default, default, Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(3.9f, 4.2f));
                ParticleSystem.AddParticle(new ScreenFlash(), npc.Center, default, Color.OrangeRed);
                ParticleSystem.AddParticle(new ScreenFlash(), npc.Center, default, Color.White);
            }
        }
        public override void PostAI(NPC npc)
        {
            if (npc.life < 500 && Main.timeForVisualEffects % 10 == 0)
            {
                Particle P = ParticleSystem.AddParticle(new ExplosionParticle(), new Vector2(npc.Center.X + Main.rand.Next(-40, 40), npc.Center.Y + Main.rand.Next(-300, 300)), default, default, Main.rand.NextFloat(MathHelper.TwoPi), Main.rand.NextFloat(0.9f, 1.2f));
                if (Main.rand.NextBool(6))
                {
                    Gore.NewGore(npc.GetSource_FromThis(), P.Position, Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(4, 7),Main.rand.Next(140,143));
                }
                for (int i = 0; i < 5; i++)
                {
                    int d = Dust.NewDust(P.Position, 0, 0, DustID.Torch, 0, 0, 0, default, 2f);
                    Main.dust[d].velocity = Main.rand.NextVector2Circular(6, 6);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].fadeIn = 2.3f;
                    Main.dust[d].customData = 0;
                }
                for (int i = 0; i < 10; i++)
                {
                    int d = Dust.NewDust(P.Position, 0, 0, DustID.Torch, 0, 0, 0, default, 2f);
                    Main.dust[d].velocity = Main.rand.NextVector2Circular(5, 5);
                    Main.dust[d].fadeIn = Main.rand.NextFloat(1, 2);
                    Main.dust[d].customData = 0;
                }
                for (int i = 0; i < 10; i++)
                {
                    int d = Dust.NewDust(P.Position, 0, 0, DustID.Smoke, 0, 0, 0, default, 1.4f);
                    Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-3, 0).RotatedBy(npc.velocity.ToRotation());
                    Main.dust[d].noGravity = !Main.rand.NextBool(10);
                }
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(P.Position, 0, 0, DustID.SolarFlare, 0, 0, 0, default, 1.4f);
                    //Main.dust[d].color = Color.Red;
                    Main.dust[d].velocity = Main.rand.NextVector2Circular(10, 6) + new Vector2(-5, 0).RotatedBy(npc.velocity.ToRotation());
                    Main.dust[d].noGravity = Main.rand.NextBool(3);
                }

                SoundStyle ExplodyOverlapEdition = new SoundStyle("Terraria/Sounds/Item_14")
                {
                    Volume = Main.rand.NextFloat(0.5f,1),
                    PitchVariance = 1f,
                    MaxInstances = 50,
                };
                SoundEngine.PlaySound(ExplodyOverlapEdition, P.Position);
            }
        }
    }
    public class ScreenFlash : Particle
    {
        public override void OnSpawn()
        {
            FrontLayer = true;
        }
        public override void Update()
        {
            TimeInWorld++;

            if (TimeInWorld < 20)
            {
                ai1 += 0.07f;
            }
            else ai1 -= 0.04f;

            if (TimeInWorld <= 0)
                Active = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color.A = 0;
            Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle(0, 0, 1, 1), Color * ai1);
        }
    }
}
