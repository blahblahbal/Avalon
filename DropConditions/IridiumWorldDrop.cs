using Avalon.Common;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

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
