using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.PreHardmode;

class BismuthWatch : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GoldWatch);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Tables)
            .AddTile(TileID.Chairs).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.accWatch < 3) player.accWatch = 3;
    }

    public override void UpdateInventory(Player player)
    {
        if (player.accWatch < 3) player.accWatch = 3;
    }
}
