using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareSofa : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareSofa>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 5)
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
