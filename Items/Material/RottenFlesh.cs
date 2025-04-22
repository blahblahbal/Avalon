using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class RottenFlesh : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(20, 20);
		Item.value = Item.sellPrice(copper: 2);
	}
}
