using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

public class RingofArrogance : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
		Item.value = Item.sellPrice(0, 4);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Generic) += 0.2f;
		player.endurance -= 0.3f;
	}
}
