using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class WarmGemsparkWaterfallStyle : ModWaterfallStyle
{
	public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(1f, WarmGemsparkBlock.G / 255f, 0f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a)
	{
		r = 255f * a;
		g = WarmGemsparkBlock.G * a;
		b = 0;
	}
}
