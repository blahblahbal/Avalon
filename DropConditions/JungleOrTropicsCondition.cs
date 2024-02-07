using Avalon.Common.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class JungleOrTropicsCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        if (info.npc.value > 0f && info.npc.lifeMax > 5 && !info.IsInSimulation && !info.npc.boss)
            return info.player.ZoneJungle;
        return false;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
        return "In the Jungle";
    }
}
