using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Avalon.Items.Material;

namespace Avalon.Items.BossBags;

public class KingStingBossBag : ModItem
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
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaspFiber>(), 1, 16, 27)); // wasp fiber
        itemLoot.Add(ItemDropRule.Common(ItemID.LesserHealingPotion, 1, 5, 16)); // healing potions
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vanity.KingStingMask>(), 7));
        // other loot comparable to queen bee
    }
}
