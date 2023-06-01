using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class ContagionPowder : ModDust
{
    public override bool Update(Dust dust)
    {

        dust.scale += 0.045f;
        var num67 = dust.scale * 0.4f;
        if (num67 > 1f)
        {
            num67 = 1f;
        }
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num67 * 0.55f, num67 * 0.9f, num67 * 0.25f);
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 50);
    }
}
