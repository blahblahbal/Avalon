using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class Grindstone : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 30;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.Grindstone>();
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.useAnimation = 15;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Extractinator)
            .AddRecipeGroup("GoldBar", 10)
            .AddRecipeGroup(RecipeGroupID.Wood, 15)
            .AddIngredient(ItemID.Chain, 2)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
