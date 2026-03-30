using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class IridiumGreatswordBubble : ModProjectile
{
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0.8f) * Projectile.Opacity * (Projectile.scale - 0.3f);
	}
	public override void SetDefaults()
	{
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 14;
		Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
		Projectile.penetrate = -1;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 120;
		Projectile.alpha = 255;
	}
	public override void AI()
	{
		Projectile.localAI[0]++;
		Projectile.velocity *= 0.93f;
		Projectile.rotation += Projectile.velocity.X * 0.1f;
		Projectile.scale = 1f + MathF.Sin(Projectile.localAI[0] * 0.1f + Projectile.identity) * 0.1f;
		if(Projectile.Opacity < 1f)
		{
			Projectile.Opacity += 0.1f;
		}
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
		d.velocity = Projectile.velocity * Main.rand.NextFloat(0.5f);
		d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f) * 0.2f * Projectile.Opacity;
		d.noGravity = true;
		d.scale = Projectile.velocity.Length() / 4;
		d.fadeIn = Projectile.velocity.Length() / 8;

		if (Projectile.ai[0] > 0 || !Main.player[Projectile.owner].channel)
		{
			Projectile.timeLeft++;
			Projectile.ai[0]++;
			if (Projectile.ai[0] > 20 + MathF.Sin(Projectile.identity) * 10)
			{
				for (int i = 0; i < Projectile.localNPCImmunity.Length; i++)
				{
					Projectile.localNPCImmunity[i] = 0;
				}
				Projectile.damage *= 2;
				Projectile.Resize(100, 100);
				Projectile.Damage();

				SoundEngine.PlaySound(SoundID.DD2_LightningBugZap, Projectile.position);
				int iterations = 13;
				for (int i = 0; i < iterations; i++)
				{
					SparkleParticle p = new();
					p.ColorTint = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
					p.FadeInEnd = Main.rand.NextFloat(4, 7);
					p.FadeOutStart = p.FadeInEnd;
					p.FadeOutEnd = Main.rand.NextFloat(13, 18);
					p.Scale = new Vector2(1, 0.6f);
					p.Velocity = new Vector2(Main.rand.NextFloat(2, 8)).RotatedBy((i * MathHelper.TwoPi / iterations) + Main.rand.NextFloat(-0.3f, 0.3f));
					p.AccelerationPerFrame = -p.Velocity / p.FadeOutEnd;
					p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;
					p.DrawHorizontalAxis = false;
					ParticleSystem.NewParticle(p, Projectile.Center);
				}
				Projectile.Kill();
			}
		}
		foreach(Projectile p in Main.ActiveProjectiles)
		{
			if (p.whoAmI == Projectile.whoAmI)
				continue;
			if(p.type == Type && p.Center.Distance(Projectile.Center) < 8)
			{
				p.velocity -= p.Center.DirectionTo(Projectile.Center) * 0.1f;
				Projectile.velocity -= Projectile.Center.DirectionTo(p.Center) * 0.1f;
			}
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
		if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		for(int i = 0; i < 10; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
			d.velocity = Main.rand.NextVector2Circular(3, 3);
			d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f) * 0.4f;
			d.noGravity = true;
		}
	}
}
