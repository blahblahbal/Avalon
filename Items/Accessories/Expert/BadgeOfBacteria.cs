using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Expert;

public class BadgeOfBacteria : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = 2;
        Item.width = 22;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = 30;
        Item.expert = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().BadgeOfBacteria = true;
    }
}
