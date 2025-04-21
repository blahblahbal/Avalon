using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class FakeFourLeafClover : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(26, 26);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 30);
	}
}
