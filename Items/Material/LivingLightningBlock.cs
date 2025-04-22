using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class LivingLightningBlock : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.LivingLightning>());
		Item.value = Item.sellPrice(copper: 10);
	}
}
