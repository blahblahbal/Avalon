using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class CoolGemsparkWaterfallStyle : ModWaterfallStyle
{
	public static int R { get; private set; } = 160;
	public static int G { get; private set; } = 0;
	public static int B { get; private set; } = 255;
	public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(R / 255f, G / 255f, B / 255f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a)
	{
		r = R = CoolGemsparkBlock.R;
		g = G = CoolGemsparkBlock.G;
		b = B = CoolGemsparkBlock.B;
	}
}
