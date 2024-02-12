using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class TropicsUndergroundBackground : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[1] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsUndergroundBackground1");
        textureSlots[2] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsUndergroundBackground2");
        textureSlots[3] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsUndergroundBackground3");
        textureSlots[4] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/TropicsUndergroundBackground4");
    }
}
