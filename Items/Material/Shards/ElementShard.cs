using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Shards;

public class ElementShard : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Shards;
	}
	public override void SetDefaults()
	{
		Item.DefaultToShard(9 + 10, true);
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 0, 24);
	}

	// legacy code from when tmod's canplace hook literally didn't work
	//public override bool? UseItem(Player player)
	//{
	//	int i = Player.tileTargetX;
	//	int j = Player.tileTargetY;
	//	if ((WorldGen.SolidTile(i - 1, j, noDoors: true) || WorldGen.SolidTile(i + 1, j, noDoors: true) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1)))
	//	{
	//		Item.createTile = ModContent.TileType<Tiles.ShardsTier2>();
	//		Item.consumable = true;
	//	}
	//	else
	//	{
	//		Item.createTile = -1;
	//		Item.consumable = false;
	//	}
	//	return null;
	//}
	public override void AddRecipes()
	{
		CreateRecipe(10)
			.AddIngredient(ModContent.ItemType<BlastShard>(), 3)
			.AddIngredient(ModContent.ItemType<TornadoShard>(), 3)
			.AddIngredient(ModContent.ItemType<VenomShard>(), 3)
			.AddIngredient(ModContent.ItemType<WickedShard>(), 3)
			.AddIngredient(ModContent.ItemType<SacredShard>(), 3)
			.AddIngredient(ModContent.ItemType<CoreShard>(), 3)
			.AddIngredient(ModContent.ItemType<TorrentShard>(), 3)
			.AddIngredient(ModContent.ItemType<DemonicShard>(), 3)
			.AddIngredient(ModContent.ItemType<FrigidShard>(), 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
