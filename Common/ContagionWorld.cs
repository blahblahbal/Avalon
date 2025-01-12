using Avalon.Backgrounds;
using Avalon.Hooks;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Avalon.Common;

internal class ContagionWorld : ModSystem
{
    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
	{
		float ContagionStrength = ModContent.GetInstance<BiomeTileCounts>().ContagionTiles / 350f;
		if (CaptureManager.Instance.Active && CaptureManager.Instance.IsCapturing && CaptureInterface.Settings.BiomeChoiceIndex == AddModdedCaptureBiomes.biomeCapturesIndexs[0])
		{
			ContagionStrength = 1f;
		}
		if (ContagionStrength != 0)
		{
			ContagionStrength = Math.Min(ContagionStrength, 1f);

			int sunR = backgroundColor.R;
			int sunG = backgroundColor.G;
			int sunB = backgroundColor.B;
			byte readableSunR = 175;
			byte readableSunG = 205;
			byte readableSunB = 0;
			sunR -= (int)((byte.MaxValue - readableSunR) * ContagionStrength / 1.6f * (backgroundColor.R / 255f));
			sunG -= (int)((byte.MaxValue - readableSunG) * ContagionStrength / 1.6f * (backgroundColor.G / 255f));
			sunB -= (int)((byte.MaxValue - readableSunB) * ContagionStrength / 1.6f * (backgroundColor.B / 255f));

			sunR = Utils.Clamp(sunR, 15, 255);
			sunG = Utils.Clamp(sunG, 15, 255);
			sunB = Utils.Clamp(sunB, 15, 255);
			backgroundColor.R = (byte)sunR;
			backgroundColor.G = (byte)sunG;
			backgroundColor.B = (byte)sunB;

			int backgroundColorAverage = (int)((backgroundColor.R + backgroundColor.G + backgroundColor.B) / 3f);
			byte readableTint_R = 125;
			byte readableTint_G = 115;
			byte readableTint_B = 75;
			int tileTint_R = (byte)((byte.MaxValue - readableTint_R) * ContagionStrength * (backgroundColorAverage / 255f));
			int tileTint_G = (byte)((byte.MaxValue - readableTint_G) * ContagionStrength * (backgroundColorAverage / 255f));
			int tileTint_B = (byte)((byte.MaxValue - readableTint_B) * ContagionStrength * (backgroundColorAverage / 255f));
			tileTint_R = (int)(tileTint_R - (ContagionStrength * 7f));
			tileTint_G = (int)(tileTint_G - (ContagionStrength * 7f));
			tileTint_B = (int)(tileTint_B - (ContagionStrength * 7f));

			tileColor.R = (byte)Math.Clamp(tileColor.R <= tileTint_R ? 1 : tileColor.R - tileTint_R, ContagionStrength * 15f, 255f);
			tileColor.G = (byte)Math.Clamp(tileColor.G <= tileTint_G ? 1 : tileColor.G - tileTint_G, ContagionStrength * 15f, 255f);
			tileColor.B = (byte)Math.Clamp(tileColor.B <= tileTint_B ? 1 : tileColor.B - tileTint_B, ContagionStrength * 15f, 255f);
		}
	}
}
