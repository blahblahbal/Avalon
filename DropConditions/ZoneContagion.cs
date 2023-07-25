using Avalon.Common.Players;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;
public class ZoneContagion : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneRockLayerHeight && info.player.GetModPlayer<AvalonBiomePlayer>().ZoneContagion && !info.IsInSimulation && info.npc.value > 0;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops in the Underground Contagion";
    }
}

