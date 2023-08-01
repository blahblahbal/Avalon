using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class DurataniumChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Orange;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Material.Bars.DurataniumBar>())
    //        .AddIngredient(Type, 3)
    //        .AddTile(TileID.WorkBenches)
    //        .Register();
    //}
}
