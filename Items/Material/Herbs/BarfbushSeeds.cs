using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Herbs;

public class BarfbushSeeds : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.AlchemySeeds;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Herbs.Barfbush>());
		Item.value = Item.sellPrice(0, 0, 0, 16);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(2).AddIngredient(ModContent.ItemType<Tile.ChunkstoneBlock>(), 5).AddIngredient(ModContent.ItemType<Material.YuckyBit>(), 2).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
	//}
}
