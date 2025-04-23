using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvGillsPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(13, 74, 137),
			new Color(16, 89, 164),
			new Color(10, 119, 230)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvGills>(), TimeUtils.MinutesToTicks(4), ClassExtensions.PotionCorkType.Elixir);
	}
}
