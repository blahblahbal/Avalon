using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Runtime.Intrinsics.X86;

namespace Avalon.Projectiles.Ranged
{
    public class NapalmBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.hide = false;
            Projectile.aiStyle = -1;
            Projectile.Size = new Vector2(16);
            Projectile.light = 0;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity= true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            if(Projectile.alpha > 0)
                Projectile.alpha-= 10;
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0)
            {
                Projectile.rotation += Projectile.direction * 0.3f;
                if (Projectile.alpha < 100 && Projectile.velocity.Y < 16)
                Projectile.velocity.Y += 0.25f;
                if (Projectile.alpha < 200)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                    d.fadeIn = 1f;
                    d.scale = 1.5f;
                    d.velocity = Projectile.velocity * 0.5f + Main.rand.NextVector2Circular(1, 1);
                    d.noGravity = true;
                }
            }
            else if(Projectile.owner == Main.myPlayer && Projectile.ai[0] > 7 && Projectile.ai[1] == 1)
            {
                Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -3)), ProjectileID.Flames, Projectile.damage / 2, Projectile.knockBack / 5, Projectile.owner);
                p.tileCollide = false;
                p.scale *= 0.5f;
                Projectile.ai[0] = 0;
            }
            else if (Projectile.owner == Main.myPlayer && Projectile.ai[0] > 7 && Projectile.ai[1] == 2)
            {
                Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-2f, 2f)), ProjectileID.Flames, Projectile.damage / 3, Projectile.knockBack / 5, Projectile.owner);
                p.tileCollide = false;
                p.scale *= 0.5f;
                Projectile.ai[0] = 0;

                if (Main.npc[(int)Projectile.ai[2]].active)
                {
                    //p.velocity += Main.npc[(int)Projectile.ai[2]].velocity * 0.5f;
                    Projectile.position = Main.rand.NextVector2FromRectangle(Main.npc[(int)Projectile.ai[2]].Hitbox) + Main.npc[(int)Projectile.ai[2]].velocity;
                }
                else
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] > 0)
            {
                Dust d = Dust.NewDustPerfect(Projectile.position, DustID.Torch, Main.rand.NextVector2Circular(4, 2) + new Vector2(0, -2), 0);
            }
            Lighting.AddLight(Projectile.Center, 0.5f, 0.3f, 0f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 160);
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[2] = target.whoAmI;
                Projectile.hide = true;
                Projectile.ai[1] = 2;
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
                Projectile.timeLeft = 60;
                Projectile.Resize(0, 0);
                Projectile.velocity *= 0;
                Projectile.tileCollide = false;
                Projectile.alpha = 256;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            Projectile.hide = true;
            Projectile.ai[1] = 1;
            SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);
            Projectile.timeLeft = 120;
            Projectile.Resize(0, 0);
            Projectile.velocity *= 0;
            Projectile.tileCollide = false;
            Projectile.alpha = 256;
            return false;
        }
    }
}
