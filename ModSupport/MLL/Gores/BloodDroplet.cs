using Avalon.Gores;

namespace Avalon.ModSupport.MLL.Gores;
public class BloodDroplet : LiquidDropletGoreBase
{
	public override float? AccumulatingFrameTimeMult => 1.25f;
	public override float? DissipatingFrameTimeMult => 1.25f;
	public override float? FallingFrameTimeMult => 1.25f;
	public override float? SplashFrameTimeMult => 1.5f;
	public override float? FallingAccel => 0.19f;
}