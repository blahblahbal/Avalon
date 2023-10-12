using Avalon.Common.Players;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;
public class UndergroundContagionCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneRockLayerHeight && (info.player.InModBiome<Biomes.UndergroundContagion>() || info.player.InModBiome<Biomes.ContagionCaveDesert>()) && !info.IsInSimulation && info.npc.value > 0;
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

