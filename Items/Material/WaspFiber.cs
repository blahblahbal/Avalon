using Avalon.Common.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class WaspFiber : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	//public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	//{
	//    itemGroup = Data.Sets.ItemGroupValues.Contagion;
	//}
	public override void SetDefaults()
	{
		Item.DefaultToMisc(20, 20);
		Item.value = Item.sellPrice(0, 0, 1, 50);
	}
}
