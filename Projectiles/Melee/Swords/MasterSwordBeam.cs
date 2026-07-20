using Avalon.Core;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.BackupIO;

namespace Avalon.Projectiles.Melee.Swords;

public class MasterSwordBeam : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Type] = 5;
		ProjectileID.Sets.TrailingMode[Type] = 2;
	}
	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.aiStyle = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 1;
		Projectile.alpha = 255;
		Projectile.friendly = true;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * Projectile.Opacity;
	}
	public override void AI()
	{
		if(Projectile.Opacity == 0)
		{
			SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/MasterSword") { pitchVariance = 0.12f}, Projectile.position);
		}
		Projectile.Opacity += 0.1f;

		Projectile.rotation += Projectile.direction * 0.4f;
		if (Projectile.direction == 1)
		{
			Projectile.spriteDirection = 1;
		}
		else
		{
			Projectile.spriteDirection = -1;
		}
		Lighting.AddLight(Projectile.Center, (63 / 255f) / 3f, (214 / 255f) / 3f, 1 / 3f);
	}
	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		var ring = VanillaParticles.RequestFadingParticle();
		var t = AssetReferences.Assets.Textures.InverseGlowRing.Asset;
		t.Wait();
		ring.SetBasicInfo(t, null, Vector2.Zero, Projectile.Center);
		int time = 25;
		ring.SetTypeInfo(time);
		ring.Scale = Vector2.One * 0.15f;
		ring.ScaleVelocity = Vector2.One.RotatedByRandom(0.2f) * 0.075f;
		ring.ScaleAcceleration = ring.ScaleVelocity / -time;
		ring.FadeInNormalizedTime = 0.1f;
		ring.FadeOutNormalizedTime = 0.1f;
		ring.ColorTint = new Color(31,107,255,64);
		ring.Rotation = Main.rand.NextFloatDirection();
		Main.ParticleSystem_World_OverPlayers.Add(ring);

		float iterations = 7;
		float spin = Main.rand.NextFloatDirection();
		for (int i = 0; i < iterations; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();
			p.TimeToLive = Main.rand.Next(25,50);
			p.FadeInEnd = p.FadeOutStart = 10;
			p.Velocity = Vector2.UnitY.RotatedBy((i / iterations * MathHelper.TwoPi) + spin + Main.rand.NextFloat(MathHelper.Pi / -iterations, MathHelper.Pi / iterations)) * Main.rand.NextFloat(5, 6);
			p.LocalPosition = Projectile.Center + p.Velocity * 3;
			p.Scale = new Vector2(3,Main.rand.NextFloat(1,2.5f));
			p.DrawHorizontalAxis = false;
			p.AccelerationPerFrame = -p.Velocity / p.TimeToLive;
			float blueness = Main.rand.NextFloat(0.5f, 1f);
			p.ColorTint = new Color(63 * blueness / 255, 214 * blueness / 255, 1f, 0);
			p.Rotation = p.Velocity.ToRotation() - MathHelper.PiOver2;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}

		iterations = 25;
		spin = Main.rand.NextFloatDirection();
		for (int i = 0; i < iterations; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowMk2, Vector2.UnitY.RotatedBy((i / iterations * MathHelper.TwoPi) + spin + Main.rand.NextFloat(MathHelper.Pi / -iterations, MathHelper.Pi / iterations)) * Main.rand.NextFloat(2, 12));
			float blueness = Main.rand.NextFloat(0.5f, 1f);
			d.color = new Color(63 * blueness / 255, 214 * blueness / 255, 1f, 0);
			d.color = Color.Lerp(d.color, Color.White with { A = 0 }, Main.rand.NextFloat(0.5f));
			d.noGravity = true;
			d.scale *= Main.rand.NextFloat(0.5f,1.25f);
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		var effect = Projectile.spriteDirection == 1? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		var t = TextureAssets.Projectile[Type].Value;
		Color c = (Color)GetAlpha(lightColor);
		for (int i = Projectile.oldPos.Length - 1; i >= 0; i--)
		{
			float percent = i / (float)Projectile.oldPos.Length;
			Main.EntitySpriteDraw(t, Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2, null, Color.Lerp(c, c.MultiplyRGB(Color.Blue), percent) with { A = 64 } * 0.4f * (1f - percent), Projectile.oldRot[i], t.Size() / 2, Projectile.scale - percent * 0.2f, effect);
		}
		Main.EntitySpriteDraw(t, Projectile.Center - Main.screenPosition, null, c, Projectile.rotation, t.Size() / 2, Projectile.scale, effect);
		return false;
	}
}