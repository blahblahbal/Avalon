using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class Noxious : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 8;
        ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 15 * 16f;
        ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
    }

    public override void SetDefaults()
    {
        Projectile.extraUpdates = 0;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 99;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 1f;
    }
}
