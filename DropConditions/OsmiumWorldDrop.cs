using ExxoAvalonOrigins.Common;
using Terraria.GameContent.ItemDropRules;

namespace ExxoAvalonOrigins.DropConditions;

public class OsmiumWorldDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Osmium;
    }

    public bool CanShowItemDropInUI()
    {
        return AvalonWorld.RhodiumOre == AvalonWorld.RhodiumVariant.Osmium;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
