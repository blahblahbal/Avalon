using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class BismuthCandelabra : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Metal.BismuthCandelabra>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 5)
			.AddIngredient(ItemID.Torch, 3)
			.AddTile(TileID.WorkBenches).Register();
	}
}
