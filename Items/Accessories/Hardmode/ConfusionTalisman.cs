using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ConfusionTalisman : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = 32;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = 24;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().ConfusionTal = true;
    }
}
