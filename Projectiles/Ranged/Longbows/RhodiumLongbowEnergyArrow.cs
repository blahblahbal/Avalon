using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class RhodiumLongbowEnergyArrow : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
		AIType = ProjectileID.WoodenArrowFriendly;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0.5f);
	}
	public override void AI()
	{
		if (Projectile.ai[0] == 0)
		{
			SoundEngine.PlaySound(SoundID.Item114 with { Volume = 0.5f, PitchVariance = 1f, MaxInstances = 10}, Projectile.position);
			SparkleParticle p = new();
			p.ColorTint = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
			p.FadeInEnd = p.FadeOutStart = Main.rand.NextFloat(3, 6);
			p.FadeOutEnd = Main.rand.NextFloat(15, 20);
			p.Scale = new Vector2(2, 0.5f);
			p.Rotation = Main.rand.NextFloat(-0.3f, 0.3f) + MathHelper.PiOver2;
			ParticleSystem.NewParticle(p, Projectile.Center);
		}
		if (Projectile.ai[0] <= 0)
			return;
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
		d.velocity += Projectile.velocity * 0.3f;
		d.color = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
		d.noGravity = true;
		d.scale = 0.6f;
	}
	public override void OnKill(int timeLeft)
	{
		SparkleParticle p = new();
		p.ColorTint = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
		p.FadeInEnd = Main.rand.NextFloat(4, 7);
		p.FadeOutStart = p.FadeInEnd;
		p.FadeOutEnd = Main.rand.NextFloat(13, 18);
		p.Scale = new Vector2(4, 2);
		p.Rotation = Projectile.oldVelocity.ToRotation() + Main.rand.NextFloat(-0.3f, 0.3f) + MathHelper.PiOver2;
		p.DrawHorizontalAxis = false;
		ParticleSystem.NewParticle(p, Projectile.Center);
		int type = ModContent.DustType<SimpleColorableGlowyDust>();
		for (int i = 0; i < 8; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, type);
			d.velocity *= 3;
			d.velocity += Projectile.velocity * 0.3f;
			d.color = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
			d.noGravity = true;
			d.scale = 1.2f;
		}
	}
	public override bool? CanHitNPC(NPC target)
	{
		return Projectile.ai[0] > 0;
	}
	public override bool ShouldUpdatePosition()
	{
		return Projectile.ai[0] > 0;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return Projectile.ai[0] > 0;
	}
}
