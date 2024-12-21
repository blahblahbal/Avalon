using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class CoolGemsparkWaterfallStyle : ModWaterfallStyle
{
	public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(CoolGemsparkBlock.R / 255f, CoolGemsparkBlock.G / 255f, CoolGemsparkBlock.B / 255f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a)
	{
		r = CoolGemsparkBlock.R * a;
		g = CoolGemsparkBlock.G * a;
		b = CoolGemsparkBlock.B * a;
	}
}
