using Avalon.Common;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Maces;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Maces;

public class MarrowMasherProj : MaceTemplate
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
}
