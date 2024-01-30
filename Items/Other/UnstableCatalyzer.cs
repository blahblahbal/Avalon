using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

internal class UnstableCatalyzer : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 50000;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddRecipeGroup(RecipeGroupID.Wood, 20)
            .AddRecipeGroup("Avalon:EvilBar", 5)
            .AddRecipeGroup(RecipeGroupID.IronBar, 15)
            .AddRecipeGroup("Avalon:WorkBenches")
            .AddTile(TileID.Anvils)
            .DisableDecraft()
            .Register();
    }
}
