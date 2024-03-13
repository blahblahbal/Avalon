using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Waters;

public class ChrysoberylWaterfallStyle : ModWaterfallStyle
{
    public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(0.745f * 0.8f, 0.925f * 0.8f, 0.1f * 0.8f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a)
    {
        r = 180f * 0.8f;
        g = 220f * 0.8f;
        b = 30f * 0.8f;
    }
}
