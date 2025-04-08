using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

class LesserStaminaPotion : ModItem
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
		Item.width = dims.Width;
		Item.useTurn = true;
		Item.useTime = 17;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 35;
		Item.maxStack = 9999;
		Item.value = 400;
		Item.useAnimation = 17;
		Item.height = dims.Height;
		Item.UseSound = SoundID.Item3;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ModContent.ItemType<Material.Ores.Boltstone>())
			.AddIngredient(ItemID.Cactus, 2)
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
		player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 35;
		player.GetModPlayer<AvalonStaminaPlayer>().StaminaHealEffect(35, true);
		player.AddBuff(ModContent.BuffType<Buffs.Debuffs.StaminaDrain>(), 60 * 9);
		if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam > player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().StatStam = player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2;
		}
		return true;
	}
}
