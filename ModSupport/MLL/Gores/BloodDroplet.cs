using Avalon.Gores;
using Microsoft.Xna.Framework;

namespace Avalon.ModSupport.MLL.Gores;
public class BloodDroplet : LiquidDropletGoreBase
{
	public override float? AccumulatingFrameTimeMult => 1.25f;
	public override float? DissipatingFrameTimeMult => 1.25f;
	public override float? FallingFrameTimeMult => 1.25f;
	public override float? SplashFrameTimeMult => 1.5f;
	public override float? FallingAccel => 1.9f;
	public override Vector3? LightColor => new(0f, 1f, 0f);
}