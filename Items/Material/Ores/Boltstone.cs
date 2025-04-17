using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class Boltstone : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Gems;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.Boltstone>());
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 0, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(25)
			.AddIngredient(ModContent.ItemType<Consumables.StaminaCrystal>())
			.AddTile(TileID.Furnaces)
			.DisableDecraft()
			.Register();
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(ModContent.ItemType<Items.Consumables.StaminaCrystal>()).AddIngredient(this, 35).AddTile(TileID.Furnaces).Register();
	//    CreateRecipe(35).AddIngredient(ModContent.ItemType<Items.Consumables.StaminaCrystal>()).AddTile(TileID.Furnaces).Register();
	//}
}
