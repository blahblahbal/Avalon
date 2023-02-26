using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.AdvancedPotions;

class AdvAmmoReservationPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(166, 166, 166),
            new Color(255, 186, 0),
            new Color(165, 58, 0)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvAmmoReservation>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;

        Item.buffTime = 50400;
    }
}
