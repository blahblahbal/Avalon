using Avalon.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common;

internal class TropicsWorld : ModSystem
{
    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
    {
        float TropicsStrength = (float)ModContent.GetInstance<BiomeTileCounts>().TropicsTiles / 1000f;
        TropicsStrength = Math.Min(TropicsStrength, 1f);

        int sunR = backgroundColor.R;
        int sunG = backgroundColor.G;
        int sunB = backgroundColor.B;
        sunR -= (int)(5f * TropicsStrength / 1.7f * (backgroundColor.R / 255f)); // 212, 255, 127
        sunG -= (int)(180f * TropicsStrength / 1.7f * (backgroundColor.G / 255f));
        sunB -= (int)(240f * TropicsStrength / 1.7f * (backgroundColor.B / 255f));

        sunR = Utils.Clamp(sunR, 15, 255);
        sunG = Utils.Clamp(sunG, 15, 255);
        sunB = Utils.Clamp(sunB, 15, 255);
        backgroundColor.R = (byte)sunR;
        backgroundColor.G = (byte)sunG;
        backgroundColor.B = (byte)sunB;

        int backgroundColorAverage = (int)((backgroundColor.R + backgroundColor.G + backgroundColor.B) / 3f);
        int tileTint_R = (byte)(80f * TropicsStrength * (backgroundColorAverage / 255f));
        int tileTint_G = (byte)(130f * TropicsStrength * (backgroundColorAverage / 255f));
        int tileTint_B = (byte)(100f * TropicsStrength * (backgroundColorAverage / 255f));
        tileTint_R = (int)(tileTint_R - (TropicsStrength * 4f));
        tileTint_G = (int)(tileTint_G - (TropicsStrength * 4f));
        tileTint_B = (int)(tileTint_B - (TropicsStrength * 4f));

        tileColor.R = (byte)Math.Clamp(tileColor.R <= tileTint_R ? 1 : tileColor.R - tileTint_R, TropicsStrength * 15f, 255f);
        tileColor.G = (byte)Math.Clamp(tileColor.G <= tileTint_G ? 1 : tileColor.G - tileTint_G, TropicsStrength * 15f, 255f);
        tileColor.B = (byte)Math.Clamp(tileColor.B <= tileTint_B ? 1 : tileColor.B - tileTint_B, TropicsStrength * 15f, 255f);
    }
}
