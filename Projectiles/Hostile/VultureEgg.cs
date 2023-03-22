using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class VultureEgg : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = true;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.timeLeft = 100;
        Projectile.light = 1f;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.scale = 1.6f;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.ai[0] < 2)
        {
            Projectile.ai[0]++;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1.5)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.7f;
            }
        }
        else
            Projectile.Kill();
        return false;
    }
    public override void Kill(int timeLeft)
    {
        NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, NPCID.Vulture);
    }
    public override void AI()
    {
        if (Projectile.velocity.Y == 0f)
        {
            Projectile.velocity.X *= 0.94f;
        }
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        Projectile.velocity.Y += 0.2f;
    }
}
