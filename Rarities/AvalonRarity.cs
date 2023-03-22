using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class AvalonRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            var colors = new List<Color> { new(71, 142, 147), new(255, 242, 0) };
            int num = (int)(Main.GlobalTimeWrappedHourly / 2f % colors.Count);
            Color teal = colors[num];
            Color yellow = colors[(num + 1) % colors.Count];
            return Color.Lerp(teal, yellow,
                Main.GlobalTimeWrappedHourly % 2f > 1f ? 1f : Main.GlobalTimeWrappedHourly % 1f);
        }
    }

    /// <inheritdoc />
    // no 'lower' tier to go to, so return the type of this rarity.
    public override int GetPrefixedRarity(int offset, float valueMult) => Type;
}
