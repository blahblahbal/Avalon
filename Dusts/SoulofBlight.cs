using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofBlight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0.105882354f * lightFade, 0.105882354f * lightFade, 0.105882354f * lightFade);
        return true;
    }
}
