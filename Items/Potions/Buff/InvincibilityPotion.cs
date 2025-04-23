using Avalon.Common.Players;
using Avalon.Items.Accessories.Hardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class InvincibilityPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[Type] = [
			new Color(122, 15, 250),
			new Color(146, 60, 250),
			new Color(177, 120, 250)
		];
	}

	public override void SetDefaults()
	{
		//item.buffType = ModContent.BuffType<Buffs.Invincibility>();
		Item.DefaultToHealingPotion(14, 24, 25);
		//Item.potionDelay = 60 * 30;
	}
	public override bool? UseItem(Player player)
	{
		player.immune = true;
		player.immuneTime = (int)(80 * (player.GetModPlayer<AvalonPlayer>().ThePill ? ThePill.LifeBonusAmount : 1));
		return base.UseItem(player);
	}
}
