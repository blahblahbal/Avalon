using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Systems.DropRule;
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
public class CopyNPCDropRule(int type) : IItemDropRule, IDropRuleKind<CopyNPCDropRule>
{
	static readonly RecursionCheckedSet<int> recursionBlocker = new();
	private readonly int type = type;

	public List<IItemDropRuleChainAttempt> ChainedRules { get; } = [];
	public bool CanDrop(DropAttemptInfo info) => true;
	public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
	{
		using IDisposable recursionBlock = recursionBlocker.TryAdd(type);
		if (recursionBlock is null) return;
		foreach (IItemDropRule rule in Main.ItemDropsDB.GetRulesForNPCID(type, false)) rule.ReportDroprates(drops, ratesInfo);
	}

	public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
	{
		using IDisposable recursionBlock = recursionBlocker.TryAdd(type);
		if (recursionBlock is null) return new ItemDropAttemptResult()
		{
			State = ItemDropAttemptResultState.DidNotRunCode
		};
		foreach (IItemDropRule rule in Main.ItemDropsDB.GetRulesForNPCID(type, false)) ItemDropping.ResolveRule(rule, info);
		return new ItemDropAttemptResult()
		{
			State = ItemDropAttemptResultState.Success
		};
	}
	static DropRuleDefinition IDropRuleKind<CopyNPCDropRule>.Import(CopyNPCDropRule rule) => new(new CopyNPCDropRule(rule.type));
	CopyNPCDropRule IDropRuleKind<CopyNPCDropRule>.Export(DropRuleDefinition definition) => new(type);
}