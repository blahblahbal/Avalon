using Avalon.Backgrounds;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class UndergroundTropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    public override string BestiaryIcon => ModContent.GetInstance<Savanna>().BestiaryIcon;
	public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/TropicsWaterStyle");
	public override string BackgroundPath => base.BackgroundPath;
    public override int BiomeTorchItemType => ModContent.ItemType<Items.Placeable.Furniture.SavannaTorch>();
    public override string MapBackground => BackgroundPath;
    public override int Music
    {
        get
        {
            return MusicID.JungleUnderground;
            //return ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundTropics") : MusicID.JungleUnderground;
        }
    }

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    {
        get
        {
            return ModContent.GetInstance<TropicsUndergroundBackground>();
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return !player.ZoneDungeon && ModContent.GetInstance<Systems.BiomeTileCounts>().TropicsTiles > 50 && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
    }
}
