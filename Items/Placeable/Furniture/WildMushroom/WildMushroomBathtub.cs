using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomBathtub : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomBathtub>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 14)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 14)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 14)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
