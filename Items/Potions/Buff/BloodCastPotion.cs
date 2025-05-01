using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class BloodCastPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(92, 16, 28),
			new Color(142, 37, 44),
			new Color(231, 122, 121)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.BloodCast>(), TimeUtils.MinutesToTicks(5), PotionCorkType.Obsidian);
	}
	public override void AddRecipes()
	{
		CreateRecipe(6)
			.AddIngredient(ItemID.LifeCrystal)
			.AddIngredient(ItemID.ManaCrystal)
			.AddIngredient(ModContent.ItemType<Material.BottledLava>())
			.AddIngredient(ItemID.Ectoplasm)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
