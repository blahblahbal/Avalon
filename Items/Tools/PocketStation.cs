using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class PocketStation : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 1, 0, 0);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddRecipeGroup("Avalon:WorkBenches")
            .AddIngredient(ItemID.MeteoriteBar, 10)
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().PocketBench = true;
    }
}
