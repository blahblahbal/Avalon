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
        return new Color(255, 255, 255, 200);
    }
}
