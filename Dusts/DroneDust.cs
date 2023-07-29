using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class DroneDust : ModDust
{
    public override bool Update(Dust dust)
    {
        dust.scale *= 0.98f;
        dust.alpha -= 4;
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(200, 200, 200, 100);
    }
}
