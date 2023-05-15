using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class LifeRegeneration : ModDust
{
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(256, 256, 256, 0) * 0.8f;
    }
    public override bool MidUpdate(Dust dust)
    {
        dust.scale -= 0.06f;
        dust.velocity *= 0.98f;
        return false;
    }
}
