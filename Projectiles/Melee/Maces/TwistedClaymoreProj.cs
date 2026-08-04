using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Interfaces;
using Avalon.Common.Templates;
using Avalon.Core;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Maces;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Maces;

public class TwistedClaymoreProj : MaceTemplate, ISyncedOnHitEffect
{
	public override LocalizedText DisplayName => ModContent.GetInstance<TwistedClaymore>().DisplayName;
	public override float MaxRotation => 4.5f;
	public override float SwingRadius => 120;
	public override float StartScaleTime => 0.5f;
	public override float StartScaleMult => 1f;
	public override float EndScaleTime => 1f / 3f;
	public override float EndScaleMult => 1f;

	public override Color? TrailColor => new Color(1f, 0, 0, 0f) * (1f - EasingFunc.Invoke(1f - Projectile.timeLeft / InitialTimeLeft));
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 3f);
	public override int TrailLength => 8;
	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float speedMultiplier = Math.Clamp(Math.Abs(Projectile.oldRot[0] - Projectile.rotation), 0, 1f);
		if (speedMultiplier > 0.1f)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust d = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, handPosition, Main.rand.NextFloat(-0.3f, 0.7f)) + Main.rand.NextVector2Circular(15, 15), DustID.Corruption);
				d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction).RotatedByRandom(0.2f) * Main.rand.NextFloat(2,5) * speedMultiplier;
				d.alpha = (int)(128 * EasingFunc.Invoke(1f - Projectile.timeLeft / InitialTimeLeft)) + 128;

				Dust d2 = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center, handPosition, Main.rand.NextFloat(-0.3f, 0.5f)) + Main.rand.NextVector2Circular(15,15), ModContent.DustType<SimpleColorableGlowyDust>());
				d2.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction).RotatedByRandom(0.3f) * Main.rand.NextFloat(3,18);
				d2.noGravity = true;
				d2.fadeIn = Main.rand.NextFloat(1.5f);
				d2.color = new Color(1, 0, Main.rand.NextFloat(), 0.25f) * EasingFunc.Invoke(1f - Projectile.timeLeft / InitialTimeLeft);
			}

			var dot = AssetReferences.Assets.Textures.TriangleThing.Asset;
			int time2 = Main.rand.Next(5, 15);
			var p = VanillaParticles.RequestFadingParticle();
			p.SetBasicInfo(dot, null, Vector2.Zero, Vector2.Lerp(Projectile.Center, handPosition, Main.rand.NextFloat(-0.3f, 0.7f)) + Main.rand.NextVector2Circular(15, 15));
			p.ColorTint = new Color(Main.rand.NextFloat(), 0, 0);
			p.SetTypeInfo(time2);
			p.FadeInNormalizedTime = 0.1f;
			p.FadeOutNormalizedTime = 0.5f;
			p.Scale = Vector2.One * Main.rand.NextFloat(0.5f);
			p.Rotation = Main.rand.NextFloatDirection();
			p.Velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction).RotatedByRandom(0.2f) * Main.rand.NextFloat(2, 15) * speedMultiplier;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<TwistedClaymoreDebuff>(), 60 * 5);
	}
	public bool SyncedOnHitNPC(Player player, NPC target, Rectangle attackHitbox, int damage, float knockback, bool crit, int hitDirection, Projectile? projectile)
	{
		Vector2 closestPoint = target.Hitbox.ClosestPointInRect(attackHitbox.Center.ToVector2());
		float VelocityDirection = (Projectile.rotation + MathHelper.PiOver4) * Owner.direction * SwingDirection * Owner.gravDir;
		var sparkle = AssetReferences.Assets.Textures.SparklyDarkOutside.Asset;
		sparkle.Wait();
		var dot = AssetReferences.Assets.Textures.TriangleThing.Asset;
		dot.Wait();

		for (int i = 0; i < 5; i++)
		{
			int time2 = Main.rand.Next(10, 40);
			var p = VanillaParticles.RequestFadingParticle();
			p.SetBasicInfo(dot, null, Vector2.Zero, closestPoint + Main.rand.NextVector2Circular(8, 8));
			p.ColorTint = Color.Black;
			p.SetTypeInfo(time2);
			p.FadeInNormalizedTime = 0.1f;
			p.FadeOutNormalizedTime = 0.5f;
			p.Scale = Vector2.One.RotatedByRandom(0.6f) * Main.rand.NextFloat(0.7f, 1);
			p.Rotation = Main.rand.NextFloatDirection();
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}
		for (int i = 0; i < 10; i++)
		{
			int time = Main.rand.Next(10, 20);
			var p = VanillaParticles.RequestFadingParticle();
			p.SetBasicInfo(sparkle, null, Vector2.One.RotatedBy(VelocityDirection + Main.rand.NextFloat(-0.4f, 0.4f)) * Main.rand.NextFloat(1,10), closestPoint);
			p.ColorTint = new Color(1, 0, Main.rand.NextFloat(), 0.25f);
			p.SetTypeInfo(time);
			p.FadeInNormalizedTime = 0.1f;
			p.FadeOutNormalizedTime = 0.5f;
			p.Scale = new Vector2(1.5f, 2);
			p.ScaleVelocity = -p.Scale / time;
			p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;
			p.AccelerationPerFrame = Main.rand.NextVector2Circular(0.1f,0.1f);
			Main.ParticleSystem_World_OverPlayers.Add(p);

		}
		return true;
	}
}
