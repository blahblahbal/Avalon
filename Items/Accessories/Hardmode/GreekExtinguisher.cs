using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class GreekExtinguisher : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 38;
        Item.rare = ItemRarityID.LightPurple;
        Item.accessory = true;
        Item.value = 100000;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.CursedInferno] = true;
    }
}
