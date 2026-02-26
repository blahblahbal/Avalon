using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class OsmiumLongbowHeld : LongbowTemplate
{
	public override void SetDefaults()
	{
		base.SetDefaults();
		DrawOffsetX = -16;
		DrawOriginOffsetY = -25;
	}
	public override bool ArrowEffect(Projectile projectile, float Power, byte variant = 0)
	{
		if (Power == 1)
		{
			SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, projectile.position);
			projectile.GetGlobalProjectile<OsmiumLongbowGlobalProjectile>().Active = true;
			return true;
		}
		return false;
	}
	public override void PostDraw(Color lightColor)
	{
		if (Main.player[Projectile.owner].channel)
		{
			Color arrowColor = Color.Lerp(Color.Blue, Color.Cyan, Main.masterColor) with { A = 0 };
			DrawArrow(arrowColor * Power, Vector2.Zero, true);
		}
	}
}
public class OsmiumLongbowGlobalProjectile : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	public bool Active;
	private int _lastHit = -1;
	public override void AI(Projectile projectile)
	{
		if (Active)
		{
			int type = ModContent.DustType<SimpleColorableGlowyDustFlat>();
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, type);
			d.velocity *= 0.3f;
			d.velocity += projectile.velocity * 0.3f;
			d.color = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
			d.noGravity = !Main.rand.NextBool(8);
			d.scale = d.noGravity ? 1.3f : 0.5f;
			d.noLight = true;

			Dust d2 = Dust.NewDustPerfect(d.position, type);
			d2.frame = d.frame;
			d2.rotation = d.rotation;
			d2.velocity = d.velocity;
			d2.color = new Color(1f, 1f, 1f, 0f);
			d2.noGravity = d.noGravity;
			d2.scale = d.scale * Main.rand.NextFloat(0.8f);
			d2.noLight = true;
		}
	}
	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Active)
		{
			_lastHit = target.whoAmI;
			projectile.Kill();
		}
	}
	public override void OnKill(Projectile projectile, int timeLeft)
	{
		if (!Active)
			return;

		SoundEngine.PlaySound(SoundID.Item14, projectile.position);
		int type = ModContent.DustType<SimpleColorableGlowyDustFlat>();
		for (int i = 0; i < 15; i++)
		{
			Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, type);
			d.velocity *= 0.3f;
			d.velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(5, 10);
			d.color = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
			d.noGravity = true;
			d.scale = Main.rand.NextFloat(1f,2f);
			d.noLight = true;

			Dust d2 = Dust.NewDustPerfect(d.position, type);
			d2.frame = d.frame;
			d2.rotation = d.rotation;
			d2.velocity = d.velocity;
			d2.color = new Color(1f, 1f, 1f, 0f);
			d2.noGravity = true;
			d2.scale = d.scale * Main.rand.NextFloat(0.5f, 0.9f);
			d2.noLight = true;
		}
		for(int i = 0; i < 5; i++)
		{
			Gore g = Gore.NewGoreDirect(projectile.GetSource_FromThis(), projectile.Center, Main.rand.NextVector2Circular(1, 1), Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1), Main.rand.NextFloat(0.25f,0.75f));
			g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);

			SparkleParticle p = new();
			p.ColorTint = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
			p.FadeInEnd = Main.rand.NextFloat(2, 5);
			p.FadeOutStart = p.FadeInEnd;
			p.FadeOutEnd = Main.rand.NextFloat(15, 17);
			p.Scale = new Vector2(2, 1);
			p.Velocity = Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(7, 9);
			p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;
			p.DrawHorizontalAxis = false;
			ParticleSystem.NewParticle(p, projectile.Center);
		}
		int proj = ModContent.ProjectileType<OsmiumShrapnel>();
		int shrapnelCount = 7 + (projectile.penetrate == -1? 10 : 4 * projectile.penetrate / 2);
		for(int i = 0; i < shrapnelCount; i++)
		{
			Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(0,Main.rand.NextFloat(2,5)).RotatedBy((i * MathHelper.TwoPi / shrapnelCount) + Main.rand.NextFloat(-0.2f,0.2f)), proj, projectile.damage / 4, projectile.knockBack / 2, projectile.owner, _lastHit);
		}
	}
}
