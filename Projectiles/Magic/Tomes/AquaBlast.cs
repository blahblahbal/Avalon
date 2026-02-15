using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic.Tomes;

public class AquaBlast : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 8;
	}
	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.scale = 1f;
		Projectile.alpha = 0;
		Projectile.aiStyle = 1;
		Projectile.timeLeft = 3600;
		Projectile.friendly = true;
		Projectile.penetrate = 20;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.MaxUpdates = 2;
		Projectile.DamageType = DamageClass.Magic;
		AIType = ProjectileID.Bullet;
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(new SoundStyle("Avalon/Sounds/Item/SplashExplosion") { Volume = 1.2f, PitchVariance = 0.2f }, Projectile.position);
		Projectile.position.X = Projectile.position.X + Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.position.X = Projectile.position.X - Projectile.width / 2;
		Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;

		for (int dustCount = 0; dustCount < 50; dustCount++)
		{
			float rotation = Main.rand.NextFloat() * MathHelper.TwoPi;
			Dust D2 = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Clentaminator_Cyan, 0, 0, 100, new Color(), 1f);
			D2.velocity = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation)) * Main.rand.NextFloat(2.5f, 4f);
			D2.noGravity = true;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Slow, 60 * 10);
		target.AddBuff(BuffID.Wet, 60 * 10);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Slow, 60 * 10);
		target.AddBuff(BuffID.Wet, 60 * 10);
	}
	public override void AI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter > 5)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame > 7) Projectile.frame = 0;
		}

		for (int q = 0; q < 3; q++)
		{
			Vector2 velocity = Projectile.velocity.RotatedBy(Projectile.ai[2] * Projectile.direction);
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Cyan, velocity.X, velocity.Y, 0, new Color(140, 188, 250), 1f);
			d.velocity.X *= 0.5f;
			d.noGravity = true;
		}
	}
}
