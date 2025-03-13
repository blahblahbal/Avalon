using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee
{
    public class TetanusChakram : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ThornChakram);
			Projectile.width = Projectile.height = 14;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitAnything();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            OnHitAnything();
        }

        void OnHitAnything()
        {
            for (int i = 0; i < 10; i++)
            {
                int Type = Main.rand.NextBool(5) ? DustID.Iron : DustID.Blood;
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, Type);
                if(d.type == DustID.Blood)
                {
                    d.alpha = 128;
                }
                d.noGravity = !Main.rand.NextBool(3);
            }
        }
    }
}
