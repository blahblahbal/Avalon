using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class AFlowerlessPlant : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Tomes;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = 15000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = ItemID.None;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.02f;
        player.GetCritChance(DamageClass.Generic)++;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.Wood, 50)
            .AddIngredient(ItemID.FallenStar, 6)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
