using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class ForsakenRelic : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 2);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (player.immune)
		{
			player.GetCritChance(DamageClass.Generic) += 7;
			player.GetDamage(DamageClass.Generic) += 0.07f;
		}
	}
}
