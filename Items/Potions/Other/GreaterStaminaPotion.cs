using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

public class GreaterStaminaPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(29, 112, 37),
			new Color(30, 133, 39),
			new Color(98, 189, 106)
		];
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.StaminaPotions;
	}
	public override void SetDefaults()
	{
		Item.DefaultToStaminaPotion(95);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 10);
	}
	public override void AddRecipes()
	{
		CreateRecipe(10)
			.AddIngredient(ModContent.ItemType<StaminaPotion>(), 10)
			.AddIngredient(ItemID.Feather, 2)
			.AddIngredient(ItemID.SoulofFlight)
			.AddTile(TileID.Bottles)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2) return false;
		return true;
	}
	public override bool? UseItem(Player player)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 95;
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaHealEffect(95, true);
		player.AddBuff(ModContent.BuffType<Buffs.Debuffs.StaminaDrain>(), 60 * 9);
		if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam > player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().StatStam = player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2;
		}
		return true;
	}
}
