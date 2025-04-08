using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class WaspFiber : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    //public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    //{
    //    itemGroup = Data.Sets.ItemGroupValues.Contagion;
    //}
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 750;
        Item.height = dims.Height;
    }
}
