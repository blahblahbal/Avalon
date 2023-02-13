using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Accessories.PreHardmode;

internal class BloodyWhetstone : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bloody Whetstone");
        //Tooltip.SetDefault("Melee attacks inflict bleeding");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new Vector2(16);
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 3);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffects>().BloodyWhetstone = true;
    }
}
