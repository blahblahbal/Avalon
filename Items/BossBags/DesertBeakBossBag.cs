using Avalon.Items.Material;
using Avalon.Items.Vanity;
using Avalon.Items.Weapons.Magic.PreHardmode;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class DesertBeakBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.BossBag[Type] = true;
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.width = 36;
        Item.height = 34;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 22, 55));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertFeather>(), 1, 18, 24));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TomeoftheDistantPast>(), 3));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EggCannon>(), 3));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertBeakMask>(), 7));

        itemLoot.Add(ItemDropRule.ByCondition(new DropConditions.RhodiumWorldDrop(), ModContent.ItemType<Material.Ores.RhodiumOre>(), 2, 40, 61));
        itemLoot.Add(ItemDropRule.ByCondition(new DropConditions.OsmiumWorldDrop(), ModContent.ItemType<Material.Ores.OsmiumOre>(), 2, 40, 61));
        itemLoot.Add(ItemDropRule.ByCondition(new DropConditions.IridiumWorldDrop(), ModContent.ItemType<Material.Ores.IridiumOre>(), 2, 40, 61));
        // ore drop
    }
}
