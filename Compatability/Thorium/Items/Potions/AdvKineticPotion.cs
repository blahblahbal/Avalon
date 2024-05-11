using Avalon.Compatability.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;

namespace Avalon.Compatability.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvKineticPotion : KineticPotion
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Item.ResearchUnlockCount = 30;
        Data.Sets.Item.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<KineticPotionBuff>());
        Data.Sets.Item.PotionToElixirBuffID.Add(ModContent.ItemType<KineticPotion>(), ModContent.BuffType<AdvKinetic>());
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.buffTime = 5 * 3600;
        Item.buffType = ModContent.BuffType<AdvKinetic>();
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
}
