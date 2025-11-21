using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

public class ResistantWoodBed : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodBed>());
		Item.width = 28;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 15)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ResistantWoodBathtub>())
			.Register();
	}
}
