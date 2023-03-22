using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class BismuthAxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 24;
        Item.UseSound = SoundID.Item1;
        Item.damage = 9;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1;
        Item.axe = 13;
        Item.useTime = 17;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 11000;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 25;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 9).AddRecipeGroup(RecipeGroupID.Wood, 3).AddTile(TileID.Anvils).Register();
    }
}
