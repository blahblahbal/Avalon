using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class Invert : IItemDropRuleCondition, IProvideItemConditionDescription
{
    private readonly IItemDropRuleCondition condition;
    private readonly string description;

    public Invert(IItemDropRuleCondition condition, string description = null)
    {
        this.condition = condition;
        this.description = description;
    }

    public bool CanDrop(DropAttemptInfo info)
    {
        return !condition.CanDrop(info);
    }

    public bool CanShowItemDropInUI()
    {
        return !condition.CanShowItemDropInUI();
    }

    public string GetConditionDescription()
    {
        return description;
    }
}
