using ModLiquidLib.ModLoader;
using Terraria;

namespace Avalon.ModSupport.MLL.Liquids;

public class AcidFall : ModLiquidFall
{
	public override bool PlayWaterfallSounds()
	{
		return false;
	}

	public override float? Alpha(int x, int y, float Alpha, int maxSteps, int s, Tile tileCache)
	{
		float num = 1f;
		if (s > maxSteps - 10)
		{
			num *= (float)(maxSteps - s) / 10f;
		}
		return num;
	}

	public override void AddLight(int i, int j)
	{
		Lighting.AddLight(i, j, 0f, 215f / 255f, 0f);
	}
}
