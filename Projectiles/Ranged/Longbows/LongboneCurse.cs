using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class LongboneCurse : ModProjectile
{
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.WaterBolt}";
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.WaterBolt);
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.timeLeft = 9 * 60;
		Projectile.extraUpdates = 1;
	}
	public override void AI()
	{
		Projectile.ai[0]++;
		for (int i = 0; i < 3; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonWater);
			d.noGravity = true;
		}
		Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonSpirit);
		d2.noGravity = true;
		d2.scale = 0.5f + (float)Math.Abs(Math.Sin(Projectile.ai[0] * 0.1f));
		d2.velocity = Projectile.velocity * 0.3f;

		Projectile.velocity.Y += 0.1f;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		if (Projectile.velocity.X != Projectile.oldVelocity.X)
		{
			Projectile.velocity.X = -Projectile.oldVelocity.X * Main.rand.NextFloat(0.8f, 1.2f);
			Projectile.netUpdate = true;
		}
		if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
		{
			Projectile.velocity.Y = -Projectile.oldVelocity.Y * Main.rand.NextFloat(0.8f, 1.2f);
			Projectile.netUpdate = true;
		}

		return false;
	}
}
