using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofHumidity : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, 0.8039216f * lightFade, 0.07450981f * lightFade);
        return true;
    }
}
