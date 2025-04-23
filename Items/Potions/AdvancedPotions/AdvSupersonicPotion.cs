using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvSupersonicPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(82, 83, 97),
			new Color(121, 123, 142),
			new Color(186, 189, 218)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvSupersonic>(), TimeUtils.MinutesToTicks(12), ClassExtensions.PotionCorkType.Elixir);
	}
}
