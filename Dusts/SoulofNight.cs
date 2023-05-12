using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofNight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.368627459f * lightFade, 0.101960786f * lightFade, 0.8666667f * lightFade);
        return true;
    }
}
