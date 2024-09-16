using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class NearHellcastle : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => MusicID.Hell;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
	public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/PhantomOvergrowthWaterStyle");
	public override string MapBackground => BackgroundPath;

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<Systems.BiomeTileCounts>().HellCastleTiles > 350 && player.ZoneUnderworldHeight;
    }
}
