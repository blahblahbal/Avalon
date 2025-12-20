using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace Avalon.DropConditions;

public class HoldingMedusaHead : IItemDropRuleCondition, IProvideItemConditionDescription
{
	public bool CanDrop(DropAttemptInfo info)
	{
		if (info.npc.lastInteraction != -1)
		{
			return Main.player[info.npc.lastInteraction].inventory[Main.player[info.npc.lastInteraction].selectedItem].type == ItemID.MedusaHead;
		}
		return false;
	}

	public bool CanShowItemDropInUI()
	{
		return false;
	}

	public string GetConditionDescription()
	{
		return "Holding Medusa Head";
	}
}
