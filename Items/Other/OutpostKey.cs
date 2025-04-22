using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class OutpostKey : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToMisc(14, 20);
		Item.rare = ItemRarityID.Lime;
	}
}
