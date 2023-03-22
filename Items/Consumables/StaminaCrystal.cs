using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class StaminaCrystal : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item29;
        Item.value = 95000;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        return player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax < 300;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax += 30;
        player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 30;
        player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 30;
        return true;
    }
}
