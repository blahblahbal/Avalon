using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofFlight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.003921569f * lightFade, 0.4862745f * lightFade, 0.6313726f * lightFade);
        return true;
    }
}
