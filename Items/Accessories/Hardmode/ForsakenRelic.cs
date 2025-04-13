using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

class ForsakenRelic : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = 22;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.height = 30;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.immune)
        {
            player.GetCritChance(DamageClass.Generic) += 7;
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }
    }
}
