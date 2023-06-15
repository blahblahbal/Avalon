using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

class Gravel : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 9999;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 15)
            .AddIngredient(ItemID.SiltBlock, 20)
            .AddIngredient(ItemID.StoneBlock, 5)
            .AddTile(TileID.Anvils).Register();

        Recipe.Create(Type, 15)
            .AddIngredient(ItemID.SlushBlock, 20)
            .AddIngredient(ItemID.StoneBlock, 5)
            .AddTile(TileID.Anvils).Register();
    }
}
