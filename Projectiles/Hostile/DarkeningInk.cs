using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile; 

public class DarkeningInk : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BulletDeadeye);
        Projectile.hide = true;
        Projectile.aiStyle = -1;
        Projectile.Size = new Microsoft.Xna.Framework.Vector2(8);
        Projectile.extraUpdates = 2;
        Projectile.light = 0;
    }
    public override void AI()
    {
        Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Wraith, Projectile.velocity.X, Projectile.velocity.Y);
        D.noGravity = true;
        D.velocity *= 0.1f;
        D.scale *= 1.5f;

        if (!Main.player[(int)Projectile.ai[0]].dead)
        {
            float MaxVelocity = 0.05f;
            Projectile.velocity.X += MathHelper.Clamp(Main.player[(int)Projectile.ai[0]].position.X - Projectile.position.X, -MaxVelocity, MaxVelocity);
            //if (Main.expertMode)
            //{
            //    Projectile.velocity += Projectile.Center.DirectionTo(Main.player[(int)Projectile.ai[0]].Center) * new Vector2(0.2f,0.03f);
            //    if (Projectile.velocity.Y > 3)
            //        Projectile.velocity.Y = 3;
            //}
        }
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        target.AddBuff(BuffID.Blackout, 60 * 5);
    }
}
