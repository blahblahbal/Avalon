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
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.consumable = true;
        Item.useTime = 10;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.createTile = ModContent.TileType<Tiles.ShardsTier2>();
        Item.placeStyle = 7 + 10;
        Item.rare = ItemRarityID.Lime;
        Item.Size = new(20);
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 6, 0);
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
