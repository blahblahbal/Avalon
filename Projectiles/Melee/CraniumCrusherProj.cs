using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Hardmode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace Avalon.Projectiles.Melee;

public class CraniumCrusherProj : MaceTemplate
{
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
}
