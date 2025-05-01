using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BrokenHiltPiece : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 1);
	}
}
