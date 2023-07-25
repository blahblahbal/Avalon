using Avalon.Common;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Avalon.Items.Material.Herbs;

namespace Avalon.Hooks
{
    internal class HerbBagHook : ModHook
    {
        protected override void Apply()
        {
            On_ItemDropDatabase.RegisterHerbBag += OnRegisterHerbBag;
        }

        private void OnRegisterHerbBag(On_ItemDropDatabase.orig_RegisterHerbBag orig, ItemDropDatabase self)
        {
            self.RegisterToItem(3093, new HerbBagDropsItemDropRule(313, 314, 315, 317, 316, 318, 2358, 307, 308, 309, 311, 310, 312, 2357,
                ModContent.ItemType<Barfbush>(), ModContent.ItemType<BarfbushSeeds>(), ModContent.ItemType<Bloodberry>(), ModContent.ItemType<BloodberrySeeds>(),
                ModContent.ItemType<Holybird>(), ModContent.ItemType<HolybirdSeeds>(), ModContent.ItemType<Sweetstem>(), ModContent.ItemType<SweetstemSeeds>()));
        }
    }
}
