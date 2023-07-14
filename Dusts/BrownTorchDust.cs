using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class BrownTorchDust : ModDust
{
    public override bool MidUpdate(Dust dust)
    {
        dust.rotation += -dust.velocity.X * 0.3f;
        dust.scale += 0.02f;
        dust.alpha += 2;

        if (!dust.noLightEmittence)
        {
            Vector3 light = new Vector3(1.1f, 0.8f, 0.4f) * dust.scale * 1.5f;
            Lighting.AddLight(dust.position, light);
        }
        if (!dust.noGravity)
        {
            dust.scale -= 0.02f;
        }
        return false;
    }
}
