using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class GhostintheMachine : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc(28, 24);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 0, 50);
	}
}
