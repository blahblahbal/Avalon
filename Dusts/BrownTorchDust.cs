using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class BrownTorchDust : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.color = Color.White;
        dust.velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
        dust.velocity.X *= 0.3f;
        dust.scale *= 0.7f;
    }
    public override bool MidUpdate(Dust dust)
    {
        dust.velocity.Y -= 0.1f;
        if (!dust.noGravity)
        {
            dust.velocity.Y += 0.05f;
        }
        if (!dust.noLightEmittence)
        {
            Vector3 light = new Vector3(1.1f, 0.8f, 0.4f) * (dust.scale * 1.4f);
            Lighting.AddLight(dust.position, light);
        }
        return false;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(1.1f * 255, 0.8f * 255, 0.4f * 255, 25);
    }
}
