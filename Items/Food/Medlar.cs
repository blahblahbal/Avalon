using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food;

public class Medlar : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
		Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.FoodParticleColors[Item.type] =
		[
			new Color(211, 165, 95),
			new Color(153, 94, 48),
			new Color(89, 47, 24)
		];
		ItemID.Sets.IsFood[Type] = true;
	}

	public override void SetDefaults()
	{
		// DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
		Item.DefaultToFood(22, 18, BuffID.WellFed, TimeUtils.MinutesToTicks(5));
	}
}
