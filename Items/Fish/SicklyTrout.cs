using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Fish;

class SicklyTrout : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Fish;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.value = 10;
        Item.maxStack = 999;
        Item.height = dims.Height;
    }
}
