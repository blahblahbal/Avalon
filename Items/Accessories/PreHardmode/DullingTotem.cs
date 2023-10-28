using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class DullingTotem : ModItem
{
    public override void SetDefaults()
    {
        Item.lifeRegen = 2;
        Item.defense = 10;
        Item.rare = ItemRarityID.LightRed;
        Item.width = 20;
        Item.value = Item.sellPrice(0, 1, 63, 0);
        Item.accessory = true;
        Item.height = 26;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Shackle, 6)
            .AddIngredient(ItemID.BandofRegeneration)
            .AddTile(TileID.Anvils).Register();
    }
}
