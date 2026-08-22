using System.Linq;
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
public record struct AlwaysAtleastOneSuccessDropRuleKind()
	: IDropRuleKind<AlwaysAtleastOneSuccessDropRule>, IDropOptionsKind
{
	static DropRuleDefinition IDropRuleKind<AlwaysAtleastOneSuccessDropRule>.Import(AlwaysAtleastOneSuccessDropRule rule) => new(new AlwaysAtleastOneSuccessDropRuleKind())
	{
		ChildRules = rule.rules.Select(DropRuleKindLoader.Import).ToArray()
	};
	readonly AlwaysAtleastOneSuccessDropRule IDropRuleKind<AlwaysAtleastOneSuccessDropRule>.Export(DropRuleDefinition definition) =>
		new(definition.ChildRules.Select(r => r.Export()).ToArray());
}
public record struct CommonDropKind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum)
	: IDropRuleKind<CommonDrop>, IDropChanceKind, IDropQuantityKind
{
	static DropRuleDefinition IDropRuleKind<CommonDrop>.Import(CommonDrop rule) => new(new CommonDropKind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum))
	{
		ItemIDs = [rule.itemId]
	};
	readonly CommonDrop IDropRuleKind<CommonDrop>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		AmountDroppedMinimum,
		AmountDroppedMaximum,
		ChanceNumerator
	);
}
public record struct CommonDropNotScalingWithLuckKind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum)
	: IDropRuleKind<CommonDropNotScalingWithLuck>, IDropChanceKind, IDropQuantityKind
{
	static DropRuleDefinition IDropRuleKind<CommonDropNotScalingWithLuck>.Import(CommonDropNotScalingWithLuck rule) => new(new CommonDropNotScalingWithLuckKind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum))
	{
		ItemIDs = [rule.itemId]
	};
	readonly CommonDropNotScalingWithLuck IDropRuleKind<CommonDropNotScalingWithLuck>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		ChanceNumerator,
		AmountDroppedMinimum,
		AmountDroppedMaximum
	);
}
public record struct CommonDropWithRerollsKind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum, int Rerolls)
	: IDropRuleKind<CommonDropWithRerolls>, IDropChanceKind, IDropQuantityKind
{
	static DropRuleDefinition IDropRuleKind<CommonDropWithRerolls>.Import(CommonDropWithRerolls rule) => new(new CommonDropWithRerollsKind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum, rule.timesToRoll - 1))
	{
		ItemIDs = [rule.itemId]
	};
	readonly CommonDropWithRerolls IDropRuleKind<CommonDropWithRerolls>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		AmountDroppedMinimum,
		AmountDroppedMaximum,
		Rerolls
	)
	{
		chanceNumerator = ChanceNumerator
	};
}
public record struct DropBasedOnExpertModeKind()
	: IDropRuleKind<DropBasedOnExpertMode>, IDropExactOptionsCoundKind
{
	readonly int IDropExactOptionsCoundKind.OptionsCount => 2;
	static DropRuleDefinition IDropRuleKind<DropBasedOnExpertMode>.Import(DropBasedOnExpertMode rule) => new(new DropBasedOnExpertModeKind())
	{
		ChildRules = [DropRuleKindLoader.Import(rule.ruleForNormalMode), DropRuleKindLoader.Import(rule.ruleForExpertMode)]
	};
	readonly DropBasedOnExpertMode IDropRuleKind<DropBasedOnExpertMode>.Export(DropRuleDefinition definition) =>
		new(definition.ChildRules[0].Export(), definition.ChildRules[1].Export());
}
public record struct DropBasedOnMasterModeKind()
	: IDropRuleKind<DropBasedOnMasterMode>, IDropExactOptionsCoundKind
{
	readonly int IDropExactOptionsCoundKind.OptionsCount => 2;
	static DropRuleDefinition IDropRuleKind<DropBasedOnMasterMode>.Import(DropBasedOnMasterMode rule) => new(new DropBasedOnMasterModeKind())
	{
		ChildRules = [DropRuleKindLoader.Import(rule.ruleForDefault), DropRuleKindLoader.Import(rule.ruleForMasterMode)]
	};
	readonly DropBasedOnMasterMode IDropRuleKind<DropBasedOnMasterMode>.Export(DropRuleDefinition definition) =>
		new(definition.ChildRules[0].Export(), definition.ChildRules[1].Export());
}
public record struct DropBasedOnMasterAndExpertModeKind()
	: IDropRuleKind<DropBasedOnMasterAndExpertMode>, IDropExactOptionsCoundKind
{
	readonly int IDropExactOptionsCoundKind.OptionsCount => 3;
	static DropRuleDefinition IDropRuleKind<DropBasedOnMasterAndExpertMode>.Import(DropBasedOnMasterAndExpertMode rule) => new(new DropBasedOnMasterAndExpertModeKind())
	{
		ChildRules = [DropRuleKindLoader.Import(rule.ruleForDefault), DropRuleKindLoader.Import(rule.ruleForExpertmode), DropRuleKindLoader.Import(rule.ruleForMasterMode)]
	};
	readonly DropBasedOnMasterAndExpertMode IDropRuleKind<DropBasedOnMasterAndExpertMode>.Export(DropRuleDefinition definition) =>
		new(definition.ChildRules[0].Export(), definition.ChildRules[1].Export(), definition.ChildRules[2].Export());
}
public record struct DropLocalPerClientAndResetsNPCMoneyTo0Kind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum, IItemDropRuleCondition Condition)
	: IDropRuleKind<DropLocalPerClientAndResetsNPCMoneyTo0>, IDropChanceKind, IDropQuantityKind, IDropConditionKind
{
	static DropRuleDefinition IDropRuleKind<DropLocalPerClientAndResetsNPCMoneyTo0>.Import(DropLocalPerClientAndResetsNPCMoneyTo0 rule) => new(new DropLocalPerClientAndResetsNPCMoneyTo0Kind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum, rule.condition))
	{
		ItemIDs = [rule.itemId]
	};
	readonly DropLocalPerClientAndResetsNPCMoneyTo0 IDropRuleKind<DropLocalPerClientAndResetsNPCMoneyTo0>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		AmountDroppedMinimum,
		AmountDroppedMaximum,
		Condition
	)
	{
		chanceNumerator = ChanceNumerator
	};
}
public record struct DropOneByOneKind(DropOneByOne.Parameters Parameters)
	: IDropRuleKind<DropOneByOne>
{
	static DropRuleDefinition IDropRuleKind<DropOneByOne>.Import(DropOneByOne rule) => new(new DropOneByOneKind(rule.parameters))
	{
		ItemIDs = [rule.itemId]
	};
	readonly DropOneByOne IDropRuleKind<DropOneByOne>.Export(DropRuleDefinition definition) => new(definition.ItemIDs[0], Parameters);
}
public record struct DropPerPlayerOnThePlayerKind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum, IItemDropRuleCondition Condition)
	: IDropRuleKind<DropPerPlayerOnThePlayer>, IDropChanceKind, IDropQuantityKind, IDropConditionKind
{
	static DropRuleDefinition IDropRuleKind<DropPerPlayerOnThePlayer>.Import(DropPerPlayerOnThePlayer rule) => new(new DropPerPlayerOnThePlayerKind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum, rule.condition))
	{
		ItemIDs = [rule.itemId]
	};
	readonly DropPerPlayerOnThePlayer IDropRuleKind<DropPerPlayerOnThePlayer>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		AmountDroppedMinimum,
		AmountDroppedMaximum,
		Condition
	)
	{
		chanceNumerator = ChanceNumerator
	};
}
public record struct FewFromOptionsDropRuleKind(int ChanceDenominator, int ChanceNumerator, int Amount)
	: IDropRuleKind<FewFromOptionsDropRule>, IDropOptionsKind, IDropChanceKind, IDropSingleQuantityKind
{
	static DropRuleDefinition IDropRuleKind<FewFromOptionsDropRule>.Import(FewFromOptionsDropRule rule) =>
		new(new FewFromOptionsDropRuleKind(rule.chanceDenominator, rule.chanceNumerator, rule.amount))
		{
			ItemIDs = rule.dropIds
		};
	readonly FewFromOptionsDropRule IDropRuleKind<FewFromOptionsDropRule>.Export(DropRuleDefinition definition) =>
		new(Amount, ChanceDenominator, ChanceNumerator, definition.ItemIDs);
}
public record struct FewFromOptionsNotScaledWithLuckDropRuleKind(int ChanceDenominator, int ChanceNumerator, int Amount)
	: IDropRuleKind<FewFromOptionsNotScaledWithLuckDropRule>, IDropOptionsKind, IDropChanceKind, IDropSingleQuantityKind
{
	static DropRuleDefinition IDropRuleKind<FewFromOptionsNotScaledWithLuckDropRule>.Import(FewFromOptionsNotScaledWithLuckDropRule rule) =>
		new(new FewFromOptionsNotScaledWithLuckDropRuleKind(rule.chanceDenominator, rule.chanceNumerator, rule.amount))
		{
			ItemIDs = rule.dropIds
		};
	readonly FewFromOptionsNotScaledWithLuckDropRule IDropRuleKind<FewFromOptionsNotScaledWithLuckDropRule>.Export(DropRuleDefinition definition) =>
		new(Amount, ChanceDenominator, ChanceNumerator, definition.ItemIDs);
}
public record struct FewFromRulesRuleKind(int ChanceDenominator, int Amount)
	: IDropRuleKind<FewFromRulesRule>, IChanceDenominatorKind, IDropSingleQuantityKind
{
	static DropRuleDefinition IDropRuleKind<FewFromRulesRule>.Import(FewFromRulesRule rule) =>
		new(new FewFromRulesRuleKind(rule.chanceDenominator, rule.amount))
		{
			ChildRules = DropRuleKindLoader.Import(rule.options)
		};
	readonly FewFromRulesRule IDropRuleKind<FewFromRulesRule>.Export(DropRuleDefinition definition) =>
		new(Amount, ChanceDenominator, definition.ChildRules.Export());
}
public record struct FromOptionsWithoutRepeatsDropRuleKind(int Amount)
	: IDropRuleKind<FromOptionsWithoutRepeatsDropRule>, IDropOptionsKind, IDropSingleQuantityKind
{
	static DropRuleDefinition IDropRuleKind<FromOptionsWithoutRepeatsDropRule>.Import(FromOptionsWithoutRepeatsDropRule rule) =>
		new(new FromOptionsWithoutRepeatsDropRuleKind(rule.dropCount))
		{
			ItemIDs = rule.dropIds
		};
	readonly FromOptionsWithoutRepeatsDropRule IDropRuleKind<FromOptionsWithoutRepeatsDropRule>.Export(DropRuleDefinition definition) =>
		new(Amount, definition.ItemIDs);
}
public record struct ItemDropWithConditionRuleKind(int ChanceDenominator, int ChanceNumerator, int AmountDroppedMinimum, int AmountDroppedMaximum, IItemDropRuleCondition Condition)
	: IDropRuleKind<ItemDropWithConditionRule>, IDropChanceKind, IDropQuantityKind, IDropConditionKind
{
	static DropRuleDefinition IDropRuleKind<ItemDropWithConditionRule>.Import(ItemDropWithConditionRule rule) => new(new ItemDropWithConditionRuleKind(rule.chanceDenominator, rule.chanceNumerator, rule.amountDroppedMinimum, rule.amountDroppedMaximum, rule.condition))
	{
		ItemIDs = [rule.itemId]
	};
	readonly ItemDropWithConditionRule IDropRuleKind<ItemDropWithConditionRule>.Export(DropRuleDefinition definition) => new(
		definition.ItemIDs[0],
		ChanceDenominator,
		AmountDroppedMinimum,
		AmountDroppedMaximum,
		Condition,
		ChanceNumerator
	);
}
public record struct LeadingConditionRuleKind(IItemDropRuleCondition Condition)
	: IDropRuleKind<LeadingConditionRule>, IDropConditionKind, IChainWrapperRuleKind
{

	static DropRuleDefinition IDropRuleKind<LeadingConditionRule>.Import(LeadingConditionRule rule) => new(new LeadingConditionRuleKind(rule.condition));
	readonly LeadingConditionRule IDropRuleKind<LeadingConditionRule>.Export(DropRuleDefinition definition) =>
		new(Condition);
}
public record struct OneFromOptionsDropRuleKind(int ChanceDenominator, int ChanceNumerator)
	: IDropRuleKind<OneFromOptionsDropRule>, IDropOptionsKind, IDropChanceKind
{

	static DropRuleDefinition IDropRuleKind<OneFromOptionsDropRule>.Import(OneFromOptionsDropRule rule) => new(new OneFromOptionsDropRuleKind(rule.chanceDenominator, rule.chanceNumerator))
	{
		ItemIDs = rule.dropIds
	};
	readonly OneFromOptionsDropRule IDropRuleKind<OneFromOptionsDropRule>.Export(DropRuleDefinition definition) =>
		new(ChanceDenominator, ChanceNumerator, definition.ItemIDs);
}
public record struct OneFromOptionsNotScaledWithLuckDropRuleKind(int ChanceDenominator, int ChanceNumerator)
	: IDropRuleKind<OneFromOptionsNotScaledWithLuckDropRule>, IDropOptionsKind, IDropChanceKind
{

	static DropRuleDefinition IDropRuleKind<OneFromOptionsNotScaledWithLuckDropRule>.Import(OneFromOptionsNotScaledWithLuckDropRule rule) => new(new OneFromOptionsNotScaledWithLuckDropRuleKind(rule.chanceDenominator, rule.chanceNumerator))
	{
		ItemIDs = rule.dropIds
	};
	readonly OneFromOptionsNotScaledWithLuckDropRule IDropRuleKind<OneFromOptionsNotScaledWithLuckDropRule>.Export(DropRuleDefinition definition) =>
		new(ChanceDenominator, ChanceNumerator, definition.ItemIDs);
}
public record struct OneFromRulesRuleKind(int ChanceDenominator, int ChanceNumerator)
	: IDropRuleKind<OneFromRulesRule>, IDropOptionsKind, IDropChanceKind
{

	static DropRuleDefinition IDropRuleKind<OneFromRulesRule>.Import(OneFromRulesRule rule) =>
		new(new OneFromRulesRuleKind(rule.chanceDenominator, rule.chanceNumerator))
		{
			ChildRules = DropRuleKindLoader.Import(rule.options)
		};
	readonly OneFromRulesRule IDropRuleKind<OneFromRulesRule>.Export(DropRuleDefinition definition) =>
		new(ChanceDenominator, ChanceNumerator, definition.ChildRules.Export());
}
public record struct SequentialRulesNotScalingWithLuckRuleKind(int ChanceDenominator, int ChanceNumerator)
	: IDropRuleKind<SequentialRulesNotScalingWithLuckRule>, IDropOptionsKind, IDropChanceKind
{

	static DropRuleDefinition IDropRuleKind<SequentialRulesNotScalingWithLuckRule>.Import(SequentialRulesNotScalingWithLuckRule rule) =>
		new(new SequentialRulesNotScalingWithLuckRuleKind(rule.chanceDenominator, rule.chanceNumerator))
		{
			ChildRules = DropRuleKindLoader.Import(rule.rules)
		};
	readonly SequentialRulesNotScalingWithLuckRule IDropRuleKind<SequentialRulesNotScalingWithLuckRule>.Export(DropRuleDefinition definition) =>
		new(ChanceDenominator, ChanceNumerator, definition.ChildRules.Export());
}
public record struct SequentialRulesRuleKind(int ChanceDenominator)
	: IDropRuleKind<SequentialRulesRule>, IDropOptionsKind, IChanceDenominatorKind
{

	static DropRuleDefinition IDropRuleKind<SequentialRulesRule>.Import(SequentialRulesRule rule) =>
		new(new SequentialRulesRuleKind(rule.chanceDenominator))
		{
			ChildRules = DropRuleKindLoader.Import(rule.rules)
		};
	readonly SequentialRulesRule IDropRuleKind<SequentialRulesRule>.Export(DropRuleDefinition definition) =>
		new(ChanceDenominator, definition.ChildRules.Export());
}