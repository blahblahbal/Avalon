using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class DarkMatterMonolith : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>());
		Item.width = 22;
		Item.height = 32;
		Item.rare = ModContent.RarityType<Rarities.TealRarity>();
		Item.value = Item.sellPrice(0, 20);
	}
	//public override void AddRecipes()
	//{
	//    Recipe.Create(Type)
	//        .AddIngredient(ModContent.ItemType<Material.DarkMatterGel>(), 100)
	//        .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 10)
	//        .AddIngredient(ModContent.ItemType<Bar.BerserkerBar>(), 5)
	//        .AddTile(TileID.Furnaces)
	//        .Register();
	//}
}
