using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class ContagionUndergroundBackground : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[1] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground1");
        textureSlots[2] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground2");
        textureSlots[3] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground3");
        textureSlots[4] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground4");
    }
}
