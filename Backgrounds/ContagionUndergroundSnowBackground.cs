using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class ContagionUndergroundSnowBackground : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[1] =
            ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundSnowBackground1");
        textureSlots[2] =
            ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundSnowBackground2");
        textureSlots[3] =
            ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundSnowBackground3");
        textureSlots[4] =
            ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundSnowBackground4");
    }
}
