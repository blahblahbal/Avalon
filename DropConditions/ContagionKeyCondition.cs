using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.DropConditions
{
    public class ContagionKeyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (info.npc.value > 0f && Main.hardMode && !info.IsInSimulation)
            {
                return info.player.InModBiome<Biomes.Contagion>() || info.player.InModBiome<Biomes.UndergroundContagion>() ||
                    info.player.InModBiome<Biomes.ContagionCaveDesert>() || info.player.InModBiome<Biomes.ContagionDesert>();
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return "Underground Contagion";
        }
    }
}
