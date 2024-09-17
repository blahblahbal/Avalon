using Terraria.GameContent.ItemDropRules;
using Terraria;

namespace Avalon.DropConditions
{
	public class UnderworldKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.npc.value > 0f && Main.hardMode && !info.IsInSimulation)
			{
				return info.player.ZoneUnderworldHeight;
			}
			return false;
		}

		public bool CanShowItemDropInUI()
		{
			return false;
		}

		public string GetConditionDescription()
		{
			return "Underworld";
		}
	}
}
