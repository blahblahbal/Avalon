using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Effects;

public class DarkMatterSky : CustomSky
{
	private static int blackHoleCounter;
	private static int blackHoleFrame;
	private static int surfaceFrame;
	private static int surfaceFrameCounter;
	private readonly Asset<Texture2D>[] darkMatterBackgrounds = new Asset<Texture2D>[25];
	private readonly Asset<Texture2D>[] darkMatterNimbuses = new Asset<Texture2D>[13];
	private Asset<Texture2D>? darkMatterBlackHole;
	private Asset<Texture2D>? darkMatterBlackHole2;
	private Asset<Texture2D>? darkMatterSky;
	private float opacity;
	private bool skyActive;

	private readonly UnifiedRandom CloudSeed = new();
	public override void OnLoad()
	{
		darkMatterSky = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterSky");
		darkMatterBlackHole = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole");
		darkMatterBlackHole2 = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole2");
		for (int i = 0; i < darkMatterBackgrounds.Length; i++)
		{
			darkMatterBackgrounds[i] = ModContent.Request<Texture2D>($"Avalon/Backgrounds/DarkMatter/DarkMatterCloud{i}");
		}
		for (int i = 0; i < darkMatterNimbuses.Length; i++)
		{
			darkMatterNimbuses[i] = ModContent.Request<Texture2D>($"Avalon/Backgrounds/DarkMatter/Nimbus/DarkMatterNimbus{i}");
		}
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		Filters.Scene.Activate("Avalon:DarkMatter");
		skyActive = true;
	}

	public override void Deactivate(params object[] args)
	{
		Filters.Scene.Deactivate("Avalon:DarkMatter");
		skyActive = false;
	}

	public override void Reset() => skyActive = false;

