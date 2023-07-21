using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class FourLeafClover : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(5);
        Item.height = dims.Height;
    }
}
