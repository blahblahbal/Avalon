using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class AncientSandy : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 20 / 16;
        Projectile.height = dims.Height * 20 / 16 / Main.projFrames[Projectile.type];
        Projectile.scale = 1f;
        Projectile.alpha = 255;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 3600;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        var newColor2 = default(Color);
        var num972 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Gold, 0f, 0f, 100, newColor2, 2f);
        Main.dust[num972].noGravity = true;
        return;
    }
}
