using Avalon.Common;
using Avalon.ModSupport.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.Depths;

namespace Avalon.ModSupport.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvGlowingPotion : GlowingPotion
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Item.ResearchUnlockCount = 30;
		Data.Sets.ItemSets.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<GlowingPotionBuff>());
		Data.Sets.ItemSets.PotionToElixirBuffID.Add(ModContent.ItemType<GlowingPotion>(), ModContent.BuffType<AdvGlowing>());
	}
	public override void AddRecipes()
	{
	}
	public override void SetDefaults()
	{
		base.SetDefaults();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 0, 4, 0);
		Item.useTime = 15;
		Item.useAnimation = 15;
		Item.buffTime = TimeUtils.MinutesToTicks(8);
		Item.buffType = ModContent.BuffType<AdvGlowing>();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
