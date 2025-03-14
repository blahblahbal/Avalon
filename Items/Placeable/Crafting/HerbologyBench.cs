using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

class HerbologyBench : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.HerbologyBench>();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 20);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddRecipeGroup(RecipeGroupID.IronBar, 8).AddRecipeGroup(RecipeGroupID.Wood, 45).AddIngredient(ItemID.GrassSeeds, 20).AddRecipeGroup("Herbs", 15).AddTile(TileID.Anvils).Register();
    }
}
