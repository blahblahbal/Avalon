using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace Avalon.Projectiles.Melee;

public class UrchinMaceProj : MaceTemplate
{
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
}
