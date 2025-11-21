using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

public class ResistantWoodCandle : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodCandle>());
		Item.width = 8;
		Item.height = 18;
		Item.value = Item.sellPrice(copper: 60);
		Item.noWet = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 4)
			.AddIngredient(ItemID.Torch)
			.AddTile(TileID.WorkBenches)
			.SortAfterFirstRecipesOf(ModContent.ItemType<ResistantWoodCandelabra>())
			.Register();
	}
}
