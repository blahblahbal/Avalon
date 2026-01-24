using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Ammo
{
    public class PathogenicBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CursedBullet);
            Projectile.aiStyle = -1;
            Projectile.light = 0;
        }

        public override void AI()
        {
            Projectile.alpha-= 50;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, new Vector3(0.32f, 0.2f, 0.5f));
            //Dust d = Dust.NewDustDirect(Projectile.Center + new Vector2(0,-3), 0, 0, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            //d.noGravity = true;
            //d.fadeIn = 1.3f;
            //d.velocity *= 0.5f;
            //d.velocity += Projectile.velocity * 0.5f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            //    d.noGravity = true;
            //    d.fadeIn = 1.3f;
            //}
            target.AddBuff(ModContent.BuffType<Pathogen>(), 10 * 60);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            //    d.noGravity = true;
            //    d.fadeIn = 1.3f;
            //}
            target.AddBuff(ModContent.BuffType<Pathogen>(), 10 * 60);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            //for (int i = 0; i < 20; i++)
            //{
            //    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            //    d.noGravity = true;
            //    d.fadeIn = 1.3f;
            //}
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
