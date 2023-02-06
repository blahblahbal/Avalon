using ExxoAvalonOrigins.Items.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Common;

public class AvalonGlobalItem : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if(item.type == ItemID.PlanteraBossBag)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LifeDew>(), 1, 10, 17));
            itemLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 70, 130));
        }
    }
    public override void SetDefaults(Item item)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            item.accessory = true;
        }
        switch (item.type)
        {
            case ItemID.MagicMirror:
            case ItemID.IceMirror:
            case ItemID.CellPhone:
            case ItemID.Shellphone:
            case ItemID.MagicConch:
            case ItemID.DemonConch:
            case ItemID.ShellphoneHell:
            case ItemID.ShellphoneOcean:
            case ItemID.ShellphoneSpawn:
                item.useTime = item.useAnimation = 30;
                break;
        }
    }
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.GetGlobalItem<AvalonGlobalItemInstance>().Tome && slot < 19 && !modded)
        {
            return false;
        }

        return !item.IsArmor() && base.CanEquipAccessory(item, player, slot, modded);
    }
}
