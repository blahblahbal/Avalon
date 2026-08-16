using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Common;
//MIT License

//Copyright(c) 2024
//Tyfyter

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
public static class DropRuleExt
{
	public static void Unload()
	{
		RuleChildFinders = null;
	}
	public delegate IEnumerable<IItemDropRule> RuleChildFinder(IItemDropRule rule);
	static Dictionary<Type, RuleChildFinder> _RuleChildFinders => new()
	{
		[typeof(AlwaysAtleastOneSuccessDropRule)] = r => ((AlwaysAtleastOneSuccessDropRule)r).rules,
		[typeof(DropBasedOnExpertMode)] = r => [((DropBasedOnExpertMode)r).ruleForNormalMode, ((DropBasedOnExpertMode)r).ruleForExpertMode],
		[typeof(DropBasedOnMasterAndExpertMode)] = r => [((DropBasedOnMasterAndExpertMode)r).ruleForDefault, ((DropBasedOnMasterAndExpertMode)r).ruleForExpertmode, ((DropBasedOnMasterAndExpertMode)r).ruleForMasterMode],
		[typeof(DropBasedOnMasterMode)] = r => [((DropBasedOnMasterMode)r).ruleForDefault, ((DropBasedOnMasterMode)r).ruleForMasterMode],
		[typeof(FewFromRulesRule)] = r => ((FewFromRulesRule)r).options,
		[typeof(OneFromRulesRule)] = r => ((OneFromRulesRule)r).options,
		[typeof(SequentialRulesNotScalingWithLuckRule)] = r => ((SequentialRulesNotScalingWithLuckRule)r).rules,
		[typeof(SequentialRulesRule)] = r => ((SequentialRulesRule)r).rules,
	};
	public static Dictionary<Type, RuleChildFinder> RuleChildFinders { get; private set; } = _RuleChildFinders;
	/// <summary>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dropRules"></param>
	/// <param name="predicate"></param>
	/// <returns>The first <see cref="IItemDropRule"/> matching <paramref name="predicate"/> in <paramref name="dropRules"/>, or null if no matching rule was found</returns>
	public static T FindDropRule<T>(this IEnumerable<IItemDropRule> dropRules, Predicate<T> predicate) where T : class, IItemDropRule
	{
		foreach (IItemDropRule dropRule in dropRules)
		{
			if (dropRule is T rule && predicate(rule)) return rule;
			if (dropRule.ChainedRules.Count != 0 && dropRule.ChainedRules.Select(c => c.RuleToChain).FindDropRule(predicate) is T foundRule) return foundRule;
			if (RuleChildFinders.TryGetValue(dropRule.GetType(), out RuleChildFinder ruleChildFinder) && ruleChildFinder(dropRule).FindDropRule(predicate) is T foundRule2) return foundRule2;
		}
		return null;
	}
	/// <returns>All <see cref="IItemDropRule"/>s in <paramref name="dropRules"/></returns>
	public static IEnumerable<T> FindDropRules<T>(this IEnumerable<IItemDropRule> dropRules) where T : class, IItemDropRule => dropRules.FindDropRules<T>(_ => true);
	/// <returns>All <see cref="IItemDropRule"/>s matching <paramref name="predicate"/> in <paramref name="dropRules"/></returns>
	public static IEnumerable<T> FindDropRules<T>(this IEnumerable<IItemDropRule> dropRules, Predicate<T> predicate) where T : class, IItemDropRule
	{
		foreach (IItemDropRule dropRule in dropRules)
		{
			if (dropRule is T rule && predicate(rule)) yield return rule;
			if (dropRule.ChainedRules.Count != 0)
			{
				foreach (T foundRule in dropRule.ChainedRules.Select(c => c.RuleToChain).FindDropRules(predicate))
				{
					yield return foundRule;
				}
			}
			if (RuleChildFinders.TryGetValue(dropRule.GetType(), out RuleChildFinder ruleChildFinder))
			{
				foreach (T foundRule in ruleChildFinder(dropRule).FindDropRules(predicate)) yield return foundRule;
			}
		}
	}
}