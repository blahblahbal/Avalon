using Avalon.Gores;
using Microsoft.Xna.Framework;
using Terraria;

namespace Avalon.ModSupport.MLL.Gores;
public class AcidDroplet : LiquidDropletGoreBase
{
	public override float? AccumulatingFrameTimeMult => 1.25f;
	public override float? DissipatingFrameTimeMult => 1.25f;
	public override float? FallingFrameTimeMult => 1.25f;
	public override float? SplashFrameTimeMult => 1.5f;
	public override float? FallingAccel => 0.19f;
	public override Vector3? LightColor => new(0f, 1f, 0f);
	public override bool? HasSound => false;
	public override Color? GetAlpha(Gore gore, Color lightColor)
	{
		return new Color(255, 255, 255, 200);
	}
}
