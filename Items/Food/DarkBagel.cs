using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Food
{
	public class DarkBagel : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 5;
			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
			ItemID.Sets.FoodParticleColors[Item.type] =
			[
				new Color(165, 125, 212),
				new Color(145, 74, 227),
				new Color(81, 42, 126)
			];
			ItemID.Sets.IsFood[Type] = true;
		}

		public override void SetDefaults()
		{
			// DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
			Item.DefaultToFood(22, 18, BuffID.WellFed2, TimeUtils.MinutesToTicks(10));
			Item.value = Item.sellPrice(0, 0, 40);
		}
	}
}
