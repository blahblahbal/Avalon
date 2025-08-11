using Avalon.Backgrounds;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class UndergroundContagion : ModBiome
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
	public override string BestiaryIcon => base.BestiaryIcon;
	public override string BackgroundPath => ModContent.GetInstance<Contagion>().BackgroundPath;
	public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/ContagionWaterStyle");
	public override string MapBackground => BackgroundPath;
	public override int BiomeTorchItemType => ModContent.ItemType<Items.Placeable.Furniture.ContagionTorch>();
	public override int BiomeCampfireItemType => ModContent.ItemType<Items.Placeable.Furniture.ContagionCampfire>();
	public override int Music
	{
		get
		{
			return ExxoAvalonOrigins.MusicMod != null ? Main.swapMusic ? MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagionEnnway") : MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagion") : MusicID.UndergroundCrimson;
		}
	}

	public override ModUndergroundBackgroundStyle? UndergroundBackgroundStyle
	{
		get
		{
			if (Main.screenPosition.Y / 16 < Main.UnderworldLayer - 196f && Main.screenPosition.Y / 16 > Main.rockLayer + 60)
			{
				if (Main.LocalPlayer.ZoneSnow)
				{
					return ModContent.GetInstance<ContagionUndergroundSnowBackground>();
				}

				return (Main.worldID % 2) switch
				{
					0 => ModContent.GetInstance<ContagionUndergroundBackground_0>(),
					1 => ModContent.GetInstance<ContagionUndergroundBackground_1>(),
					_ => throw new System.NotImplementedException()
				};
			}
			return default;
		}
	}

	public override bool IsBiomeActive(Player player)
	{
		return !player.ZoneDungeon && ModContent.GetInstance<Systems.BiomeTileCounts>().ContagionTiles > 200 && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
	}
}
