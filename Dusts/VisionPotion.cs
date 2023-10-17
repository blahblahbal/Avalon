using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class VisionPotion : ModDust
{
    public override bool Update(Dust dust)
    {
        return true;
    }

    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 0);
    }
}
