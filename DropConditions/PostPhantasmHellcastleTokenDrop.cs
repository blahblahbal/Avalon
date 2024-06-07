using Avalon.Common.Players;
using Avalon.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;

public class PostPhantasmHellcastleTokenDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.GetModPlayer<AvalonBiomePlayer>().ZoneHellcastle && NPC.downedMoonlord &&
               ModContent.GetInstance<DownedBossSystem>().DownedPhantasm && !info.IsInSimulation &&
               info.npc.value > 0;
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
