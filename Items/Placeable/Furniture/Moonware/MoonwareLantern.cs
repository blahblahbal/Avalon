using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareLantern : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareLantern>());
		Item.width = 12;
		Item.height = 28;
		Item.value = Item.sellPrice(copper: 30);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 6)
			.AddIngredient(ItemID.Torch)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
