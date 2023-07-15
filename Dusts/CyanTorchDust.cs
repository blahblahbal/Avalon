using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class CyanTorchDust : ModDust
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
            Vector3 light = new Vector3(0, 1f, 1f) * dust.scale * 1.5f;
            Lighting.AddLight(dust.position, light);
        }
        return false;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(0, 1f * 255, 1f * 255, 25);
    }
}
