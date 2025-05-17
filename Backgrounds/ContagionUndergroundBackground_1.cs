using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class ContagionUndergroundBackground_1 : ModUndergroundBackgroundStyle
{
	public override void FillTextureArray(int[] textureSlots)
	{
		textureSlots[0] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground_1_0"); // Sky border
		textureSlots[1] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground_1_1"); // Dirt layer
		textureSlots[2] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground_1_2"); // Underground border
		textureSlots[3] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground_1_3"); // Underground
		textureSlots[4] = ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground_1_4"); // Hell border?
	}
}
