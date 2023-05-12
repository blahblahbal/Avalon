using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class SoulofSight : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, 0.847058833f * lightFade, 0.172549024f * lightFade);
        return true;
    }
}
