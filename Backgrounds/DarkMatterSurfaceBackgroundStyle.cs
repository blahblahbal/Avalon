using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class DarkMatterSurfaceBackgroundStyle : ModSurfaceBackgroundStyle
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

    /// <inheritdoc />
    public override int ChooseMiddleTexture() => BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/DarkMatterSurfaceMiddle");
}
