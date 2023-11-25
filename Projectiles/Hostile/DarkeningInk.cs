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
            Projectile.velocity.X += MathHelper.Clamp(Main.player[(int)Projectile.ai[0]].position.X - Projectile.position.X,-0.03f,0.03f);
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Blackout, 60 * 5);
    }
}
