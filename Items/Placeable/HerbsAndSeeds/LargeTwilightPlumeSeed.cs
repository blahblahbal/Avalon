using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.HerbsAndSeeds;

public class LargeTwilightPlumeSeed : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;
		Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Herbs.LargeHerbsStage1>(), 11);
	}
}
