using Avalon.Common;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Hooks
{
    internal class DebuffImmunityDrops : ModHook
    {
        protected override void Apply()
        {
            On_ItemDropDatabase.RegisterStatusImmunityItems += On_ItemDropDatabase_RegisterStatusImmunityItems;
        }

        private void On_ItemDropDatabase_RegisterStatusImmunityItems(On_ItemDropDatabase.orig_RegisterStatusImmunityItems orig, ItemDropDatabase self)
        {
            // call this method and do nothing, to remove the original drops; since Avalon modifies the rates with Clover Potion active,
            // this needs to be removed. I couldn't figure out a better way, so this is how it'll work for now.
        }
    }
}
