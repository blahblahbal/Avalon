using System.Linq;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class UndergroundHardmodeContagionTokenDrop : IItemDropRuleCondition
{
    private readonly IItemDropRuleCondition[] conditions;

    public UndergroundHardmodeContagionTokenDrop(params IItemDropRuleCondition[] rules)
    {
        conditions = rules;
    }

    public bool CanDrop(DropAttemptInfo info) => conditions.All(val => val.CanDrop(info));

    public bool CanShowItemDropInUI() => false;

    public string? GetConditionDescription() => null;
}
