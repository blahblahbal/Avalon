using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class FleshyTendril : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Fleshy Tendril");
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc();
		Item.value = Item.sellPrice(0, 0, 0, 10);
	}
}
