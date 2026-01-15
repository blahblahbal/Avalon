using Avalon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Misc.SolarSystem;

public class Jupiter : Planet
{
    public override int Radius { get; set; } = 136;
    public override string PlanetName { get; set; } = "Jupiter";
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 120;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 3;
        Projectile.timeLeft = 300;
        Projectile.ignoreWater = true;
        DrawOffsetX = -(int)(dims.Width / 2 - Projectile.Size.X / 2);
        DrawOriginOffsetY = -(int)(dims.Width / 2 - Projectile.Size.Y / 2);
    }
}
