using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class TropicalShroomCap : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc();
		Item.value = Item.buyPrice(0, 0, 1);
	}
}
