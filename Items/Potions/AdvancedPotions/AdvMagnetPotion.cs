using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvMagnetPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(127, 127, 127),
			new Color(195, 195, 195),
			new Color(255, 255, 255),

			new Color(116, 188, 255),
			new Color(246, 108, 126)
		];
	}

	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvMagnet>(), TimeUtils.MinutesToTicks(8), PotionCorkType.Elixir);
	}
}
