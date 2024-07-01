using Avalon.Buffs.Debuffs;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged
{
    public class PathogenicDart : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CursedDart);
            Projectile.alpha = 0;
            Projectile.light = 0;
        }

        public override void PostAI()
        {
            //Dust d = Dust.NewDustDirect(Projectile.Center + new Vector2(0, -3), 0, 0, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
            //d.noGravity = true;
            //d.fadeIn = 1f;
            //d.velocity *= 0.5f;
            //d.velocity += Projectile.velocity * 0.5f;
            Projectile.ai[2]++;
            if (Projectile.ai[2] > Main.rand.Next(10, 20))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(1, 1), ModContent.ProjectileType<PathogenDartTrail>(), (int)(Projectile.damage * 1.25f), 0, Projectile.owner);
                Projectile.ai[2] = 0;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Pathogen>(), 8 * 60);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Pathogen>(), 8 * 60);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
		public override void OnKill(int timeLeft)
		{
			// too powerful but code could be recycled idk
			//int amount = Main.rand.Next(2) + 3;
			//Vector2 rotation = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(0.25f, 0.65f);
			//for (int i = 0; i < amount; i++)
			//{
			//	Vector2 perturbedSpeed = rotation.RotatedBy(MathHelper.ToRadians(Main.rand.Next(270, 450) / amount * i));
			//	Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, perturbedSpeed, ModContent.ProjectileType<PathogenDartTrail>(), (int)(Projectile.damage * 0.7f), 0, Projectile.owner);
			//}

			for (int i = 0; i < 6; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PathogenDust>(), 0, 0, 128, default, 1);
				d.noGravity = true;
				d.fadeIn = 1.3f;
			}
		}
	}
}
