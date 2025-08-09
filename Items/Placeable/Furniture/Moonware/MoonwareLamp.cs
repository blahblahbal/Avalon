using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 3)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
