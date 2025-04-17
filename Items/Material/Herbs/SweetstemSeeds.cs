using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Herbs;

public class SweetstemSeeds : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.AlchemySeeds;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Herbs.Sweetstem>());
		Item.value = Item.sellPrice(0, 0, 0, 16);
	}
	//public override void AddRecipes()
	//{
	//    CreateRecipe(2).AddIngredient(ItemID.Hive, 5).AddIngredient(ItemID.Stinger).AddIngredient(ItemID.Seed, 8).AddTile(ModContent.TileType<Tiles.SeedFabricator>()).Register();
	//}
}
