using Avalon.Backgrounds;
using Avalon.Common;
using Avalon.Systems;
using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class ContagionHousingHook : ModHook
{
	protected override void Apply()
	{
		On_WorldGen.GetTileTypeCountByCategory += On_WorldGen_GetTileTypeCountByCategory;
	}

	private int On_WorldGen_GetTileTypeCountByCategory(On_WorldGen.orig_GetTileTypeCountByCategory orig, int[] tileTypeCounts, Terraria.Enums.TileScanGroup group)
	{
		int t = orig.Invoke(tileTypeCounts, group);
		switch (group)
		{
			case Terraria.Enums.TileScanGroup.Corruption:
				return t + tileTypeCounts[ModContent.TileType<ContagionShortGrass>()] +
					tileTypeCounts[ModContent.TileType<Ickgrass>()] + tileTypeCounts[ModContent.TileType<Chunkstone>()] +
					tileTypeCounts[ModContent.TileType<Snotsand>()] + tileTypeCounts[ModContent.TileType<Snotsandstone>()] +
					tileTypeCounts[ModContent.TileType<HardenedSnotsand>()] + tileTypeCounts[ModContent.TileType<ContagionThornyBushes>()] +
					tileTypeCounts[ModContent.TileType<YellowIce>()] + -5 * tileTypeCounts[TileID.Sunflower];
			default: return t;
		}
	}
}

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

    //public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    //{
    //    get
    //    {
    //        if (Main.LocalPlayer.ZoneSnow)
    //        {
    //            return ModContent.GetInstance<ContagionUndergroundSnowBackground>();
    //        }

    //        return ModContent.GetInstance<ContagionUndergroundBackground>();
    //    }
    //}

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<BiomeTileCounts>().ContagionTiles >= 300 && player.ZoneOverworldHeight;
        //return player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight;
    }
}


