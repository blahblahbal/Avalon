using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwareBed : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwareBed>());
		Item.width = 28;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 15)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
