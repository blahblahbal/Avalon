using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class FreezethrowerDust : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), ((float)67 / 255 * lightFade), ((float)70 / 255 * lightFade), 1 * lightFade);
        return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(135, 140, 255, 100);
    }
}
