using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class LeapingPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(137, 93, 13),
			new Color(198, 137, 23),
			new Color(246, 172, 34)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Leaping>(), TimeUtils.MinutesToTicks(6), PotionCorkType.Obsidian);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Holybird>()).AddIngredient(ItemID.FallenStar).AddIngredient(ItemID.Vine).AddIngredient(ModContent.ItemType<Material.BottledLava>()).AddTile(TileID.Bottles).Register();
	//}
}
