using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofDelight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), (189 / 255f * lightFade), (133 / 255f * lightFade), 1f * lightFade);
        return true;
    }
}
