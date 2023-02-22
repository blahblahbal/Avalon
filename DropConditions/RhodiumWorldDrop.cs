using ExxoAvalonOrigins.Common;
using Terraria.GameContent.ItemDropRules;

namespace ExxoAvalonOrigins.DropConditions;

public class RhodiumWorldDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Rhodium;
    }

    public bool CanShowItemDropInUI()
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Rhodium;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
