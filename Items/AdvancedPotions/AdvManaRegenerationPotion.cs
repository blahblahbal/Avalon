using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvManaRegenerationPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(137, 13, 86),
            new Color(230, 10, 139),
            new Color(255, 144, 210)
        };
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.BuffPotion;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvManaRegeneration>();
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
        Item.buffTime = 14400;
    }
}
