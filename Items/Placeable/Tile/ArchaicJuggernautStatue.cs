using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class ArchaicJuggernautStatue : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.ArchaicJuggernautStatue>());
		Item.width = 30;
		Item.height = 30;
	}
	//public override void AddRecipes()
	//{
	//    Terraria.Recipe.Create(Type,5)
	//        .AddIngredient(ItemID.StoneBlock,5)
	//        .AddIngredient(ModContent.ItemType<Material.Ores.IridiumOre>())
	//        .AddTile(TileID.Furnaces)
	//        .Register();
	//}
}
