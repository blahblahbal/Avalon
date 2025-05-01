using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class TimeShiftPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(78, 42, 35),
			new Color(158, 113, 86),
			new Color(234, 199, 138)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.TimeShift>(), TimeUtils.MinutesToTicks(9));
	}
	public override void AddRecipes()
	{
		CreateRecipe(5)
			.AddIngredient(ItemID.BottledWater)
			.AddIngredient(ItemID.BottledHoney, 5)
			.AddIngredient(ItemID.Feather, 8)
			.AddIngredient(ItemID.FastClock)
			.AddTile(TileID.Bottles)
			.Register();
	}
}
