using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class ShockwavePotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(91, 54, 36),
			new Color(192, 128, 97),

			new Color(59, 114, 32)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Shockwave>(), TimeUtils.MinutesToTicks(7), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddIngredient(ItemID.Meteorite).AddIngredient(ItemID.Blinkroot).AddTile(TileID.Bottles).Register();
	}
}
