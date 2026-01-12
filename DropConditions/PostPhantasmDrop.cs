using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostPhantasmDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<SyncAvalonWorldData>().DownedPhantasm && !info.IsInSimulation && info.npc.value > 0;
    }

    public bool CanShowItemDropInUI()
    {
        return false;
    }

    public string GetConditionDescription()
    {
		return string.Empty;
	}
}
