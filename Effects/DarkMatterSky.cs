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

		// End the spritebatch and begin again to allow for drawing the black hole center without transparency
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);

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

		UnifiedRandom? currentCloudSeed = new(CloudSeed.GetHashCode());

		int loops2 = 1500;
		for (int i = 0; i < loops2; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				float rot = MathHelper.PiOver2 * j - MathF.PI / 2.25f;
				float radius = currentCloudSeed.NextFloat(1920f, 3000f) * highResScale;
				float endRadius = 0.01f;
				float spiralTwist = 2.5f;
				//float endPointFix = 0.05f / spiralTwist;
				float inverseSpeed = 500f;
				float time = (((float)Main.timeForVisualEffects / inverseSpeed + i) % MathF.Tau) - MathHelper.PiOver2;

				bool rock = currentCloudSeed.NextBool(20);
				int cloud = currentCloudSeed.Next(rock ? darkMatterRocks.Length : darkMatterNimbuses.Length);
				Texture2D tex = rock ? darkMatterRocks[cloud].Value : darkMatterNimbuses[cloud].Value;
				Color color = new Color(currentCloudSeed.Next(240, 256), currentCloudSeed.Next(190, 211), currentCloudSeed.Next(225, 246), 200);

				float spiral = MathF.Atan2(MathF.Cos(time), MathF.Sin(time));
				static float Ease(float x) => Easings.PowIn(x, 7.5f);
				float spiralMult = Utils.Remap(Ease(Utils.Remap(spiral, -MathF.PI, MathF.PI, endRadius, 1f)), Ease(endRadius), 1f, endRadius, 1f);
				float finalSpiralTwist = (MathHelper.PiOver2 * (spiralTwist - 1f) + rot);
				float finalXPos = xPos + MathF.Cos(time * spiralTwist + finalSpiralTwist) * radius * spiralMult;
				float finalYPos = yPos - MathF.Sin(time * spiralTwist + finalSpiralTwist) * radius * spiralMult;

				float distanceMult = Easings.ExpoOut(spiralMult * 0.65f);

				Vector2 pos = new(finalXPos, finalYPos);
				Color finalColor = color * opacity * distanceMult;
				if (rock)
				{
					finalColor.A = (byte)Math.Clamp(255 * opacity, 0, 255);
				}
				float cloudRot = currentCloudSeed.NextFloat(-0.3f, 0.3f) + (rock ? -time * currentCloudSeed.NextFloat(-2f, 8f) : 0);

				// prevent drawing inside black hole
				float cull = 50f * highResScale;
				if (Math.Abs(finalXPos - xPos) < cull && Math.Abs(finalYPos - yPos) < cull)
				{
					continue;
				}

				float scale = highResScale * distanceMult;
				Vector2 texSizeRotated = tex.Size() * scale * new Vector2(1f - MathF.Abs(MathF.Cos(cloudRot)), 1f - MathF.Abs(MathF.Sin(cloudRot)));

				// prevent drawing outside the screen
				if (((pos + (texSizeRotated / 2f)).X < 0 && (pos + (texSizeRotated / 2f)).Y < 0) || ((pos - (texSizeRotated / 2f)).X > Main.PendingResolutionWidth && (pos - (texSizeRotated / 2f)).Y > Main.PendingResolutionHeight))
				{
					continue;
				}

				if (rock)
				{
					spriteBatch.End();
					spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);
				}

				spriteBatch.Draw(tex, pos,
				tex.Bounds, finalColor, cloudRot, tex.Size() / 2f,
				scale, SpriteEffects.None, 0);

				if (rock)
				{
					spriteBatch.End();
					spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Main.BackgroundViewMatrix.EffectMatrix);
				}
			}
		}
		int loops = 100;
		for (int i = 0; i < loops; i++)
		{
			float radius = 144f * highResScale;
			float inverseSpeed = 150f;
			float radiusYMult = 0.75f;
			int cloud = currentCloudSeed.Next(13);
			Texture2D tex = darkMatterNimbuses[cloud].Value;
			Color color = new Color(255, 200, 235, 200);
			float time = (float)Main.timeForVisualEffects / inverseSpeed + i;
			float finalXPos = xPos + MathF.Sin(time) * radius;
			float finalYPos = yPos + MathF.Sin(time + MathHelper.Pi / 3f) * radius * radiusYMult;
			float rot = MathHelper.PiOver4 * radiusYMult + currentCloudSeed.NextFloat(-0.3f, 0.3f);
			float distanceMult = MathF.Sin((time + MathHelper.Pi) / 2f);

			spriteBatch.Draw(tex, new Vector2(finalXPos, finalYPos),
			tex.Bounds, color * opacity * distanceMult, rot, tex.Size() / 2f,
			0.455f * highResScale * distanceMult, SpriteEffects.None, 0);
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
