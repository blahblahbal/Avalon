using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

internal class Catalyzer : ModItem
{
    public static readonly Condition NearShimmer = new("Conditions.NearShimmer", () => Main.LocalPlayer.GetModPlayer<AvalonPlayer>().AdjShimmer);

    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Catalyzer>();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddRecipeGroup(RecipeGroupID.Wood, 20)
            .AddRecipeGroup("ExxoAvalonOrigins:EvilBar", 5)
            .AddRecipeGroup(RecipeGroupID.IronBar, 15)
            .AddRecipeGroup("ExxoAvalonOrigins:WorkBenches")
            .AddCondition(NearShimmer)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
