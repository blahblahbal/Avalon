using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class BlahPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3]
        {
            Color.Orange,
            Color.LightGray,
            Color.Goldenrod
        };
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.BuffPotion;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Blah>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = false;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 1;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.buffTime = 5 * 60 * 60 * 60; // 5 hours
    }
}
