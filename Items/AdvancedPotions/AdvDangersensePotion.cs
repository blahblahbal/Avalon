using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.AdvancedPotions;

class AdvDangersensePotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(251, 105, 29),
            new Color(220, 73, 4),
            new Color(140, 33, 10)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvDangersense>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 72000;
    }
}
