using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class Catalyzer : ModItem
{
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

    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddRecipeGroup(RecipeGroupID.Wood, 20)
    //        .AddRecipeGroup("Avalon:EvilBar", 5)
    //        .AddRecipeGroup(RecipeGroupID.IronBar, 15)
    //        .AddRecipeGroup("Avalon:WorkBenches")
    //        .AddCondition(Condition.NearShimmer)
    //        .AddTile(TileID.Anvils)
    //        .Register();
    //}
}
