using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class EarthenInsignia : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().EarthInsig = true;
    }
}
