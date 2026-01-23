using Avalon.Common;
using Avalon.Common.Interfaces;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Maces;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Maces;

public class CraniumCrusherProj : MaceTemplate, ISyncedOnHitEffect
{
	public override LocalizedText DisplayName => ModContent.GetInstance<CraniumCrusher>().DisplayName;
	public override float MaxRotation => MathF.PI + MathHelper.PiOver2;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 83f;
	public override Vector2 VisualOffset => new(3, -3);
	public override float ScaleMult => CraniumCrusher.ScaleMult;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2.5f);
	public override int TrailLength => 6;
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (hit.Crit)
		{
			target.AddBuff(BuffID.BrokenArmor, TimeUtils.SecondsToTicks(12));
			hit.Knockback *= 3f;
		}
	}

	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		if (crit)
		{
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact with { Pitch = -0.3f});
			for (int i = 0; i < 10; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = target.Hitbox.ClosestPointInRect(Projectile.Center);
				s.Velocity = Vector2.Normalize(Projectile.position - Projectile.oldPos[5]).RotatedBy((-MathHelper.PiOver4 * player.direction * -Projectile.ai[0]) + Main.rand.NextFloat(-1.5f, 1.5f)) * Main.rand.NextFloat(4, 7);
				s.LocalPosition += s.Velocity * 3;
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(4f, 0.7f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(5, 8);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(15, 20);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(0.3f, 0.5f, 0.85f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
				Dust d = Dust.NewDustPerfect(s.LocalPosition, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(16,16));
				d.noGravity = true;
				d.color = s.ColorTint;
				d.color.A = 0;
			}
		}
		else
		{
			for (int i = 0; i < 5; i++)
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = target.Hitbox.ClosestPointInRect(Projectile.Center);
				s.Velocity = Vector2.Normalize(Projectile.position - Projectile.oldPos[5]).RotatedBy((-MathHelper.PiOver4 * player.direction * -Projectile.ai[0]) + Main.rand.NextFloat(-0.5f, 0.5f)) * Main.rand.NextFloat(1, 7);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(3f, 0.3f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 5);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(10, 15);
				s.AdditiveAmount = 0.75f;
				s.ColorTint = new Color(0.3f, 0.5f, 0.85f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
				Dust d = Dust.NewDustPerfect(s.LocalPosition, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(3, 3));
				d.noGravity = true;
				d.color = s.ColorTint;
				d.color.A = 0;
			}
		}
	}
}
