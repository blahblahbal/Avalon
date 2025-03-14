using Avalon.Common;
using Avalon.ModSupport.Thorium.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.Depths;

namespace Avalon.ModSupport.Thorium.Items.Potions;

[ExtendsFromMod("ThoriumMod")]
class AdvConflagrationPotion : ConflagrationPotion
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Item.ResearchUnlockCount = 30;
		Data.Sets.Item.ElixirToPotionBuffID.Add(Type, ModContent.BuffType<ConflagrationPotionBuff>());
		Data.Sets.Item.PotionToElixirBuffID.Add(ModContent.ItemType<ConflagrationPotion>(), ModContent.BuffType<AdvConflagration>());
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
		Item.buffTime = TimeUtils.MinutesToTicks(1);
		Item.buffType = ModContent.BuffType<AdvConflagration>();
		Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
	}
}
