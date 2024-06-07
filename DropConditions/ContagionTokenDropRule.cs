using Terraria.GameContent.ItemDropRules;
using Terraria;
using Avalon.Biomes;

namespace Avalon.DropConditions;

public class ContagionTokenDropRule : IItemDropRuleCondition, IProvideItemConditionDescription
{
	public bool CanDrop(DropAttemptInfo info)
	{
		return (info.player.InModBiome<Contagion>() || info.player.InModBiome<UndergroundContagion>()) && NPC.downedBoss1 && info.npc.value > 0 && !info.IsInSimulation;
	}
	public bool CanShowItemDropInUI()
	{
		return false;
	}
	public string GetConditionDescription()
	{
		return "Drops in the Contagion after the Eye of Cthulhu is defeated";
	}
}
