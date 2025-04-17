using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Ores;

public class Starstone : ModItem
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
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Ores.Starstone>());
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 0, 2);
	}
	public override void AddRecipes()
	{
		Recipe.Create(ItemID.ManaCrystal).AddIngredient(this, 60).AddTile(TileID.Furnaces).Register();
		CreateRecipe(60).AddIngredient(ItemID.ManaCrystal).AddTile(TileID.Furnaces).Register();
		Recipe.Create(ItemID.FallenStar).AddIngredient(this, 12).AddTile(TileID.Furnaces).Register();
	}
}
