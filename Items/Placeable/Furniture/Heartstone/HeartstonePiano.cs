using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Heartstone;

public class HeartstonePiano : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Heartstone.HeartstonePiano>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 4)
			.AddIngredient(ModContent.ItemType<Material.Ores.Heartstone>(), 15)
			.AddIngredient(ItemID.Book)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
