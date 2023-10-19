using Terraria;
using Terraria.ModLoader;

namespace Avalon.Gores;

public class LargeBubble : ModGore
{
    public override bool Update(Gore gore)
    {
        gore.velocity.Y *= 0f;
        gore.velocity.X *= 0.5f;
        gore.timeLeft--;
        if (gore.timeLeft <= 0)
        {
            gore.alpha = 255;
            gore.active = false;
        }
        return true;
    }
}
