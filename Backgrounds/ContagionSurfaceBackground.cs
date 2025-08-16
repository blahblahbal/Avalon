using Avalon.Common;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class ContagionSurfaceBackground : ModSurfaceBackgroundStyle
{
	public override void ModifyFarFades(float[] fades, float transitionSpeed)
	{
		for (int i = 0; i < fades.Length; i++)
		{
			if (i == Slot)
			{
				fades[i] += transitionSpeed;
				if (fades[i] > 1f)
				{
					fades[i] = 1f;
				}
			}
			else
			{
				fades[i] -= transitionSpeed;
				if (fades[i] < 0f)
				{
					fades[i] = 0f;
				}
			}
		}
	}

	public override int ChooseFarTexture()
	{
		return 24;
	}

	public override int ChooseMiddleTexture()
	{
		return 25;
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		return BackgroundTextureLoader.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground1");
	}

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		float bgScale = Main.instance.GetBackGroundScale();
		float screenOff = Main.instance.GetScreenOff();
		double bgParallax = Main.instance.GetBackgroundParrallax();
		int bgTopY = Main.instance.GetBackgroundTopY();
		float scAdj = Main.instance.GetScAdj();
		int bgWidthScaled = Main.instance.GetBackGroundWidthScaled();
		int bgStartX = Main.instance.GetBackgroundStartX();
		int bgLoops = Main.instance.GetBackgroundLoops();
		Color ColorOfSurfaceBackgroundsModified = Main.instance.GetColorOfSurfaceBackgroundsModified();

		string? closeBGPath;
		string? closeMidBGPath;
		string? closeFarBGPath;
		switch (AvalonWorld.contagionBG)
		{
			case 3:
			{
				closeBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground7";
				closeMidBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground5";
				closeFarBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground6";
				break;
			}
			case 2:
			{
				closeBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground8";
				closeMidBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground2";
				closeFarBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground3";
				break;
			}
			case 1:
			{
				closeBGPath = null;
				closeMidBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground5";
				closeFarBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground6";
				break;
			}
			default:
			{
				closeBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground1";
				closeMidBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground2";
				closeFarBGPath = $"{Mod.Name}/Backgrounds/ContagionSurfaceBackground3";
				break;
			}
		}

		bool renderBG = false;
		if ((!Main.remixWorld || (Main.gameMenu && !WorldGen.remixWorldGen)) && (!WorldGen.remixWorldGen || !WorldGen.drunkWorldGen))
		{
			renderBG = true;
		}
		if (Main.mapFullscreen)
		{
			renderBG = false;
		}
		int topPos = 30;
		if (Main.gameMenu)
		{
			topPos = 0;
		}
		if (WorldGen.drunkWorldGen)
		{
			topPos = -180;
		}
		float surfacePos = (float)Main.worldSurface;
		if (surfacePos == 0f)
		{
			surfacePos = 1f;
		}
		float dynamicTopPos = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - 600f;
		double backgroundTopMagicNumber = (dynamicTopPos - screenOff / 2f) / (surfacePos * 16f);
		backgroundTopMagicNumber = 0f - MathHelper.Lerp((float)backgroundTopMagicNumber, 1f, 0f);
		backgroundTopMagicNumber = (0f - dynamicTopPos + screenOff / 2f) / (surfacePos * 16f);
		float bgGlobalScaleMultiplier = 2f;
		int pushBGTopHack = 0;
		int topOffset = -180;
		bool canOffset = true;
		int topHackPos = 0;
		if (Main.gameMenu)
		{
			topHackPos -= topOffset;
		}
		pushBGTopHack = topHackPos;
		pushBGTopHack += topPos;
		if (canOffset)
		{
			pushBGTopHack += topOffset;
		}
		if (renderBG)
		{
			if (closeFarBGPath != null)
			{
				Texture2D closeFarBG = ModContent.Request<Texture2D>(closeFarBGPath).Value;
				bgScale = 1.25f;
				bgParallax = 0.4;
				bgTopY = (int)(backgroundTopMagicNumber * 1800.0 + 1500.0) + (int)scAdj + pushBGTopHack;
				//Main.instance.SetBackgroundOffsets((Texture2D)ModContent.Request<Texture2D>("TheConfectionRebirth/Backgrounds/ConfectionSurfaceClose1"), backgroundTopMagicNumber, pushBGTopHack);
				bgScale *= bgGlobalScaleMultiplier;
				//Main.instance.LoadBackground(ModContent.Request<Texture2D>("TheConfectionRebirth/Backgrounds/ConfectionSurfaceClose1").Value);
				bgWidthScaled = (int)((float)closeFarBG.Width * bgScale);
				SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1.2f / (float)bgParallax);
				bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
				if (Main.gameMenu)
					bgTopY = 320 + pushBGTopHack;

				bgLoops = Main.screenWidth / bgWidthScaled + 2;
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					for (int i = 0; i < bgLoops; i++)
					{
						Main.spriteBatch.Draw(closeFarBG, new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, closeFarBG.Width, closeFarBG.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
					}
				}
			}
			if (closeMidBGPath != null)
			{
				Texture2D closeMidBG = ModContent.Request<Texture2D>(closeMidBGPath).Value;
				bgScale = 1.31f;
				bgParallax = 0.43;
				bgTopY = (int)(backgroundTopMagicNumber * 1950.0 + 1750.0) + (int)scAdj + pushBGTopHack;
				//Main.instance.SetBackgroundOffsets(textureSlot2, backgroundTopMagicNumber, pushBGTopHack);
				bgScale *= bgGlobalScaleMultiplier;
				//Main.instance.LoadBackground(textureSlot2);
				bgWidthScaled = (int)((float)closeMidBG.Width * bgScale);
				SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / (float)bgParallax);
				bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
				if (Main.gameMenu)
				{
					bgTopY = 400 + pushBGTopHack;
					bgStartX -= 80;
				}

				bgLoops = Main.screenWidth / bgWidthScaled + 2;
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					for (int i = 0; i < bgLoops; i++)
					{
						Main.spriteBatch.Draw(closeMidBG, new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, closeMidBG.Width, closeMidBG.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
					}
				}
			}
			if (closeBGPath != null)
			{
				Texture2D closeBG = ModContent.Request<Texture2D>(closeBGPath).Value;
				bgScale = 1.34f;
				bgParallax = 0.49;
				bgTopY = (int)(backgroundTopMagicNumber * 2100.0 + 2000.0) + (int)scAdj + pushBGTopHack;
				//Main.instance.SetBackgroundOffsets(textureSlot3, backgroundTopMagicNumber, pushBGTopHack);
				bgScale *= bgGlobalScaleMultiplier;
				//Main.instance.LoadBackground(textureSlot3);
				bgWidthScaled = (int)((float)closeBG.Width * bgScale);
				SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / (float)bgParallax);
				bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
				if (Main.gameMenu)
				{
					bgTopY = 480 + pushBGTopHack;
					bgStartX -= 120;
				}

				bgLoops = Main.screenWidth / bgWidthScaled + 2;
				if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
				{
					for (int i = 0; i < bgLoops; i++)
					{
						Main.spriteBatch.Draw(closeBG, new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, closeBG.Width, closeBG.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
					}
				}
			}
		}
		if (canOffset)
		{
			pushBGTopHack -= topOffset;
		}

		//Flashcode, flashes the background when the world globe is used
		Texture2D blackPixel = TextureAssets.MagicPixel.Value;
		float flashPower = AvalonWorld.contagionBGFlash;
		Color color = Color.Black * flashPower;
		Main.spriteBatch.Draw(blackPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
		return false;
	}
}
