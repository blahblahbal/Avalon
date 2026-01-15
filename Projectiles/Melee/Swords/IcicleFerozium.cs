using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class IcicleFerozium : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.width = Projectile.height = 12;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 2;
		Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 20f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
			Projectile.velocity.X = Projectile.velocity.X * 0.97f;
		}
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}
}
