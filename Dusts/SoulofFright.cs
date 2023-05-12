using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofFright : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 1f * lightFade, 0.309803933f * lightFade, 0.003921569f * lightFade);
        return true;
    }
}
