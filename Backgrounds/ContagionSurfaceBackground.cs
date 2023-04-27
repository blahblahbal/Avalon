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
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground2");
    }

    public override int ChooseMiddleTexture()
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground3");
    }

    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        b -= 75;
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceBackground1");
    }

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
