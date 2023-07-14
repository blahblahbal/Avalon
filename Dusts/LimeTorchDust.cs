using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class LimeTorchDust : ModDust
{
    public override bool MidUpdate(Dust dust)
    {
        dust.rotation += -dust.velocity.X * 0.3f;
        dust.scale += 0.02f;
        dust.alpha += 2;

        if (!dust.noLightEmittence)
        {
            Vector3 light = new Vector3(0.714f, 1f, 0) * dust.scale * 1.5f;
            Lighting.AddLight(dust.position, light);
        }
        if (!dust.noGravity)
        {
            dust.scale -= 0.02f;
        }
        return false;
    }
}
