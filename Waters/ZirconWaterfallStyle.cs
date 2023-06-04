using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class ZirconWaterfallStyle : ModWaterfallStyle
{
    public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(1.1f * 0.8f, 0.8f * 0.8f, 0.4f * 0.8f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a)
    {
        r = 230f * 0.8f;
        g = 174f * 0.8f;
        b = 92f * 0.8f;
    }
}
