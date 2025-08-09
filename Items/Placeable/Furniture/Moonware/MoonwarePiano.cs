using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Moonware;

public class MoonwarePiano : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Moonware.MoonwarePiano>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 4)
			.AddIngredient(ModContent.ItemType<Tile.MoonplateBlock>(), 15)
			.AddIngredient(ItemID.Book)
			.AddTile(TileID.SkyMill)
			.Register();
	}
}
