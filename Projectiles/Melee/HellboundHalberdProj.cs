using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Hardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace Avalon.Projectiles.Melee;

public class HellboundHalberdProj : MaceTemplate
{
	public override float MaxRotation => MathF.PI + MathF.PI / 4f;
	public override float? StartRotationLimit => MathHelper.PiOver2;
	public override float SwingRadius => 104f;
	public override float ScaleMult => HellboundHalberd.ScaleMult;
	public override float EndScaleTime => 0.2f;
	public override Func<float, float> EasingFunc => rot => Easings.PowOut(rot, 2f);
	public override int TrailLength => 6;
	public override Func<(SpriteEffects, float, Vector2), (SpriteEffects, float, Vector2)> SpriteEffectsFunc => FlipSprite;
	public (SpriteEffects, float, Vector2) FlipSprite((SpriteEffects spriteEffects, float rotation, Vector2 offset) t)
	{
		if ((SwingDirection == 1 && Owner.gravDir != -1) || (Owner.gravDir == -1 && SwingDirection == -1))
		{
			if (Owner.direction == 1)
			{
				return t;
			}
			if (Owner.direction == -1)
			{
				return (SpriteEffects.FlipHorizontally, MathHelper.PiOver2, new Vector2(-t.offset.X, t.offset.Y));
			}
		}
		else if ((SwingDirection == -1 && Owner.gravDir != -1) || (Owner.gravDir == -1 && SwingDirection == 1))
		{
			if (Owner.direction == 1)
			{
				return (SpriteEffects.FlipHorizontally, MathHelper.PiOver2, new Vector2(-t.offset.X, t.offset.Y));
			}
			if (Owner.direction == -1)
			{
				return t;
			}
		}
		return t;
	}
	public override void EmitDust(Vector2 handPosition, float swingRadius, float rotationProgress, float easedRotationProgress)
	{
		Vector2 offsetFromHand = Projectile.Center - handPosition;
		float dirMod = SwingDirection * Owner.gravDir;
		float progressMult = 2f - (rotationProgress * 2f);

		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
		d.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d.noGravity = true;
		d.fadeIn = Main.rand.NextFloat(0, 1.5f);

		Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
		d2.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
		d2.noGravity = true;
		d2.fadeIn = Main.rand.NextFloat(0, 1.5f);

		if (Main.rand.NextBool(4))
		{
			Dust dSmall = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
			dSmall.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
			dSmall.fadeIn = Main.rand.NextFloat(0, 0.7f);
			dSmall.scale *= 0.5f;
			Dust d2Small = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare);
			d2Small.velocity = Vector2.Normalize(offsetFromHand * dirMod).RotatedBy(MathHelper.PiOver2 * Owner.direction) * 3 * progressMult;
			d2Small.fadeIn = Main.rand.NextFloat(0, 0.7f);
			d2Small.scale *= 0.5f;
		}
	}
}
