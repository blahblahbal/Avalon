using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Fish;

public class Ickfish : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		Item.DefaultToFish();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 15);
	}
}
