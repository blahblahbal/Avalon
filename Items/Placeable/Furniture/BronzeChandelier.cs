using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class BronzeChandelier : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Metal.BronzeChandelier>());
		Item.width = 26;
		Item.height = 26;
		Item.value = Item.sellPrice(silver: 7, copper: 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 4)
			.AddIngredient(ItemID.Torch, 4)
			.AddIngredient(ItemID.Chain)
			.AddTile(TileID.Anvils).Register();
	}
}
