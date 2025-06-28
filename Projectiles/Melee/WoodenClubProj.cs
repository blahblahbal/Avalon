using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.PreHardmode;
using System;

namespace Avalon.Projectiles.Melee;

public class WoodenClubProj : MaceTemplate
{
	public override float MaxRotation => MathF.PI;
	public override float SwingRadius => 50f;
	public override float ScaleMult => WoodenClub.ScaleMult;
	public override float StartScaleTime => 0.5f;
	public override float StartScaleMult => 0.95f;
	public override float EndScaleTime => 1f / 3f;
	public override float EndScaleMult => 0.95f;
	public override Func<float, float> EasingFunc => rot => Easings.PowInOut(rot, 3f);
	public override int TrailLength => 0;
}
