using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Coughwood;

public class CoughwoodLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Tiles.Contagion.Coughwood.Coughwood>(), 3)
			.AddTile(TileID.WorkBenches).Register();
	}
}
