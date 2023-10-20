using Avalon.Common.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class SandstormCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneDesert && info.player.ZoneSandstorm && info.npc.value > 0 && !info.IsInSimulation;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "During Sandstorm";
    }
}
