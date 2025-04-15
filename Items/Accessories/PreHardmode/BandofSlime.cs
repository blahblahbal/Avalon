using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;

public class BandofSlime : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.endurance += 0.05f;
		player.noFallDmg = true;
		player.slippy = true;
		player.slippy2 = true;
	}
}
