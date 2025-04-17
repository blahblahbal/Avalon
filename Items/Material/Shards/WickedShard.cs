using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Shards;

public class WickedShard : ModItem
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
		Item.DefaultToShard(7 + 10, true);
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
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<CorruptShard>(), 2)
			.AddIngredient(ItemID.SoulofNight, 2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
