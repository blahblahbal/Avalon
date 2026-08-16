using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Data.Sets;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class CloverPotionActive : IItemDropRuleCondition, IProvideItemConditionDescription
{
	public bool CanDrop(DropAttemptInfo info)
	{
		if (info.npc.lastInteraction != -1)
		{
			return Main.player[info.npc.lastInteraction].GetModPlayer<AvalonPlayer>().Lucky;
		}
		return false;
	}

	public bool CanShowItemDropInUI()
	{
		return false;
	}

	public string GetConditionDescription()
	{
		return "Clover Potion active";
	}
}

file class CloverPotionGlobalNPC : GlobalNPC
{
	private static readonly HashSet<IItemDropRule> PreventDuplicates = [];
	public static void ModifyDrops(ILoot loot)
	{
		foreach (CommonDrop rule in loot.Get().FindDropRules<CommonDrop>())
		{
			if (!PreventDuplicates.Contains(rule))
			{
				if (ItemSets.ItemDropsAffectedByCloverPotion[rule.itemId])
				{
					IItemDropRule clover = ItemDropRule.ByCondition(new CloverPotionActive(), rule.itemId, rule.chanceDenominator, rule.amountDroppedMinimum, rule.amountDroppedMaximum, rule.chanceNumerator);
					rule.OnFailedRoll(clover);
					PreventDuplicates.Add(clover);
					PreventDuplicates.Add(rule);
				}
			}
		}
	}
	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
	{
		ModifyDrops(npcLoot);
	}
	public override void ModifyGlobalLoot(GlobalLoot globalLoot)
	{
		ModifyDrops(globalLoot);
	}
}