using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Crafting;

public class LibraryAltar : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Functional.LibraryAltar>());
		Item.width = 32;
		Item.height = 32;
		Item.rare = ItemRarityID.Blue;
	}
}
