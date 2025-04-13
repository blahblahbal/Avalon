using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BandofSlime : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.width = 28;
        Item.accessory = true;
        Item.value = 50000;
        Item.height = 30;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.endurance += 0.05f;
        player.noFallDmg = true;
        player.slippy = true;
        player.slippy2 = true;
    }
}
