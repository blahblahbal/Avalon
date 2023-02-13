using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.PreHardmode;

class BlackWhetstone : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Black Whetstone");
        //Tooltip.SetDefault("Increases melee armor penetration by 10");
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.Size = new Vector2(16);
        Item.rare = ItemRarityID.Green;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 1);
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetArmorPenetration(DamageClass.Melee) += 10;
    }
}
