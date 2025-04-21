using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class FourLeafClover : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(26, 26);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(5);
	}
}
