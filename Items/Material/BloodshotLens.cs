using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BloodshotLens : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(12, 20);
		Item.value = Item.sellPrice(0, 0, 20);
	}
}
