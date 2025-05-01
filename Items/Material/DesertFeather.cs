using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class DesertFeather : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 24);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 1);
	}
}
