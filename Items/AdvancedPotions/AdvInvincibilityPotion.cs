using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.AdvancedPotions;

class AdvInvincibilityPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
            Color.Indigo,
            Color.Purple
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvInvincibility>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 1200;
    }
}
