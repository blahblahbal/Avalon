using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class VisionPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(22, 90, 75),
			new Color(94, 163, 99),
			new Color(176, 247, 126)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Vision>(), TimeUtils.MinutesToTicks(4), ClassExtensions.PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
		   .AddIngredient(ModContent.ItemType<Material.BottledLava>())
		   .AddIngredient(ItemID.Ruby)
		   .AddIngredient(ItemID.Moonglow)
		   .AddTile(TileID.Bottles)
		   .Register();
	}
}
