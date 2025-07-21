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
	private readonly Asset<Texture2D>[] darkMatterRocks = new Asset<Texture2D>[20];
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
		for (int i = 0; i < darkMatterNimbuses.Length; i++)
		{
			darkMatterNimbuses[i] = ModContent.Request<Texture2D>($"Avalon/Backgrounds/DarkMatter/Nimbus/DarkMatterNimbus{i}");
		}
		for (int i = 0; i < darkMatterRocks.Length; i++)
		{
			darkMatterRocks[i] = ModContent.Request<Texture2D>($"Avalon/Backgrounds/DarkMatter/Rocks/DarkMatterRock{i}");
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
		if (!(maxDepth >= float.MaxValue) || !(minDepth < float.MaxValue))
		{
			return;
		}

		Matrix matrix = spriteBatch.transformMatrix;
		// End the spritebatch and begin again to draw with transparency and non-blurry scaling
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

		// Draw the sky texture
		spriteBatch.Draw(darkMatterSky.Value, new Rectangle(0, 0, Main.PendingResolutionWidth, Main.PendingResolutionHeight),
			new Color(51, 41, 48) * opacity); // Main.ColorOfTheSkies

		if (!Main.BackgroundEnabled)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, matrix);
			return;
		}

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
		var percentage = new Vector2(3840 / (float)Main.PendingResolutionWidth, 2160 / (float)Main.PendingResolutionHeight);

		// Create the modifier for the X coordinate of the black hole
		int xModifier = Math.Min((int)(modifier * percentage.X), xCenter);

		// Assign positions for the black hole and spiral clouds
		int xPos = (xCenter - (darkMatterBlackHole.Value.Width / 100) + xModifier) / 2;
		int yPos = (yCenter - (darkMatterBlackHole.Value.Height / 100)) / 2;

		float highResScale = 1f;
		float maxWidthHeightMinusPos = MathF.Max((Main.PendingResolutionWidth - xPos), (Main.PendingResolutionHeight - yPos));
		if (maxWidthHeightMinusPos - 1233 > 0) // 1233 is the value that once exceeded, it stops reaching the edges of the screen; essentially, this gives you a value to scale by on resolutions higher than 1920x1080
		{
			highResScale = maxWidthHeightMinusPos / 1233f;
		}

		// End the spritebatch and begin again to allow for drawing the black hole center without transparency
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

		// Draw the black hole's center
		spriteBatch.Draw(darkMatterBlackHole2.Value, new Vector2(xPos, yPos), null,
			Color.White * opacity, 0f,
			new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
			0.25f * highResScale + scaleMod, SpriteEffects.None, 1f);

		// Draw the black hole
		spriteBatch.Draw(darkMatterBlackHole.Value, new Vector2(xPos, yPos), null, new Color(255, 255, 255, 0) * opacity, 0f,
			new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
			0.25f * highResScale + scaleMod, SpriteEffects.None, 1f);

		UnifiedRandom? currentCloudSeed = new(CloudSeed.GetHashCode());

		float endRadius = 0.01f;
		float spiralTwist = 2.5f;
		float inverseSpeed = 500f;
		byte rockAlpha = (byte)Math.Clamp(255 * opacity, 0, 255);
		float tauPow = MathF.Pow(MathF.Tau, 2.5f);
		float time1 = (float)Main.timeForVisualEffects / inverseSpeed * ModContent.GetInstance<AvalonClientConfig>().DarkMatterVortexSpeed;
		int drawCount = 10;
		Color color = new(244, 195, 232, 0);
		float cullRange = 1920 * highResScale;

		for (float i = 0; i < MathF.Tau; i += MathF.Tau / 150f)
		{
			float[] radius = new float[drawCount];
			float[] cloudRotRand = new float[drawCount];
			for (int r = 0; r < drawCount; r++)
			{
				radius[r] = currentCloudSeed.NextFloat(1920f, 3000f) * highResScale;
				cloudRotRand[r] = currentCloudSeed.NextFloat(-0.3f, 0.3f);
			}
			for (int j = 0; j < 4; j++)
			{
				bool[] rock = new bool[drawCount];
				float[] rockRotRand = new float[drawCount];
				int[] cloud = new int[drawCount];
				for (int r = 0; r < drawCount; r++)
				{
					rock[r] = currentCloudSeed.NextBool(20);
					rockRotRand[r] = rock[r] ? currentCloudSeed.NextFloat(-2f, 8f) : 0f;
					cloud[r] = currentCloudSeed.Next(rock[r] ? darkMatterRocks.Length : darkMatterNimbuses.Length);
				}
				float time2 = (time1 + i) % MathF.Tau;

				// prevent drawing inside black hole
				if (time2 is > MathF.Tau - 1.75f)
				{
					continue;
				}

				// prevent drawing outside the screen, visualisation of texture/screen bounds over time can be played with here https://www.desmos.com/calculator/zwdebhrpie
				// todo: automatically scale based on resolution
				switch (j)
				{
					case 0:
						if (time2 is < 2f)
						{
							continue;
						}
						break;
					case 1:
						//if (time2 is < 1.7f or (> 2.75f and < 3.3f))
						//{
						//	continue;
						//}
						if (time2 < 1.4f) // this fucker just couldn't be consistent on higher resolutions so it needs to WASTE time rendering just to support them
						{
							continue;
						}
						break;
					case 2:
						if (time2 is < 2.8f)
						{
							continue;
						}
						break;
					case 3:
						if (time2 is < 2.25f)
						{
							continue;
						}
						break;
				}

				float finalTime = Utils.Remap(MathF.Pow(time2, 2.5f), 0f, tauPow, 0f, MathF.Tau, false) - MathHelper.PiOver2;

				float spiral = MathF.Atan2(MathF.Cos(finalTime), MathF.Sin(finalTime));
				float spiralMult = Utils.Remap(Easings.PowIn(Utils.Remap(spiral, -MathF.PI, MathF.PI, endRadius, 1f), 7.5f), 0f, 1f, endRadius, 1f);

				float rot = MathHelper.PiOver2 * j - MathF.PI / 2.25f;

				float finalSpiralTwist = MathHelper.PiOver2 * (spiralTwist - 1f) + rot;

				float distanceMult = Easings.ExpoOut(spiralMult * 0.65f);

				float finalPosCos = MathF.Cos(finalTime * spiralTwist + finalSpiralTwist) * spiralMult;
				float finalPosSin = MathF.Sin(finalTime * spiralTwist + finalSpiralTwist) * spiralMult;

				// prevent drawing outside the screen again, this time using actual positions and hardcoded estimates of the maximum image bounds rotated by 0.3rad, so it's more accurate than the other culling, but requires you to calculate everything above first
				if (xPos + finalPosCos * cullRange + 175 < 0 || yPos - finalPosSin * cullRange + 125 < 0 || xPos + finalPosCos * cullRange - 175 > Main.PendingResolutionWidth || yPos - finalPosSin * cullRange - 125 > Main.PendingResolutionHeight)
				{
					continue;
				}

				for (int r = 0; r < drawCount; r++)
				{
					Texture2D tex = rock[r] ? darkMatterRocks[cloud[r]].Value : darkMatterNimbuses[cloud[r]].Value;

					Color finalColor = color * opacity * distanceMult;
					if (rock[r])
					{
						finalColor.A = rockAlpha;
					}
					else
					{
						finalColor = finalColor.MultiplyRGBByFloat(distanceMult);
					}

					spriteBatch.Draw(tex, new Vector2(xPos + finalPosCos * radius[r], yPos - finalPosSin * radius[r]),
					tex.Bounds, finalColor, cloudRotRand[r] + (rock[r] ? -finalTime * rockRotRand[r] : 0), tex.Size() / 2f,
					highResScale * distanceMult, SpriteEffects.None, 0);
				}
			}
		}

		float radiusInner = 144f * highResScale;
		//float inverseSpeedInner = 150f;
		float inverseSpeedInner = 125f;
		float radiusYMult = 0.75f;
		float timeInner1 = (float)Main.timeForVisualEffects / inverseSpeedInner * ModContent.GetInstance<AvalonClientConfig>().DarkMatterVortexSpeed;
		float scaleInner = 0.455f * highResScale;
		Color colorInner = new(255, 200, 235, 200);

		for (int i = 0; i < 100; i++)
		{
			int cloud = currentCloudSeed.Next(13);
			Texture2D tex = darkMatterNimbuses[cloud].Value;
			float timeInner2 = timeInner1 + i;
			float finalXPos = xPos + MathF.Sin(timeInner2) * radiusInner;
			float finalYPos = yPos + MathF.Sin(timeInner2 + MathHelper.Pi / 3f) * radiusInner * radiusYMult;
			float rot = MathHelper.PiOver4 * radiusYMult + currentCloudSeed.NextFloat(-0.3f, 0.3f);
			float distanceMult = MathF.Sin((timeInner2 + MathHelper.Pi) / 2f);
			Color finalColor = (colorInner * opacity * distanceMult).MultiplyRGBByFloat(distanceMult);
			finalColor.A = 0;

			spriteBatch.Draw(tex, new Vector2(finalXPos, finalYPos),
			tex.Bounds, finalColor, rot, tex.Size() / 2f,
			scaleInner * distanceMult, SpriteEffects.None, 0);
		}

		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, matrix);
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
