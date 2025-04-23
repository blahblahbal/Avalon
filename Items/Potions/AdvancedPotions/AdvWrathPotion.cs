using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvWrathPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(120, 36, 30),
			new Color(216, 73, 63),
			new Color(233, 125, 117)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvWrath>(), TimeUtils.MinutesToTicks(8), ClassExtensions.PotionCorkType.Elixir);
	}
}
