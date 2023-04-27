using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class ContagionSurfaceDesertBackground : ModSurfaceBackgroundStyle
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
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceDesertBackground3");
    }

    public override int ChooseMiddleTexture()
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceDesertBackground2");
    }

    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionSurfaceDesertBackground1");
    }
}
