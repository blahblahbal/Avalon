using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareCandelabra : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareCandelabra>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 3);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 5)
			.AddIngredient(ItemID.Torch, 3)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
