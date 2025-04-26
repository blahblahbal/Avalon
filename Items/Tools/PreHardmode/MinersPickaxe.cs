using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class MinersPickaxe : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(60, 10, 3.5f, 18, 19, 1);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 32);
	}
}
