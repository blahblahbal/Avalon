using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Shards;

class CorruptShard : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.Size = new(20);
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
    }
}
