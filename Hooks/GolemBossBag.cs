using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Players;
using Terraria.GameContent.ItemDropRules;
using System;
using Avalon.Items.Accessories.Hardmode;
using Avalon.Items.Weapons.Magic.Hardmode;

namespace Avalon.Hooks;

internal class GolemBossBag : ModHook
{
    protected override void Apply()
    {
        On_ItemDropDatabase.RegisterBossBags += On_ItemDropDatabase_RegisterBossBags;
    }

    private void On_ItemDropDatabase_RegisterBossBags(On_ItemDropDatabase.orig_RegisterBossBags orig, ItemDropDatabase self)
    {
        orig.Invoke(self);
        IItemDropRule itemDropRuleStynger = ItemDropRule.Common(ItemID.Stynger);
        itemDropRuleStynger.OnSuccess(ItemDropRule.Common(ItemID.StyngerBolt, 1, 60, 99), hideLootReport: true);

        self.RegisterToItem(ItemID.GolemBossBag, new OneFromRulesRule(1,
            //itemDropRuleStynger,
            ItemDropRule.Common(ItemID.PossessedHatchet),
            ItemDropRule.Common(ItemID.SunStone),
            ItemDropRule.Common(ItemID.EyeoftheGolem),
            ItemDropRule.Common(ItemID.HeatRay),
            ItemDropRule.Common(ItemID.StaffofEarth),
            ItemDropRule.Common(ItemID.GolemFist),
            ItemDropRule.Common(ModContent.ItemType<EarthenInsignia>()),
            ItemDropRule.Common(ModContent.ItemType<HeartoftheGolem>()),
            ItemDropRule.Common(ModContent.ItemType<Sunstorm>())));

        //self.RegisterToItem(ItemID.GolemBossBag, new OneFromRulesRule(1,
        //    itemDropRuleStynger,
        //    ItemDropRule.Common(ItemID.PossessedHatchet),
        //    ItemDropRule.Common(ItemID.SunStone),
        //    ItemDropRule.Common(ItemID.EyeoftheGolem),
        //    ItemDropRule.Common(ItemID.HeatRay),
        //    ItemDropRule.Common(ItemID.StaffofEarth),
        //    ItemDropRule.Common(ItemID.GolemFist),
        //    ItemDropRule.Common(ModContent.ItemType<EarthenInsignia>()),
        //    ItemDropRule.Common(ModContent.ItemType<HeartoftheGolem>()),
        //    ItemDropRule.Common(ModContent.ItemType<Sunstorm>())));
    }
}
