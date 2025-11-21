using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

public class ResistantWoodBookcase : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodBookcase>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 20)
			.AddIngredient(ItemID.Book, 10)
			.AddTile(TileID.Sawmill)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ResistantWoodBathtub>())
			.Register();
	}
}
