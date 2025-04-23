using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class ShadowPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(1, 1, 1),
			new Color(49, 49, 49),
			new Color(102, 102, 102)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Shadows>(), TimeUtils.MinutesToTicks(7));
	}
	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(ItemID.BottledWater, 5).AddIngredient(ModContent.ItemType<Material.ChaosDust>(), 9).AddIngredient(ItemID.Waterleaf, 3).AddIngredient(ItemID.Fireblossom, 3).AddTile(TileID.Bottles).Register();
	}
}
