using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class FractureRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            List<Color> colors = new List<Color>
            {
                new Color(238, 51, 53),
                new Color(239, 99, 0),
                new Color(255, 198, 0),
                new Color(0, 237, 14),
                new Color(33, 184, 115),
                new Color(27, 247, 229),
                new Color(23, 147, 234),
                new Color(165, 0, 236),
                new Color(137, 126, 187),
                new Color(160, 124, 80)
            };
            int numColors = colors.Count;
            float fade = Main.GameUpdateCount % 60 / 60f;
            int index = (int)(Main.GameUpdateCount / 60 % numColors);
            int nextIndex = (index + 1) % numColors;
            return Color.Lerp(colors[index], colors[nextIndex], fade);
        }
    }

    public override int GetPrefixedRarity(int offset, float valueMult)
    {
        return Type; // no 'lower' tier to go to, so return the type of this rarity.
    }
}
