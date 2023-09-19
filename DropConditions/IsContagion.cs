using Avalon.Common;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions
{
    internal class IsContagion : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return "The world evil is Contagion";
        }
    }
}
