using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Waist)]
class ZincWatch : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 30;
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 13);
        Item.accessory = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 10)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Tables)
            .AddTile(TileID.Chairs)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.accWatch < 2) player.accWatch = 2;
    }

    public override void UpdateInventory(Player player)
    {
        if (player.accWatch < 2) player.accWatch = 2;
    }
}
