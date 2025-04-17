using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class Tourmaline : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 15;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Gems;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.PlacedGems>(), 3 + 6);
		Item.value = Item.sellPrice(0, 0, 11, 25);
	}

	// legacy code from when tmod's canplace hook literally didn't work
	//public override bool? UseItem(Player player)
	//{
	//	int i = Player.tileTargetX;
	//	int j = Player.tileTargetY;
	//	if ((WorldGen.SolidTile(i - 1, j, noDoors: true) || WorldGen.SolidTile(i + 1, j, noDoors: true) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1)))
	//	{
	//		Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
	//		Item.consumable = true;
	//	}
	//	else
	//	{
	//		Item.createTile = -1;
	//		Item.consumable = false;
	//	}
	//	return null;
	//}
}
