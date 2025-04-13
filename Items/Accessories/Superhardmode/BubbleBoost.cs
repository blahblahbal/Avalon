using Avalon.Common.Players;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Superhardmode;

public class BubbleBoost : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.width = 32;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.height = 32;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().BubbleBoost = true;
        player.noFallDmg = true;
    }
}
