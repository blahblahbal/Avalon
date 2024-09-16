using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.WallOfSteel;

public class WoSLaserSmall : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.hostile = true;
        Projectile.penetrate = 1;
        Projectile.light = 0.8f;
        Projectile.alpha = 0;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 300;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.DeathLaser;
    }
}
