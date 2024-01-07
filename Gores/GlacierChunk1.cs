using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Gores;

public class GlacierChunk1 : ModGore
{
    public override bool Update(Gore gore)
    {
        var lightFade = (gore.timeLeft > 255f ? 1f : gore.timeLeft / 255f);
        Lighting.AddLight((int)(gore.position.X / 16f), (int)(gore.position.Y / 16f), 0f, (0.4f * lightFade), (0.5f * lightFade));
        return true;
    }
    public override Color? GetAlpha(Gore gore, Color lightColor)
    {
        return new Color(255, 255, 255, 200) * (gore.timeLeft > 255f ? 1f : gore.timeLeft / 255f);
    }
}
