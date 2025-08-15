using Avalon.Common;
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
		if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
		{
			return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground6");
		}
		return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground3");
	}

	public override int ChooseMiddleTexture()
	{
		if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
		{
			return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground5");
		}
		return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground2");
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		b -= 75;
		if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
		{
			return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground1");
		}
		return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground7");
	}


	/*public override void ModifyFarFades(float[] fades, float transitionSpeed)
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
        if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
        {
            return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceFarBG");
        }
        return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceFarBG");
    }

    public override int ChooseMiddleTexture()
    {
        if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
        {
            return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceBackground2");
        }
        return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceBackground5");
    }

    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
        {
            return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceClose1");
        }
        return BackgroundTextureLoader.GetBackgroundSlot("Avalon/Backgrounds/ContagionSurfaceClose1");
    }

    public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
    {
        float bgScale = (float)typeof(Main).GetField("bgScale", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
        float screenOff = (float)typeof(Main).GetField("screenOff", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        double bgParallax = (double)typeof(Main).GetField("bgParallax", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        int bgTopY = (int)typeof(Main).GetField("bgTopY", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        float scAdj = (float)typeof(Main).GetField("scAdj", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        int bgWidthScaled = (int)typeof(Main).GetField("bgWidthScaled", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
        int bgStartX = (int)typeof(Main).GetField("bgStartX", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        int bgLoops = (int)typeof(Main).GetField("bgLoops", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Main.instance);
        Color ColorOfSurfaceBackgroundsModified = (Color)typeof(Main).GetField("ColorOfSurfaceBackgroundsModified", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

        string TexturePath = "Avalon/Backgrounds/ContagionSurfaceBackground1";
        string TexturePath2 = "Avalon/Backgrounds/ContagionSurfaceBackground4";
        string TexturePath3 = "TheConfectionRebirth/Projectiles/CreamBolt";
        if (ModContent.GetInstance<AvalonWorld>().SecondaryContagionBG == 1)
        {
            TexturePath = "Avalon/Backgrounds/ContagionSurfaceBackground1";
            TexturePath2 = "Avalon/Backgrounds/ContagionSurfaceBackground4";
            TexturePath3 = "Avalon/";
        }

        bool flag = false;
        if ((!Main.remixWorld || (Main.gameMenu && !WorldGen.remixWorldGen)) && (!WorldGen.remixWorldGen || !WorldGen.drunkWorldGen))
        {
            flag = true;
        }
        if (Main.mapFullscreen)
        {
            flag = false;
        }
        int num = 30;
        if (Main.gameMenu)
        {
            num = 0;
        }
        if (WorldGen.drunkWorldGen)
        {
            num = -180;
        }
        float num12 = (float)Main.worldSurface;
        if (num12 == 0f)
        {
            num12 = 1f;
        }
        float num17 = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - 600f;
        double backgroundTopMagicNumber = (num17 - screenOff / 2f) / (num12 * 16f);
        backgroundTopMagicNumber = 0f - MathHelper.Lerp((float)backgroundTopMagicNumber, 1f, 0f);
        backgroundTopMagicNumber = (0f - num17 + screenOff / 2f) / (num12 * 16f);
        float bgGlobalScaleMultiplier = 2f;
        int pushBGTopHack = 0;
        int num3 = -180;
        bool flag2 = true;
        int num4 = 0;
        if (Main.gameMenu)
        {
            num4 -= num3;
        }
        pushBGTopHack = num4;
        pushBGTopHack += num;
        if (flag2)
        {
            pushBGTopHack += num3;
        }
        if (flag)
        {
            bgScale = 1.25f;
            bgParallax = 0.4;
            bgTopY = (int)(backgroundTopMagicNumber * 1800.0 + 1500.0) + (int)scAdj + pushBGTopHack;
            //Main.instance.SetBackgroundOffsets((Texture2D)ModContent.Request<Texture2D>("TheConfectionRebirth/Backgrounds/ConfectionSurfaceClose1"), backgroundTopMagicNumber, pushBGTopHack);
            bgScale *= bgGlobalScaleMultiplier;
            //Main.instance.LoadBackground(ModContent.Request<Texture2D>("TheConfectionRebirth/Backgrounds/ConfectionSurfaceClose1").Value);
            bgWidthScaled = (int)((float)ModContent.Request<Texture2D>(TexturePath3).Width() * bgScale);
            SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1.2f / (float)bgParallax);
            bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
            if (Main.gameMenu)
                bgTopY = 320 + pushBGTopHack;

            bgLoops = Main.screenWidth / bgWidthScaled + 2;
            if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
            {
                for (int i = 0; i < bgLoops; i++)
                {
                    Main.spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(TexturePath3), new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, ModContent.Request<Texture2D>(TexturePath3).Width(), ModContent.Request<Texture2D>(TexturePath3).Height()), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
                }
            }

            bgScale = 1.31f;
            bgParallax = 0.43;
            bgTopY = (int)(backgroundTopMagicNumber * 1950.0 + 1750.0) + (int)scAdj + pushBGTopHack;
            //Main.instance.SetBackgroundOffsets(textureSlot2, backgroundTopMagicNumber, pushBGTopHack);
            bgScale *= bgGlobalScaleMultiplier;
            //Main.instance.LoadBackground(textureSlot2);
            bgWidthScaled = (int)((float)ModContent.Request<Texture2D>(TexturePath2).Width() * bgScale);
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
                    Main.spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(TexturePath2), new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, ModContent.Request<Texture2D>(TexturePath2).Width(), ModContent.Request<Texture2D>(TexturePath2).Height()), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
                }
            }
            bgScale = 1.34f;
            bgParallax = 0.49;
            bgTopY = (int)(backgroundTopMagicNumber * 2100.0 + 2000.0) + (int)scAdj + pushBGTopHack;
            //Main.instance.SetBackgroundOffsets(textureSlot3, backgroundTopMagicNumber, pushBGTopHack);
            bgScale *= bgGlobalScaleMultiplier;
            //Main.instance.LoadBackground(textureSlot3);
            bgWidthScaled = (int)((float)ModContent.Request<Texture2D>(TexturePath).Width() * bgScale);
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
                    Main.spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(TexturePath), new Vector2(bgStartX + bgWidthScaled * i, bgTopY), new Rectangle(0, 0, ModContent.Request<Texture2D>(TexturePath).Width(), ModContent.Request<Texture2D>(TexturePath).Height()), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, SpriteEffects.None, 0f);
                }
            }
        }
        if (flag2)
        {
            pushBGTopHack -= num3;
        }

        return false;
    }*/

	//private static int SurfaceFrameCounter;
	//private static int SurfaceFrame;
	//public override int ChooseMiddleTexture()
	//{
	//    if (++SurfaceFrameCounter > 12)
	//    {
	//        SurfaceFrame = (SurfaceFrame + 1) % 4;
	//        SurfaceFrameCounter = 0;
	//    }
	//    switch (SurfaceFrame)
	//    {
	//        case 0:
	//            return mod.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ExampleBiomeSurfaceMid0");
	//        case 1:
	//            return mod.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ExampleBiomeSurfaceMid1");
	//        case 2:
	//            return mod.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ExampleBiomeSurfaceMid2");
	//        case 3:
	//            return mod.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ExampleBiomeSurfaceMid3");
	//        default:
	//            return -1;
	//    }
	//}
}
