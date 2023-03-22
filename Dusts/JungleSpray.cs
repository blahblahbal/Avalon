using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class JungleSpray : ModDust
{
    public override bool Update(Dust dust)
    {
        var num67 = dust.scale * 0.1f;
        if (num67 > 1f)
        {
            num67 = 1f;
        }
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num67 * 0.2f, num67, num67 * 0.5f);
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(200, 200, 200, 100);
    }
}
