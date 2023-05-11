using Avalon.Backgrounds;
using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class AshenOvergrowth : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
    //public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/ContagionWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    //public override string BackgroundPath => base.BackgroundPath;
    //public override string MapBackground => BackgroundPath;
    //public override int Music
    //{
    //    get
    //    {
    //        return ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Contagion") : MusicID.Crimson;
    //    }
    //}
    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<BiomeTileCounts>().AshenOvergrowthTiles > 150 && player.ZoneUnderworldHeight;
        //return player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight;
    }
}
