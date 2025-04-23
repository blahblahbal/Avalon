using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvObsidianSkinPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(58, 48, 102),
			new Color(90, 72, 168),
			new Color(132, 116, 199)
		];
	}
	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.AdvancedBuffs.AdvObsidianSkin>(), TimeUtils.MinutesToTicks(8), ClassExtensions.PotionCorkType.Elixir);
	}
}
