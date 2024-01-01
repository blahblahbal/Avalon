using Avalon.Common;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class IridiumWorldDropAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Iridium && !Main.expertMode;
    }

    public bool CanShowItemDropInUI()
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Iridium && !Main.expertMode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
