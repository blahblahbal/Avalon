using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Waters;

public class ZirconWaterfallStyle : ModWaterfallStyle
{
    public override void AddLight(int i, int j) =>
             Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), new Vector3(1.1f * 0.8f, 0.8f * 0.8f, 0.4f * 0.8f) * 0.25f);
    public override void ColorMultiplier(ref float r, ref float g, ref float b, float a) //Does this do anything? idk tbh, don't think so lmao
    {
        r = 280f * 0.8f;
        g = 204f * 0.8f;
        b = 102f * 0.8f;
    }
}
