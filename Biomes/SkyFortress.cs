using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class SkyFortress : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/SkyFortress") : MusicID.OtherworldlySpace;

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<Systems.BiomeTileCounts>().SkyFortressTiles > 75;
    }
}
