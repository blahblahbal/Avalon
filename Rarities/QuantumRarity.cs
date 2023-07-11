using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Rarities;

public class QuantumRarity : ModRarity
{
    public override Color RarityColor
    {
        get
        {
            List<Color> colors = new List<Color>
            {
                new Color(255, 0, 255, 255),
                new Color(128, 0, 255, 255),
                new Color(128, 0, 128, 255),
                new Color(255, 0, 128, 255),
                new Color(255, 0, 255, 0),
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
