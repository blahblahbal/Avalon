using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

internal class OilBottle : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.width = 20;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = 20;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().OilBottle = true;
    }
}
