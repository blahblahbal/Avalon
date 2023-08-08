using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class NaquadahLance : SpearTemplate
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 19;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.1f;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
    }
    protected override float HoldoutRangeMax => 160;
    protected override float HoldoutRangeMin => 40;
}
