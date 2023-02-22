using ExxoAvalonOrigins.Common;
using Terraria.GameContent.ItemDropRules;

namespace ExxoAvalonOrigins.DropConditions;

public class IridiumWorldDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Iridium;
    }

    public bool CanShowItemDropInUI()
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Iridium;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
