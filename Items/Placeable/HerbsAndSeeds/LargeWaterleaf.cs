using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.HerbsAndSeeds;

public class LargeWaterleaf : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 15;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Herbs.LargeHerbsStage4>(), 4);
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(copper: 60);
	}
}
