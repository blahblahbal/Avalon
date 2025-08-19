using Avalon.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class FlightCandleBiome : ModBiome
{
	public override SceneEffectPriority Priority => SceneEffectPriority.None;

	public override int Music => -1;
	public override string BestiaryIcon => base.BestiaryIcon;
	public override string BackgroundPath => base.BackgroundPath;
	public override string MapBackground => BackgroundPath;

	public override bool IsBiomeActive(Player player)
	{
		return ModContent.GetInstance<BiomeTileCounts>().FlightCandleNearby;
	}
}
