using Avalon.Biomes;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;
public class ZoneOutpost : IItemDropRuleCondition, IProvideItemConditionDescription
{
	public bool CanDrop(DropAttemptInfo info)
	{
		return info.player.InModBiome<TuhrtlOutpost>() && !info.IsInSimulation && info.npc.value > 0;
	}
	public bool CanShowItemDropInUI()
	{
		return false;
	}
	public string GetConditionDescription()
	{
		return "Drops in the Tuhrtl Outpost";
	}
}

