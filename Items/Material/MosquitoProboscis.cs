using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class MosquitoProboscis : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToMisc();
		Item.value = Item.sellPrice(copper: 40);
	}
}
