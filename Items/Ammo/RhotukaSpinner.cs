using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class RhotukaSpinner : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Ammo;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 0, 8);
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(5).AddIngredient(ItemID.WoodenArrow, 5).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Anvils).Register();
    //}
}
