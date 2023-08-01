using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class CrispyRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            List<Color> colors = new List<Color>
            {
                new Color(178, 0, 255),
                new Color(255, 0, 0)
            };
            int num = (int)(Main.GlobalTimeWrappedHourly / 2f % colors.Count);
            Color orange = colors[num];
            Color silver = colors[(num + 1) % colors.Count];
            return Color.Lerp(orange, silver, (Main.GlobalTimeWrappedHourly % 2f > 1f) ? 1f : (Main.GlobalTimeWrappedHourly % 1f));
        }
    }

    public override int GetPrefixedRarity(int offset, float valueMult)
    {
        return Type; // no 'lower' tier to go to, so return the type of this rarity.
    }
}
