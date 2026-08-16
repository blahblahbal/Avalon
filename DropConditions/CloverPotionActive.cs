using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Data.Sets;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
file class CloverPotionModHook : ModHook
{
	protected override void Apply()
	{
		MonoModHooks.Add(((Action<NPC, NPCLoot>)NPCLoader.ModifyNPCLoot).Method, ModifyNPCLoot_Detour);
		MonoModHooks.Add(((Action<GlobalLoot>)NPCLoader.ModifyGlobalLoot).Method, ModifyGlobalLoot_Detour);
	}
	private static void ModifyNPCLoot_Detour(Action<NPC, NPCLoot> orig, NPC npc, NPCLoot npcLoot)
	{
		orig.Invoke(npc, npcLoot);
		ModifyDrops(npcLoot);
	}
	private static void ModifyGlobalLoot_Detour(Action<GlobalLoot> orig, GlobalLoot globalLoot)
	{
		orig.Invoke(globalLoot);
		ModifyDrops(globalLoot);
	}
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
}