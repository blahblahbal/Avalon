using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.Armageddon
{
    public class DarkMatterSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(20, 2);
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.timeLeft = 1000;
            Projectile.tileCollide = false;
        }
        public override void OnSpawn(IEntitySource source)
        {
        }
        public override void AI()
        {
            if(Projectile.timeLeft == 1000)
            {
                Projectile.frame = Main.rand.Next(6);
            }
            Vector2 oldBottom = Projectile.Bottom;
            if (Projectile.ai[1] < 1)
            {
                Projectile.ai[1] += 0.08f;
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption, 0, -3, 128);
                }
            }
            else if(Projectile.timeLeft > 30)
            {
                Projectile.timeLeft = 30;
            }
            if (Main.rand.NextBool(10))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Corruption, 0, 0, 128);
            }
            Projectile.height = (int)(192 * Projectile.ai[1] * Projectile.scale);
            Projectile.Bottom = oldBottom;

            if(Projectile.timeLeft < 10)
            {
                Projectile.alpha += 255 / 10;
                Projectile.ai[1] -= 0.1f;
                Projectile.velocity.Y += 0.2f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Vector2 Scale = new Vector2(MathHelper.Clamp(Projectile.ai[1] * 2, 0, 1), Projectile.ai[1]) * Projectile.scale;
            Main.EntitySpriteDraw(tex, Projectile.Bottom - Main.screenPosition + new Vector2(0,4), new Rectangle(0, tex.Height / 6 * Projectile.frame, tex.Width, tex.Height / 6), new Color(lightColor.R * Projectile.Opacity, lightColor.G * Projectile.Opacity, lightColor.B * Projectile.Opacity, 255) * Projectile.Opacity, -MathHelper.PiOver2, new Vector2(0, tex.Height / 12), Scale, SpriteEffects.None);
            return false;
        }
    }

    public class DarkMatterSpikeSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(9, 2);
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.extraUpdates = 20;
            Projectile.timeLeft = 600 * 3;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 120 == 0 && Main.netMode != NetmodeID.MultiplayerClient && Projectile.velocity.Y == 0)
            {
                SoundStyle Spawn = new SoundStyle("Terraria/Sounds/Item_50")
                {
                    Volume = 0.2f,
                    PitchVariance = 0.5f,
                    MaxInstances = 30,
                    Pitch = -0.6f
                };

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Top, Vector2.Zero, ModContent.ProjectileType<DarkMatterSpike>(), Projectile.damage, 0, Projectile.owner);
                SoundEngine.PlaySound(Spawn, Projectile.position);
				ParticleSystem.NewParticle(new ColorExplosion( new Color(64, 0, 78, 128),Main.rand.NextFloatDirection(), 0.5f),Projectile.Top);
            }
            if(Projectile.tileCollide)
                Projectile.velocity.Y += 4;
            else if (!Collision.SolidCollision(Projectile.position, 2, 2))
            {
                Projectile.tileCollide = true;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(Projectile.velocity.X == 0)
            {
                Projectile.Kill();
            }
            Projectile.velocity.Y = 0;
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
    }
}
