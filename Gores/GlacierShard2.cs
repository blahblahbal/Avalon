using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Gores;

public class GlacierShard2 : ModGore
{
    public override bool Update(Gore gore)
    {
        return true;
    }
    public override Color? GetAlpha(Gore gore, Color lightColor)
    {
        return Color.White * (gore.timeLeft > 200f ? 200f / 255f : gore.timeLeft / 255f);
    }
}
