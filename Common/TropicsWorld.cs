using Avalon.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common;

internal class TropicsWorld : ModSystem
{
    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
    {
        float TropicsStrength = (float)ModContent.GetInstance<BiomeTileCounts>().TropicsTiles / 200f;
        TropicsStrength = Math.Min(TropicsStrength, 1f);

        int sunR = backgroundColor.R;
        int sunG = backgroundColor.G;
        int sunB = backgroundColor.B;
        sunR -= (int)(255f * TropicsStrength / 2 * (backgroundColor.R / 255f)); // 212, 255, 127
        sunB -= (int)(255f * TropicsStrength / 2  * (backgroundColor.B / 255f));
        sunG -= (int)(255f * TropicsStrength / 2 * (backgroundColor.G / 255f));
        sunR = Utils.Clamp(sunR, 15, 255);
        sunG = Utils.Clamp(sunG, 15, 255);
        sunB = Utils.Clamp(sunB, 15, 255);
        backgroundColor.R = (byte)sunR;
        backgroundColor.G = (byte)sunG;
        backgroundColor.B = (byte)sunB;

        int tileTint_R = (byte)(255f * TropicsStrength / 2f * (backgroundColor.R / 255f) * 1.6f); // 1.6
        int tileTint_G = (byte)(161f * TropicsStrength / 2f * (backgroundColor.G / 255f) * 1.5f);
        int tileTint_B = (byte)(99f * TropicsStrength / 2f * (backgroundColor.B / 255f) * 1.8f);

        tileColor.R = ((byte)(tileColor.R <= tileTint_R ? 0 : tileColor.R - tileTint_R));
        tileColor.G = ((byte)(tileColor.G <= tileTint_G ? 0 : tileColor.G - tileTint_G));
        tileColor.B = ((byte)(tileColor.B <= tileTint_B ? 0 : tileColor.B - tileTint_B));
    }
}