	public override bool IsActive()
	{
		if (!skyActive)
		{
			return opacity > 0f;
		}

		return true;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		if (!(maxDepth >= float.MaxValue) || !(minDepth < float.MaxValue) || !Main.BackgroundEnabled)
		{
			return;
		}

		Matrix matrix = spriteBatch.transformMatrix;
		// End the spritebatch and begin again to draw with transparency and non-blurry scaling
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

		// Draw the sky texture
		spriteBatch.Draw(darkMatterSky.Value, new Rectangle(0, 0, Main.PendingResolutionWidth, Main.PendingResolutionHeight),
			new Color(51, 41, 48) * opacity); // Main.ColorOfTheSkies

		if (Main.netMode == NetmodeID.Server)
		{
			return;
		}

		if (!Main.gamePaused)
		{
			// Surface frame counter
			if (++surfaceFrameCounter > 3)
			{
				surfaceFrame = (surfaceFrame + 1) % 25;
				surfaceFrameCounter = 0;
			}

			// Black hole pulse timer
			if (++blackHoleCounter > 2)
			{
				blackHoleFrame = (blackHoleFrame + 1) % 10;
				blackHoleCounter = 0;
			}
		}

		// Math to allow the pulsing to work
		float scaleMod = ((blackHoleFrame % 5) - 2) * 0.0003f;
		if (blackHoleFrame is >= 5 and <= 9)
		{
			scaleMod *= -1;
		}

		// Find the center of the screen
		int xCenter = Main.PendingResolutionWidth / 2;
		int yCenter = Main.PendingResolutionHeight / 2;

		// Modifier to allow the black hole to draw in the same place no matter your resolution
		int modifier = (Main.PendingResolutionWidth - Main.PendingResolutionHeight) / 4;

		// If the your game screen's height is greater than your game screen's width, swap the modifier
		if (modifier < 0)
		{
			modifier = (Main.PendingResolutionHeight - Main.PendingResolutionWidth) / 4;
		}

		// Create a variable for the percentage to scale the textures by
		var percentage = new Vector2(3840 / (float)Main.PendingResolutionWidth, 2160 / (float)Main.PendingResolutionHeight); // FIX FOR 4k LATER 3840x2160

		// Create the modifier for the X coordinate of the black hole
		int xModifier = (int)(modifier * percentage.X);

		// Assign positions for the black hole and spiral clouds
		int xPos = (xCenter - (darkMatterBlackHole.Value.Width / 100) + xModifier) / 2;
		int yPos = (yCenter - (darkMatterBlackHole.Value.Height / 100)) / 2;

		float highResScale = 1f;
		float maxWidthHeightMinusPos = MathF.Max((Main.PendingResolutionWidth - xPos), (Main.PendingResolutionHeight - yPos));
		if (maxWidthHeightMinusPos - 1233 > 0) // 1233 is the value that once exceeded, it stops reaching the edges of the screen; essentially, this gives you a value to scale by on resolutions higher than 1920x1080
		{
			highResScale = maxWidthHeightMinusPos / 1233f;
		}

		// Redraw stars that are in the background
		Vector2 origin = default;
		Vector2 position = default;
		//for (int i = 0; i < Main.star.Length; i++)
		//{
		//	Star star = Main.star[i];
		//	if (star != null)
		//	{
		//		Texture2D starTex = TextureAssets.Star[star.type].Value;
		//		origin = new Vector2(starTex.Width * 0.5f, starTex.Height * 0.5f);
		//		int bgTop = (int)((0f - Main.screenPosition.Y) / ((Main.worldSurface * 16.0) - 600.0) * 200.0);
		//		float posX = star.position.X * (Main.screenWidth / 800f);
		//		float posY = star.position.Y * (Main.screenHeight / 600f);
		//		position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
		//		spriteBatch.Draw(starTex, position, new Rectangle(0, 0, starTex.Width, starTex.Height),
		//			Color.White * star.twinkle * 0.952f * opacity, star.rotation, origin, star.scale * star.twinkle,
		//			SpriteEffects.None, 0f);
		//	}
		//}

		// End the spritebatch and begin again to allow for drawing the black hole center without transparency
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

		//for (int i = 0; i < 200; i++)
		//{
		//	for (int j = 1; j < 3; j++)
		//	{
		//		float mult = 300f;
		//		float div = 150f;
		//		spriteBatch.Draw(TextureAssets.Cloud[17].Value, new Vector2(xPos + MathF.Sin((float)Main.timeForVisualEffects / div + i) * mult, yPos + MathF.Sin((float)Main.timeForVisualEffects / div + i + j) * mult),
		//		TextureAssets.Cloud[17].Value.Bounds, Color.White * opacity * MathF.Sin(((float)Main.timeForVisualEffects / div + i + j) / 2f), 0, TextureAssets.Cloud[17].Value.Size() / 2f,
		//		highResScale * 0.25f, SpriteEffects.None, 0);
		//	}
		//}
		//for (int i = 0; i < 200; i++)
		//{
		//	float mult = 300f;
		//	float div = 150f;
		//	spriteBatch.Draw(TextureAssets.Cloud[17].Value, new Vector2(xPos + MathF.Sin((float)Main.timeForVisualEffects / div + i) * mult, yPos + MathF.Sin((float)Main.timeForVisualEffects / div + i + 1) * mult),
		//	TextureAssets.Cloud[17].Value.Bounds, Color.White * opacity * MathF.Sin(((float)Main.timeForVisualEffects / div + i - 1.5f) / 2f), 0, TextureAssets.Cloud[17].Value.Size() / 2f,
		//	highResScale * 0.25f, SpriteEffects.None, 0);

		//	spriteBatch.Draw(TextureAssets.Cloud[17].Value, new Vector2(xPos + MathF.Sin((float)Main.timeForVisualEffects / div + i) * mult, yPos + MathF.Sin((float)Main.timeForVisualEffects / div + i + 2) * mult),
		//	TextureAssets.Cloud[17].Value.Bounds, Color.White * opacity * MathF.Sin(((float)Main.timeForVisualEffects / div + i + 3) / 2f), 0, TextureAssets.Cloud[17].Value.Size() / 2f,
		//	highResScale * 0.25f, SpriteEffects.None, 0);
		//}

		// Draw the black hole's center
		spriteBatch.Draw(darkMatterBlackHole2.Value, new Vector2(xPos, yPos), null,
			Color.White * opacity, 0f,
			new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
			0.25f * highResScale + scaleMod, SpriteEffects.None, 1f);

		// End and begin again, allowing transparency and non-blurry scaling
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

		// Draw the black hole
		spriteBatch.Draw(darkMatterBlackHole.Value, new Vector2(xPos, yPos), null, Color.White * opacity, 0f,
			new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
			0.25f * highResScale + scaleMod, SpriteEffects.None, 1f);

		// temporary to toggle between old and new clouds, until the new ones are perfect
		if (true)
		{
			// Draw the spiral clouds
			spriteBatch.Draw(darkMatterBackgrounds[surfaceFrame].Value, new Vector2(xPos, yPos),
				new Rectangle(0, 0, 819, 819), Color.White * opacity, 0, new Vector2(819, 819) / 2f,
				3.011f * highResScale, SpriteEffects.None, 0);
		}
		else
		{
			UnifiedRandom? currentCloudSeed = new(CloudSeed.GetHashCode());

			int loops2 = 500;
			for (int i = 0; i < loops2; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int tempJ = j;

					float radius = currentCloudSeed.NextFloat(700, 1250) * highResScale;
					float inverseSpeed = 150f;
					//float radiusMult = currentCloudSeed.NextFloat(-);
					int cloud = currentCloudSeed.Next(13);
					//if (cloud > 8)
					//{
					//	cloud += 5;
					//}
					//Texture2D tex = TextureAssets.Cloud[cloud].Value;
					Texture2D tex = darkMatterNimbuses[cloud].Value;
					//Color color = new Color(255, 150, 225, 108);
					Color color = new Color(currentCloudSeed.Next(240, 256), currentCloudSeed.Next(190, 211), currentCloudSeed.Next(225, 246), 200);
					//Color color = Color.White;
					float time = (float)Main.timeForVisualEffects / inverseSpeed + i;
					float spiralMult = MathF.Pow(Utils.Remap(MathF.Atan2(MathF.Cos(time + (MathHelper.PiOver2 * tempJ)), MathF.Sin(time + (MathHelper.PiOver2 * tempJ))), -MathHelper.Pi, MathHelper.Pi, 0f, 1f), 2.75f);
					float finalXPos = xPos + MathF.Cos(time) * radius * spiralMult;
					float finalYPos = yPos - MathF.Sin(time) * radius * spiralMult;
					float rot = currentCloudSeed.NextFloat(-0.3f, 0.3f);
					float distanceMult = Utils.Remap(MathF.Pow(1f + spiralMult, 2.5f), 0, 5f, 0, 1f);
					if (distanceMult < 0.25f)
					{
						distanceMult = Easings.PowIn(distanceMult, 2f);
					}
					//distanceMult = Easings.ExpoOut(spiralMult * 0.4f) * 0.4f;

					spriteBatch.Draw(tex, new Vector2(finalXPos, finalYPos),
					tex.Bounds, color * opacity * distanceMult, rot, tex.Size() / 2f,
					1f * highResScale * distanceMult, SpriteEffects.None, 0);
				}
			}
			int loops = 100;
			for (int i = 0; i < loops; i++)
			{
				float radius = 144f * highResScale;
				float inverseSpeed = 150f;
				float radiusYMult = 0.75f;
				int cloud = currentCloudSeed.Next(13);
				//if (cloud > 8)
				//{
				//	cloud += 5;
				//}
				//Texture2D tex = TextureAssets.Cloud[cloud].Value;
				Texture2D tex = darkMatterNimbuses[cloud].Value;
				//Color color = new Color(255, 150, 225, 108);
				Color color = new Color(255, 200, 235, 200);
				//Color color = Color.White;
				float time = (float)Main.timeForVisualEffects / inverseSpeed + i;
				float finalXPos = xPos + MathF.Sin(time) * radius;
				//float funRadiusMod = (radius * Utils.Remap(MathF.Sin(time), -1f, 1f, 0f, 1f));
				//float finalYPos = yPos + MathF.Sin(time + MathHelper.Pi / 3f) * funRadiusMod * radiusYMult;
				float finalYPos = yPos + MathF.Sin(time + MathHelper.Pi / 3f) * radius * radiusYMult;
				float rot = MathHelper.PiOver4 * radiusYMult + currentCloudSeed.NextFloat(-0.3f, 0.3f);
				float distanceMult = MathF.Sin((time + MathHelper.Pi) / 2f);

				spriteBatch.Draw(tex, new Vector2(finalXPos, finalYPos),
				tex.Bounds, color * opacity * distanceMult, rot, tex.Size() / 2f,
				0.455f * highResScale * distanceMult, SpriteEffects.None, 0);
				//spriteBatch.Draw(tex, new Vector2(finalXPos, finalYPos),
				//tex.Bounds, color * opacity * distanceMult, rot, tex.Size() / 2f,
				//0.35f * highResScale * Utils.Remap(distanceMult, -1f, 1f, -0.5f, 1f), SpriteEffects.None, 0);

				//spriteBatch.Draw(tex, new Vector2(xPos + MathF.Sin((float)Main.timeForVisualEffects / inverseSpeed + i) * radius, yPos + MathF.Sin((float)Main.timeForVisualEffects / inverseSpeed + i + MathHelper.PiOver4 * 3f) * radius),
				//tex.Bounds, color * opacity * MathF.Sin(((float)Main.timeForVisualEffects / inverseSpeed + i + MathF.PI) / 2f), -MathHelper.PiOver4, tex.Size() / 2f,
				//highResScale * 0.35f, SpriteEffects.None, 0);
			}
		}


