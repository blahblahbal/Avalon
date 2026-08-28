using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Data.Sets;
using Avalon.Systems.DropRule;
using System;
using System.Collections.Generic;
using System.Linq;
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
file class CloverPotionModHook : ModHook
{
	/// <summary>
	/// Add any drop rules that should be affected which are below a 1/100 drop chance.<br></br><br></br>
	/// Only works for vanilla drop rules.
	/// </summary>
	//public static readonly HashSet<IItemDropRule> ItemDropRulesAffectedByCloverPotion = [];
	/// <summary>
	/// Drop rules that should not be affected by the potion. Cannot currently add to it manually cause I haven't written the logic to get the root rules yet.<br></br><br></br>
	/// Gets populated automatically by rules that have already been altered to prevent recursion.
	/// </summary>
	private static readonly HashSet<IItemDropRule> Excluded_ItemDropRulesAffectedByCloverPotion = [];
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
	private static void ModifyDrops(ILoot loot)
	{
		foreach (IItemDropRule rule in loot.Get(false).FindDropRules<IItemDropRule>(static rule => !Excluded_ItemDropRulesAffectedByCloverPotion.Contains(rule) && DropRuleDefinition.ImportForCheck(rule) != null))
		{
			DropRuleDefinition drd = DropRuleDefinition.Import(rule);
			if (drd.kind is IChanceDenominatorKind data)
			{
				if ((float)data.ChanceNumerator / data.ChanceDenominator <= 0.01f || (drd.ItemIDs?.Any(x => ItemSets.ItemIDsAffectedByCloverPotion[x]) ?? false))
				{
					data.ChanceDenominator -= data.ChanceNumerator * (1 + (drd.kind is CommonDropWithRerollsKind x ? x.Rerolls : 0));
					IItemDropRule newRule = drd.Export();
					rule.OnFailedRoll(new LeadingConditionRule(new CloverPotionActive())).OnSuccess(newRule);
					Excluded_ItemDropRulesAffectedByCloverPotion.Add(newRule);
					Excluded_ItemDropRulesAffectedByCloverPotion.Add(rule);
				}
			}
		}
	}
}