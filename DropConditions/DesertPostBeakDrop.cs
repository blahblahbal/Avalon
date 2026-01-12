using Avalon.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class DesertPostBeakDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        if (info.npc.lastInteraction != -1)
        {
            return ModContent.GetInstance<SyncAvalonWorldData>().DownedDesertBeak && Main.player[info.npc.lastInteraction].ZoneUndergroundDesert && !info.IsInSimulation && info.npc.value > 0;
        }
        return false;
    }

    public bool CanShowItemDropInUI()
    {
        return ModContent.GetInstance<SyncAvalonWorldData>().DownedDesertBeak;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
