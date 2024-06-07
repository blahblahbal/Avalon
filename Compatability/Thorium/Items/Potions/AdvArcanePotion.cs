using Avalon.Compatability.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.Depths;

namespace Avalon.Compatability.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvArcanePotion : ArcanePotion
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return false; // ModLoader.HasMod("ThoriumMod");
	}
	public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        Item.ResearchUnlockCount = 30;
        Data.Sets.Item.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<ArcanePotionBuff>());
        Data.Sets.Item.PotionToElixirBuffID.Add(ModContent.ItemType<ArcanePotion>(), ModContent.BuffType<AdvArcane>());
    }

    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.buffTime = 8 * 3600;
        Item.buffType = ModContent.BuffType<AdvArcane>();
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
}
