using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class Cough : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.aiStyle = -1;
        Projectile.penetrate = 1;
        Projectile.alpha = 100;
        Projectile.friendly = false;
        Projectile.timeLeft = 720;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.scale = 1;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }

    public override void AI()
    {

        Projectile.alpha += 1;
        if (Projectile.alpha == 255) Projectile.Kill();

        Projectile.velocity = Projectile.velocity.RotatedByRandom(0.1f) * 0.99f;
        Projectile.rotation += Projectile.velocity.Length() * 0.1f;
        if (Projectile.scale < 0.9f)
        {
            Projectile.scale *= 0.99f;
        }
    }
    public override bool CanHitPlayer(Player target)
    {
        return Projectile.alpha < 210;
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        if (Projectile.ai[0] == 0)
        {
            target.AddBuff(BuffID.Venom, 5 * 16);
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = oldVelocity * Main.rand.NextFloat(-0.2f,0.2f);
        Projectile.tileCollide = false;
        return false;
    }
}
