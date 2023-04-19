using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class HardmodeOnly : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return CanShowItemDropInUI() && !info.IsInSimulation && info.npc.value > 0;
    }

    public bool CanShowItemDropInUI()
    {
        return Main.hardMode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
