using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

internal class UnderworldKey : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.TempleKey)
    //        .AddIngredient(ModContent.ItemType<UnderworldKeyMold>())
    //        .AddIngredient(ItemID.SoulofFright, 5)
    //        .AddIngredient(ItemID.SoulofMight, 5)
    //        .AddIngredient(ItemID.SoulofSight, 5)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
