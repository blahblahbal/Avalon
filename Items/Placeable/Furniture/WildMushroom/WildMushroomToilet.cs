using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomToilet : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomToilet>());
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 6)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 6)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 6)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
