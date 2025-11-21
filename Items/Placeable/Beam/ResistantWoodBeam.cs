using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Beam;

public class ResistantWoodBeam : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 50;
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ResistantWoodBeam>());
	}
	public override void AddRecipes()
	{
		Terraria.Recipe.Create(Type, 2)
			.AddIngredient(ModContent.ItemType<Tile.ResistantWood>())
			.AddTile(TileID.Sawmill)
			.SortAfterFirstRecipesOf(ItemID.AshWoodToilet)
			.Register();
	}
}
