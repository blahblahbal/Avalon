using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class Root : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 22);
		Item.value = Item.sellPrice(silver: 1);
	}
}
