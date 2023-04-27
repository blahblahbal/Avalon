using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PeeBullet : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = 1;
        AIType = ProjectileID.WoodenArrowFriendly;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.light = 0.7f;
        Projectile.alpha = 0;
        Projectile.MaxUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
    }
}
