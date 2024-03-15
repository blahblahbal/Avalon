using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Depths;

namespace Avalon.Compatability.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvGlowingPotion : GlowingPotion
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Item.ResearchUnlockCount = 30;
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.buffTime = 8 * 3600;
        Item.buffType = ModContent.BuffType<Buffs.AdvGlowing>();
    }
}
