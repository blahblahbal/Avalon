using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class Dirtalier : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Dirtalier>());
		Item.width = 26;
		Item.height = 26;
		Item.value = Item.sellPrice(silver: 6);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10)
			.AddIngredient(ItemID.DirtiestBlock, 1)
			.AddIngredient(ItemID.Torch, 4)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Anvils).Register();
	}
}
