using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class BismuthHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.damage = 11;
        Item.autoReuse = true;
        Item.hammer = 61;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.useTime = 18;
        Item.knockBack = 4.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 11000;
        Item.useAnimation = 28;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 8).AddRecipeGroup(RecipeGroupID.Wood, 3).AddTile(TileID.Anvils).Register();
    }
}
