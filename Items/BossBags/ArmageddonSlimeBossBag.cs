using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.BossBags;

public class ArmageddonSlimeBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.BossBag[Type] = true;
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        Item.ResearchUnlockCount = 3;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.EaterOfWorldsBossBag);
    }
    public override bool CanRightClick()
    {
        return true;
    }
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatterSoilBlock>(), 1, 100, 211));
    }
}
