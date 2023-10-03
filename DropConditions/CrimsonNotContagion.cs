using Avalon.Common;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions
{
    internal class CrimsonNotContagion : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return WorldGen.crimson && ModContent.GetInstance<AvalonWorld>().WorldEvil != WorldGeneration.Enums.WorldEvil.Contagion;
        }

        public bool CanShowItemDropInUI()
        {
            return WorldGen.crimson && ModContent.GetInstance<AvalonWorld>().WorldEvil != WorldGeneration.Enums.WorldEvil.Contagion;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
}
