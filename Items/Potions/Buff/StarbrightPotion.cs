using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class StarbrightPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(14, 68, 255),
			new Color(23, 148, 242),
			new Color(24, 213, 241)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Starbright>(), TimeUtils.MinutesToTicks(5), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.Lens).AddIngredient(ItemID.Meteorite).AddTile(TileID.Bottles).Register();
	}
}
