using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class IridiumLongbowEnergyArrow : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
		AIType = ProjectileID.WoodenArrowFriendly;
		Projectile.extraUpdates+= 3;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 0f);
	}
	public override void AI()
	{
		Dust d = Dust.NewDustDirect(Projectile.position,Projectile.width,Projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
		d.velocity += Projectile.velocity * 0.3f;
		d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
		d.noGravity = true;
		d.scale = 0.6f;
	}
	public override void OnKill(int timeLeft)
	{
		SparkleParticle p = new();
		p.ColorTint = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
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
			d.color = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
			d.noGravity = true;
			d.scale = 1.2f;
		}
	}
	public override bool? CanHitNPC(NPC target)
	{
		return target.whoAmI != Projectile.ai[2];
	}
}
