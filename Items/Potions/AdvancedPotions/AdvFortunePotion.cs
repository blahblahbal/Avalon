using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvFortunePotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(29, 88, 50),
			new Color(38, 144, 74),
			new Color(116, 192, 142)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Dupe>(), TimeUtils.MinutesToTicks(10));
	}
}
