using Avalon.Projectiles.Hostile.DesertBeak;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Misc;

public class ExplosiveEggShrapnel : EggShrapnel
{
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(8);
		Projectile.aiStyle = 1;
		AIType = ProjectileID.Bullet;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.timeLeft = 25;
		Projectile.penetrate = 3;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.ignoreWater = true;
		Projectile.extraUpdates = 1;
	}
}
