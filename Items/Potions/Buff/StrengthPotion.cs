using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class StrengthPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(37, 16, 164),
			new Color(89, 78, 244),
			new Color(175, 163, 255)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Strong>(), TimeUtils.MinutesToTicks(9), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.AdamantiteBar)
			.AddIngredient(ItemID.Diamond)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.TitaniumBar)
			.AddIngredient(ItemID.Diamond)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>())
			.AddIngredient(ItemID.Diamond)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
