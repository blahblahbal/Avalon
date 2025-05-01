using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class CloverPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(68, 84, 16),
			new Color(109, 157, 32),
			new Color(203, 229, 87)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Clover>(), TimeUtils.MinutesToTicks(30), PotionCorkType.Obsidian);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.FakeFourLeafClover>())
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Holybird>())
			.AddIngredient(ItemID.Fireblossom)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(20)
			.AddIngredient(ModContent.ItemType<Material.FourLeafClover>())
			.AddIngredient(ModContent.ItemType<Material.BottledLava>(), 20)
			.AddIngredient(ModContent.ItemType<Holybird>(), 20)
			.AddIngredient(ItemID.Fireblossom, 20)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
