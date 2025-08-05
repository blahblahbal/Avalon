using ModLiquidLib.ModLoader;
using Terraria;

namespace Avalon.ModSupport.MLL.Liquids;

public class BloodFall : ModLiquidFall
{
	//Removes the waterfall sound that waterfalls normally make.
	//useful for when the waterfall is not ment to make waterfall sounds
	public override bool PlayWaterfallSounds()
	{
		return false;
	}

	//Usually waterfalls draw as a slight opacity
	//Lava, Honey and shimmer all draw at a slight higher opacity than water
	//We can modify how strong the alpha is.
	//0 (un-see-able), 1 (fully opaque)
	public override float? Alpha(int x, int y, float Alpha, int maxSteps, int s, Tile tileCache)
	{
		return 1f;
	}

	//We add light to our waterfall as the liquid tied to this fall also shines a bright white light
	public override void AddLight(int i, int j)
	{
		Lighting.AddLight(i, j, (float)(200f / 255f), 0f, 0f);
	}
}
