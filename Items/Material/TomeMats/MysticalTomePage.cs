using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

class MysticalTomePage : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = Data.Sets.ItemGroupValues.CraftedTomeMats;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.FallenStar)
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddRecipeGroup(RecipeGroupID.Wood, 3)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
