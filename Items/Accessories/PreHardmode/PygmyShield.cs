using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class PygmyShield : ModItem
{
    public override void SetDefaults()
    {
        Item.defense = 3;
        Item.rare = ItemRarityID.Green;
        Item.width = 20;
        Item.value = Item.sellPrice(0, 0, 75);
        Item.accessory = true;
        Item.height = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.CobaltShield)
            .AddIngredient(ItemID.PygmyNecklace)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxMinions++;
        player.noKnockback = true;
    }
}
