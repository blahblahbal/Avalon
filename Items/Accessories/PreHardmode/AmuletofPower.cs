using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

[AutoloadEquip(EquipType.Neck)]
public class AmuletofPower : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 90);
		Item.defense = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.07f;
		player.GetCritChance(DamageClass.Generic) += 5;
		player.statManaMax2 += 40;
		player.statLifeMax2 += 40;
		player.GetModPlayer<AvalonPlayer>().AllCritDamage(0.05f);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<AmethystAmulet>())
			.AddIngredient(ModContent.ItemType<TopazAmulet>())
			.AddIngredient(ModContent.ItemType<SapphireAmulet>())
			.AddIngredient(ModContent.ItemType<EmeraldAmulet>())
			.AddIngredient(ModContent.ItemType<RubyAmulet>())
			.AddIngredient(ModContent.ItemType<DiamondAmulet>())
			.AddIngredient(ModContent.ItemType<TourmalineAmulet>())
			.AddIngredient(ModContent.ItemType<PeridotAmulet>())
			.AddIngredient(ModContent.ItemType<ZirconAmulet>())
			.AddTile(TileID.Anvils)
			.Register();
	}
}
