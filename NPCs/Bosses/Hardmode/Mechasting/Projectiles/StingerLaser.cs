using Avalon.Data.Sets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode.Mechasting.Projectiles;

public class StingerLaser : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileSets.DontReflect[Type] = true;
	}
	public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 20;
        Projectile.height = dims.Height * 4 / 20 / Main.projFrames[Projectile.type];
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.penetrate = 1;
        Projectile.light = 0.8f;
        Projectile.alpha = 0;
        Projectile.scale = 1.2f;
        Projectile.timeLeft = 300;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.DeathLaser;
    }
}
