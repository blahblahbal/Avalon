using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class WisdomPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(66, 32, 46),
			new Color(125, 49, 78),
			new Color(192, 97, 132)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Wisdom>(), TimeUtils.MinutesToTicks(4), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
			.AddIngredient(ItemID.Waterleaf)
			.AddIngredient(ItemID.Moonglow)
			.AddIngredient(ItemID.FallenStar, 2)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
