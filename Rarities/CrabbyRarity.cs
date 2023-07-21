using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class CrabbyRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            var colors = new List<Color> { new(76, 255, 0), new(0, 127, 14) };
            int num = (int)(Main.GlobalTimeWrappedHourly / 2f % colors.Count);
            Color blue = colors[num];
            Color silver = colors[(num + 1) % colors.Count];
            return Color.Lerp(blue, silver,
                Main.GlobalTimeWrappedHourly % 2f > 1f ? 1f : Main.GlobalTimeWrappedHourly % 1f);
        }
    }

    /// <inheritdoc />
    // no 'lower' tier to go to, so return the type of this rarity.
    public override int GetPrefixedRarity(int offset, float valueMult) => Type;
}
