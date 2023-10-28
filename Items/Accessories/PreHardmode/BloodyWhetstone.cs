using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

internal class BloodyWhetstone : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new Vector2(16);
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().BloodyWhetstone = true;
    }
}
