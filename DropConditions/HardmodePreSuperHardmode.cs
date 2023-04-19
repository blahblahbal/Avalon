using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class HardmodePreSuperHardmode : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return CanShowItemDropInUI() && info.npc.value > 0 && !info.IsInSimulation && info.npc.value > 0;
    }

    public bool CanShowItemDropInUI()
    {
        return Main.hardMode; // && !ModContent.GetInstance<AvalonWorld>().SuperHardmode;
    }

    public string GetConditionDescription()
    {
        return null;
    }
}
