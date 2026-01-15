using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Maces;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Maces;

public class UrchinMaceProj : MaceTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<UrchinMace>().DisplayName;
	public override float MaxRotation => MathF.PI + MathF.PI / 8f;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 83f;
	public override float ScaleMult => UrchinMace.ScaleMult;
	public override float EndScaleTime => 0.5f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 4;
	public override Color? TrailColor => Color.Black;
	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float progressMult = 2f - (rotationProgress * 2f);

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water_Cavern);
		d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;

		Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water);
		d2.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;

		Dust d3 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Venom);
		d3.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d3.alpha = 128;
		d3.noGravity = true;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(Main.rand.NextBool(3) ? 3 : 1));
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(Main.rand.NextBool(3) ? 3 : 1));
	}
}