using Avalon.Biomes;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;
public class ZoneTropics : IItemDropRuleCondition, IProvideItemConditionDescription
{
	public bool CanDrop(DropAttemptInfo info)
	{
		return (info.player.InModBiome<Savanna>() || info.player.InModBiome<UndergroundTropics>()) && !info.IsInSimulation && info.npc.value > 0;
	}
	public bool CanShowItemDropInUI()
	{
		return false;
	}
	public string GetConditionDescription()
	{
		return "Drops in the Tropics";
	}
}

