using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

class PathogenCampfire : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.useStyle = 1;
        Item.useTurn = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.autoReuse = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = (ModContent.TileType<Tiles.Furniture.PathogenCampfire>());
        Item.width = 12;
        Item.height = 12;
    }
    public override void AddRecipes()
    {
        CreateRecipe(33).AddRecipeGroup("Wood", 10).AddIngredient(ModContent.ItemType<PathogenTorch>(), 5).Register();
    }
}
