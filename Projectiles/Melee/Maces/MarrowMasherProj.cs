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

public class MarrowMasherProj : MaceTemplate, ISyncedOnHitEffect
{
	public override LocalizedText DisplayName => ModContent.GetInstance<MarrowMasher>().DisplayName;
	public override float MaxRotation => MathF.PI + MathHelper.PiOver4;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 58f;
	public override float ScaleMult => MarrowMasher.ScaleMult;
	public override float EndScaleTime => 0.167f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 4;
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (hit.Crit)
		{
			hit.Knockback *= 1.5f;
		}
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		//Main.NewText("Name: " + player.name + " Target:" + target.TypeName + $" ({target.whoAmI})");
		//Main.NewText("Target pos: " + target.Center.ToString() + "Player Pos: " + player.Center.ToString());
		//Main.NewText($"Crit: {crit} dir: {hitDirection}");
		Vector2 closestPoint = target.Hitbox.ClosestPointInRect(Projectile.Center);
		float VelocityDirection = (Projectile.rotation + MathHelper.PiOver4) * Owner.direction * SwingDirection * Owner.gravDir;
		if (crit)
		{
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundMiss);
			for (int i = 0; i < 5; i++)
			{
				SparkleParticle s = new();
				s.Velocity = Vector2.One.RotatedBy(VelocityDirection + Main.rand.NextFloat(-2f, 2f)) * Main.rand.NextFloat(3, 6);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(4f, 0.7f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(5, 8);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(15, 20);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(0.85f, 0.5f, 0.5f);
				ParticleSystem.NewParticle(s, closestPoint + s.Velocity);
				Dust d = Dust.NewDustPerfect(closestPoint, ModContent.DustType<SimpleColorableGlowyDust>(), Main.rand.NextVector2Circular(3,3));
				d.noGravity = true;
				d.color = s.ColorTint;
				d.color.A = 0;
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				SparkleParticle s = new();
				s.Velocity = Vector2.One.RotatedBy(VelocityDirection + Main.rand.NextFloat(-1.5f, 1.5f)) * Main.rand.NextFloat(3, 5);
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(3f, 0.3f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 5);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(10, 15);
				s.AdditiveAmount = 0.25f;
				s.ColorTint = new Color(0.85f, 0.5f, 0.5f);
				ParticleSystem.NewParticle(s, closestPoint);
			}
		}
	}
}
