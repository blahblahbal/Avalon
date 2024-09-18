using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class ToxinDust : ModDust
{
    public override bool Update(Dust dust)
    {
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 244f / 255f, 125f / 255f, 31f / 255f);
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 30);
    }
}
