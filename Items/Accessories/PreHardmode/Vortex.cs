using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

class Vortex : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 24;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OnFire] = true;
    }
}
