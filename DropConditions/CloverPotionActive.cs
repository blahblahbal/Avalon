using Avalon.Common.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.DropConditions;

public class CloverPotionActive : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        if (info.npc.lastInteraction != -1)
        {
            return Main.player[info.npc.lastInteraction].GetModPlayer<AvalonPlayer>().Lucky;
        }
        return false;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
