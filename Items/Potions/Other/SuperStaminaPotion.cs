using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

class SuperStaminaPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1] { Color.Green };
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.StaminaPotions;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.consumable = true;
		Item.rare = ItemRarityID.Yellow;
		Item.width = dims.Width;
		Item.useTurn = true;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 120;
		Item.maxStack = 9999;
		Item.value = 4000;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.UseSound = SoundID.Item3;
	}
	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ModContent.ItemType<GreaterStaminaPotion>(), 2)
			.AddIngredient(ItemID.ChlorophyteBar)
			.AddIngredient(ItemID.SharkFin, 2)
			.AddTile(TileID.Bottles)
			.Register();

		//CreateRecipe(2)
		//    .AddIngredient(ModContent.ItemType<GreaterStaminaPotion>(), 2)
		//    .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>())
		//    .AddIngredient(ItemID.SharkFin, 2)
		//    .AddTile(TileID.Bottles)
		//    .Register();
	}
	public override bool CanUseItem(Player player)
	{
		if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2) return false;
		return true;
	}
	public override bool? UseItem(Player player)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 120;
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaHealEffect(120, true);
		player.AddBuff(ModContent.BuffType<Buffs.Debuffs.StaminaDrain>(), 60 * 9);
		if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam > player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().StatStam = player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2;
		}
		return true;
	}
}
