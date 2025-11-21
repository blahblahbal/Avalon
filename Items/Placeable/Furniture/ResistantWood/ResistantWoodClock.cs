using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

public class ResistantWoodClock : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodClock>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("IronBar", 3)
			.AddIngredient(ItemID.Glass, 6)
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 10)
			.AddTile(TileID.Sawmill)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ResistantWoodChair>())
			.Register();
	}
}
