using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class OilBottle : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.width = 22;
        Item.accessory = true;
        Item.value = 50000;
		Item.height = 28;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().OilBottle = true;
    }
}
