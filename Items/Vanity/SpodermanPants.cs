using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Vanity;

[AutoloadEquip(EquipType.Legs)]
class SpodermanPants : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.vanity = true;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
           .AddIngredient(ItemID.Silk, 15)
           .AddIngredient(ItemID.FireblossomSeeds, 2)
           .AddIngredient(ItemID.MushroomGrassSeeds, 1)
           .AddTile(TileID.Loom)
           .Register();
    }
}