		//spriteBatch.Draw(darkMatterBackgrounds[surfaceFrame].Value, new Vector2(xPos, yPos), null,
		//    new Color(255, 255, 255, 255), 0f,
		//    new Vector2(darkMatterBackgrounds[surfaceFrame].Width() >> 1,
		//        darkMatterBackgrounds[surfaceFrame].Height() >> 1), 3f, SpriteEffects.None, 1f);

		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, matrix);
		// DOES NOT WORK, COPIED FROM VANILLA
		//float num = Math.Min(PlayerInput.RealScreenHeight, Main.LogicCheckScreenHeight);
		//float num2 = Main.screenPosition.Y + Main.screenHeight / 2 - num / 2f;

		//scAdj = (float)(Main.worldSurface * 16.0) / (num2 + num);

		//float screenOff = Main.screenHeight - 600f;
		//float worldSurfaceMod = (float)Main.worldSurface;
		//if (worldSurfaceMod == 0f)
		//{
		//    worldSurfaceMod = 1f;
		//}
		//float num16 = Main.screenPosition.Y + Main.screenHeight / 2 - 600f;
		//double num17 = (num16 - screenOff / 2f) / (worldSurfaceMod * 16f);
		//num17 = 0f - MathHelper.Lerp((float)num17, 1f, 0f);
		//num17 = (0f - num16 + screenOff / 2f) / (worldSurfaceMod * 16f);
		//float num18 = 2f;
		//int num19 = 0;


		//parallax = 0.17;
		//bgScale = 1.1f;
		//bgTopY = (int)(num17 * 1400.0 + 900.0) + (int)scAdj + num19;

		//bgScale *= num18;
		//bgWidthScaled = (int)((double)(2100f * bgScale) * 1.05);
		//bgStartX = (int)(0.0 - Math.IEEERemainder(Main.screenPosition.X * parallax, bgWidthScaled) - bgWidthScaled / 2);
		////
		//bgLoops = Main.screenWidth / bgWidthScaled + 2;
		//if (Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
		//{
		//    for (int m = 0; m < bgLoops; m++)
		//    {

		//        spriteBatch.Draw(darkMatterFloatingRocks.Value, new Vector2(bgStartX + bgWidthScaled * (m + 1), bgTopY), new Rectangle(0, 0, darkMatterFloatingRocks.Value.Width, darkMatterFloatingRocks.Value.Height), new Color(255, 255, 255, 255), 0f, default, bgScale, SpriteEffects.None, 1f);
		//    }
		//}

		// Draw the floating rocks
		// spriteBatch.Draw(darkMatterFloatingRocks.Value, new Vector2(xPos, yPos + 200), null,
		//     new Color(255, 255, 255, 255), 0f,
		//     new Vector2(darkMatterFloatingRocks.Width() >> 1, darkMatterFloatingRocks.Height() >> 1), 1f,
		//     SpriteEffects.None, 1f);
		// spriteBatch.Draw(darkMatterFloatingRocks.Value,
		//     new Vector2(xPos + darkMatterFloatingRocks.Value.Width, yPos + 200), null,
		//     new Color(255, 255, 255, 255), 0f,
		//     new Vector2(darkMatterFloatingRocks.Width() >> 1, darkMatterFloatingRocks.Height() >> 1), 1f,
		//     SpriteEffects.None, 1f);
	}

	public override void Update(GameTime gameTime)
	{
		//Main.eclipseLight = 1f;
		if (!DarkMatterWorld.InArea)
		{
			skyActive = false;
		}

		if (skyActive && opacity < 1f)
		{
			opacity += 0.02f;
		}
		else if (!skyActive && opacity > 0f)
		{
			opacity -= 0.02f;
		}
	}

	public override float GetCloudAlpha() => ((1f - opacity) * 0.97f) + 0.03f;

	public override Color OnTileColor(Color inColor)
	{
		return Color.Lerp(inColor, new Color(126, 71, 107) * 0.55f, opacity);
	}

}
