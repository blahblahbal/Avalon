using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class Windshield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 26;
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.value = 100000;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.WindPushed] = true;
    }
}
