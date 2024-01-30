using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class OilBottle : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width;
        Projectile.height = dims.Height;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.BoneGloveProj;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.alpha = 0;
        Projectile.scale = 1f;
        Projectile.tileCollide = true;
    }
    public override void OnKill(int timeLeft)
    {
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<OilExplosion>(), 22, 2);
    }
}
