using Avalon.Common.Players;
using Avalon.Players;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Effects;

public class DarkMatterSky : CustomSky
{
    private static int blackHoleCounter;
    private static int blackHoleFrame;
    private static int surfaceFrame;
    private static int surfaceFrameCounter;
    private readonly Asset<Texture2D>[] darkMatterBackgrounds = new Asset<Texture2D>[25];
    private Asset<Texture2D>? darkMatterBlackHole;
    private Asset<Texture2D>? darkMatterBlackHole2;
    private Asset<Texture2D>? darkMatterSky;
    private float opacity;
    private bool skyActive;

    public override void OnLoad()
    {
        darkMatterSky = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterSky");
        darkMatterBlackHole = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole");
        darkMatterBlackHole2 = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole2");
        for (int i = 0; i < darkMatterBackgrounds.Length; i++)
        {
            darkMatterBackgrounds[i] =
                ModContent.Request<Texture2D>($"Avalon/Backgrounds/DarkMatter/DarkMatterCloud{i}");
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

        // End the spritebatch and begin again to draw with transparency and non-blurry scaling
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

        // Draw the sky texture
        spriteBatch.Draw(darkMatterSky.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),
            new Color(51, 41, 48) * opacity); // Main.ColorOfTheSkies

        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

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

        // Math to allow the pulsing to work
        float scaleMod = ((blackHoleFrame % 5) - 2) * 0.0003f;
        if (blackHoleFrame is >= 5 and <= 9)
        {
            scaleMod *= -1;
        }

        // Find the center of the screen
        int xCenter = Main.ScreenSize.X / 2;
        int yCenter = Main.ScreenSize.Y / 2;

        // Modifier to allow the black hole to draw in the same place no matter your resolution
        int modifier = (Main.screenWidth - Main.screenHeight) / 2;

        // If the your game screen's height is greater than your game screen's width, swap the modifier
        if (modifier < 0)
        {
            modifier = (Main.screenHeight - Main.screenWidth) / 2;
        }

        // Create a variable for the percentage to scale the textures by
        var percentage = new Vector2(1920 / Main.ScreenSize.X, 1080 / Main.ScreenSize.Y); // FIX FOR 4k LATER 3840x2160

        // Create the modifier for the X coordinate of the black hole
        int xModifier = (int)(modifier * percentage.X);

        // Assign positions for the black hole and spiral clouds
        int xPos = (xCenter - (darkMatterBlackHole.Value.Width / 100) + xModifier) / 2;
        int yPos = (yCenter - (darkMatterBlackHole.Value.Height / 100)) / 2;

        // Redraw stars that are in the background
        Vector2 origin = default;
        Vector2 position = default;
        for (int i = 0; i < Main.star.Length; i++)
        {
            Star star = Main.star[i];
            if (star != null)
            {
                Texture2D starTex = TextureAssets.Star[star.type].Value;
                origin = new Vector2(starTex.Width * 0.5f, starTex.Height * 0.5f);
                int bgTop = (int)((0f - Main.screenPosition.Y) / ((Main.worldSurface * 16.0) - 600.0) * 200.0);
                float posX = star.position.X * (Main.screenWidth / 800f);
                float posY = star.position.Y * (Main.screenHeight / 600f);
                position = new Vector2(posX + origin.X, posY + origin.Y + bgTop);
                spriteBatch.Draw(starTex, position, new Rectangle(0, 0, starTex.Width, starTex.Height),
                    Color.White * star.twinkle * 0.952f * opacity, star.rotation, origin, star.scale * star.twinkle,
                    SpriteEffects.None, 0f);
            }
        }

        // End the spritebatch and begin again to allow for drawing the black hole center without transparency
        spriteBatch.End();
        spriteBatch.Begin();

        // Draw the black hole's center
        spriteBatch.Draw(darkMatterBlackHole2.Value, new Vector2(xPos, yPos), null,
            new Color(255, 255, 255, 255), 0f,
            new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
            0.25f + scaleMod, SpriteEffects.None, 1f);

        // End and begin again, allowing transparency and non-blurry scaling
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);

        // Draw the black hole
        spriteBatch.Draw(darkMatterBlackHole.Value, new Vector2(xPos, yPos), null, Color.White, 0f,
            new Vector2(darkMatterBlackHole2.Width() >> 1, darkMatterBlackHole2.Height() >> 1),
            0.25f + scaleMod, SpriteEffects.None, 1f);

        // Draw the spiral clouds
        spriteBatch.Draw(darkMatterBackgrounds[surfaceFrame].Value, new Vector2(xPos, yPos), null,
            new Color(255, 255, 255, 255), 0f,
            new Vector2(darkMatterBackgrounds[surfaceFrame].Width() >> 1,
                darkMatterBackgrounds[surfaceFrame].Height() >> 1), 3f, SpriteEffects.None, 1f);

        spriteBatch.End();
        spriteBatch.Begin();
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
        if (!Main.LocalPlayer.GetModPlayer<AvalonPlayer>().DarkMatterMonolith)
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
        return new Color(126, 71, 107) * 0.55f;
    }

}
