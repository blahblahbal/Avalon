using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class TropicsSurfaceBackground : ModSurfaceBackgroundStyle
{
    private static int SurfaceFrameCounter;
    private static int SurfaceFrame;

    // Use this to keep far Backgrounds like the mountains.
    // This shit doesn't work. Why?
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

    // Also this displays too low down.
    public override int ChooseFarTexture()
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsSurfaceBackground3");
    }

    public override int ChooseMiddleTexture()
    {
        if (++SurfaceFrameCounter > 12)
        {
            SurfaceFrame = (SurfaceFrame + 1) % 4;
            SurfaceFrameCounter = 0;
        }

        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsSurfaceBackground2");
    }

    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        b -= 200;
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsSurfaceBackground1");
    }
}
