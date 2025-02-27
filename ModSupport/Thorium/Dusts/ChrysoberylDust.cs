using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Dusts;

public class ChrysoberylDust : ModDust
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override bool Update(Dust dust)
    {
        if (!dust.noLightEmittence)
        {
            var lightFade = (dust.scale > 1 ? 1 : dust.scale);
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), ((float)243 / 255 * lightFade), ((float)238 / 255 * lightFade), ((float)140 / 255 * lightFade));
        }
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        if (!dust.noLight)
        {
            return new Color(255, 255, 255, 100);
        }
        return default;
    }
}
