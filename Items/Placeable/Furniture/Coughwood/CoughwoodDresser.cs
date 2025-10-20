using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodDresser : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodDresser>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>(), 16)
			.AddTile(TileID.Sawmill).Register();
	}
}
