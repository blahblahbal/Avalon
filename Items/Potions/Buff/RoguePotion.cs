using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class RoguePotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(99, 11, 9),
			new Color(184, 16, 12),
			new Color(255, 99, 95)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Rogue>(), TimeUtils.TimeToTicks(0, 0, 4, 30), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.Blinkroot)
			.AddIngredient(ItemID.SpecularFish)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
