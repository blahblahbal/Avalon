using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Head)]
class SpodermanMask : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.vanity = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
           .AddIngredient(ItemID.Silk, 10)
           .AddIngredient(ItemID.FireblossomSeeds, 3)
           .AddTile(TileID.Loom)
           .Register();
    }
}
