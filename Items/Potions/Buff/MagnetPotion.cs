using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class MagnetPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(127, 127, 127),
			new Color(195, 195, 195),
			new Color(255, 255, 255),

			new Color(116, 188, 255),
			new Color(246, 108, 126)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Magnet>(), TimeUtils.MinutesToTicks(4), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ItemID.Ebonkoi)
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ItemID.Hemopiranha)
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ModContent.ItemType<Fish.SicklyTrout>())
			.AddIngredient(ItemID.Blinkroot)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
