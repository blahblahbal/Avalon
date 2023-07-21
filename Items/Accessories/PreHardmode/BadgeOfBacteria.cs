using Avalon.Common.Players;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BadgeOfBacteria : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("Increases damage by 8 after being hit\nAttackers also take damage for a short time after you are hit");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = 2;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
        Item.expert = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<AvalonPlayer>().BadgeOfBacteria = true;
    }
}
