using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class YellowSolutionDust : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), (0.15f * lightFade), (0.15f * lightFade), 0f);
        return true;
    }
    //public override Color GetAlpha(Color newColor)
    //{
    //	return new Color(200, 200, 200, 0);
    //}
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
