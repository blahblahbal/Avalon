using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ChaosCrystal : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Lime;
        Item.width = 26;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3, 0, 0);
		Item.height = 32;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.35f);
        player.GetModPlayer<AvalonPlayer>().AllMaxCrit(10);
    }
}
