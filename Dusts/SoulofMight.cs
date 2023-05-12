using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofMight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.921052635f * lightFade, 0.309803933f * lightFade, 1f * lightFade);
        return true;
    }
}
