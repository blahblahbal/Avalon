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
        float TropicsStrength = (float)ModContent.GetInstance<BiomeTileCounts>().SavannaTiles / 1000f;
		if (TropicsStrength != 0)
		{
			TropicsStrength = Math.Min(TropicsStrength, 1f);

			int sunR = backgroundColor.R;
			int sunG = backgroundColor.G;
			int sunB = backgroundColor.B;
			//byte readableSunR = 250;
			//byte readableSunG = 75;
			//byte readableSunB = 15;
			byte readableSunR = 255;
			byte readableSunG = 110;
			byte readableSunB = 80;
			sunR -= (int)((byte.MaxValue - readableSunR) * TropicsStrength / 1.7f * (backgroundColor.R / 255f)); // 212, 255, 127
			sunG -= (int)((byte.MaxValue - readableSunG) * TropicsStrength / 1.7f * (backgroundColor.G / 255f));
			sunB -= (int)((byte.MaxValue - readableSunB) * TropicsStrength / 1.7f * (backgroundColor.B / 255f));

			sunR = Utils.Clamp(sunR, 15, 255);
			sunG = Utils.Clamp(sunG, 15, 255);
			sunB = Utils.Clamp(sunB, 15, 255);
			backgroundColor.R = (byte)sunR;
			backgroundColor.G = (byte)sunG;
			backgroundColor.B = (byte)sunB;

			int backgroundColorAverage = (int)((backgroundColor.R + backgroundColor.G + backgroundColor.B) / 3f);
			//byte readableTint_R = 175;
			//byte readableTint_G = 125;
			//byte readableTint_B = 155;
			byte readableTint_R = 245;
			byte readableTint_G = 180;
			byte readableTint_B = 215;
			int tileTint_R = (byte)((byte.MaxValue - readableTint_R) * TropicsStrength * (backgroundColorAverage / 255f));
			int tileTint_G = (byte)((byte.MaxValue - readableTint_G) * TropicsStrength * (backgroundColorAverage / 255f));
			int tileTint_B = (byte)((byte.MaxValue - readableTint_B) * TropicsStrength * (backgroundColorAverage / 255f));
			tileTint_R = (int)(tileTint_R - (TropicsStrength * 4f));
			tileTint_G = (int)(tileTint_G - (TropicsStrength * 4f));
			tileTint_B = (int)(tileTint_B - (TropicsStrength * 4f));

			tileColor.R = (byte)Math.Clamp(tileColor.R <= tileTint_R ? 1 : tileColor.R - tileTint_R, TropicsStrength * 15f, 255f);
			tileColor.G = (byte)Math.Clamp(tileColor.G <= tileTint_G ? 1 : tileColor.G - tileTint_G, TropicsStrength * 15f, 255f);
			tileColor.B = (byte)Math.Clamp(tileColor.B <= tileTint_B ? 1 : tileColor.B - tileTint_B, TropicsStrength * 15f, 255f);
		}
	}
}
