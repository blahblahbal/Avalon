using Avalon.Backgrounds;
using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class Contagion : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/ContagionWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int BiomeTorchItemType => ModContent.ItemType<Items.Placeable.Furniture.ContagionTorch>();
    public override int BiomeCampfireItemType => ModContent.ItemType<Items.Placeable.Furniture.ContagionCampfire>();
    public override int Music
    {
        get
        {
            return ExxoAvalonOrigins.MusicMod != null ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Contagion") : MusicID.Crimson;
        }
    }
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneDesert)
            {
                return ModContent.GetInstance<ContagionSurfaceDesertBackground>();
            }

            return ModContent.GetInstance<ContagionSurfaceBackground>();
        }
    }

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneSnow)
            {
                return ModContent.GetInstance<ContagionUndergroundSnowBackground>();
            }

            return ModContent.GetInstance<ContagionUndergroundBackground>();
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<BiomeTileCounts>().ContagionTiles > 350 && player.ZoneOverworldHeight;
        //return player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight;
    }
}


