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
		Projectile.tileCollide = false;
	}
	public override void AI()
	{
		for (int i = 0; i < 3; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonWater);
			d.noGravity = true;
			d.velocity *= 0.5f;
			d.velocity += Projectile.velocity * 0.3f;
		}
		if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			Projectile.tileCollide = true;
		//Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.DungeonSpirit);
		//d2.noGravity = true;
		//d2.scale = 0.5f + (float)Math.Abs(Math.Sin(Projectile.ai[0] * 0.1f));
		//d2.velocity += Projectile.velocity * 0.3f;

		Projectile.velocity.Y += 0.1f;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item112 with { PitchVariance = 0.4f, Pitch = -0.3f, Volume = 0.4f, MaxInstances = 10}, Projectile.position);
		if (Projectile.velocity.X != Projectile.oldVelocity.X)
		{
			Projectile.velocity.X = -Projectile.oldVelocity.X * Main.rand.NextFloat(0.8f, 1f);
		}
		if (Projectile.velocity.Y != Projectile.oldVelocity.Y)
		{
			Projectile.velocity.Y = -Projectile.oldVelocity.Y * Main.rand.NextFloat(0.8f, 1f);
		}
		Projectile.velocity = Projectile.velocity.LengthClamp(16, 2);
		if(Projectile.velocity == Vector2.Zero)
		{
			Projectile.velocity = Main.rand.NextVector2CircularEdge(1, 11) * Main.rand.NextFloat(2,16);
		}
		Projectile.ai[0]++;
		if (Projectile.ai[0] > 5)
		{
			Projectile.Kill();
		}
		Projectile.netUpdate = true;
		return false;
	}
}
