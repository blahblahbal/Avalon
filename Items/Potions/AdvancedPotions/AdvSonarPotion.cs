using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvSonarPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(217, 254, 161),
			new Color(69, 110, 9),
			new Color(135, 219, 11)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvSonar>(), TimeUtils.MinutesToTicks(16), ClassExtensions.PotionCorkType.Elixir);
	}
}
