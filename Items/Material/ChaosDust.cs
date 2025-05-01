using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class ChaosDust : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMisc(20, 14);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 0, 10);
	}
}
