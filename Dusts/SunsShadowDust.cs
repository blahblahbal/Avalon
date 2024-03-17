using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SunsShadowDust : ModDust
{
    public override bool Update(Dust dust)
    {
        if (!dust.noLightEmittence)
        {
            var lightFade = (dust.scale > 1 ? 1 : dust.scale);
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), ((float)255 / 255 * lightFade), ((float)200 / 255 * lightFade), ((float)70 / 255 * lightFade));
        }
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        if (!dust.noLight)
        {
            return Color.White;
        }
        return default;
    }
}
