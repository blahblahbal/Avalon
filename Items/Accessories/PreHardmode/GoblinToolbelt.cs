using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class GoblinToolbelt : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
		Item.width = 36;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5);
        Item.height = 22;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.blockRange += 2;
        player.accWatch = 3;
        player.accCompass = 1;
        player.accDepthMeter = 1;
        player.GetModPlayer<AvalonPlayer>().GoblinToolbelt = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Toolbelt)
            .AddIngredient(ItemID.GPS)
            .AddIngredient(ItemID.TinkerersWorkshop)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.accWatch = 3;
        player.accCompass = 1;
        player.accDepthMeter = 1;
    }
}
