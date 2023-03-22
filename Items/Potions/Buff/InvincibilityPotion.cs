using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class InvincibilityPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] { Color.Indigo, Color.Purple };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        //item.buffType = ModContent.BuffType<Buffs.Invincibility>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.EatFood;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 600;
        Item.UseSound = SoundID.Item3;
    }
}
