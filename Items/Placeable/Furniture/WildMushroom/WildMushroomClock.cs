using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomClock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomClock>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddRecipeGroup("IronBar", 3)
			.AddIngredient(ItemID.Glass, 6)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe()
			.AddRecipeGroup("IronBar", 3)
			.AddIngredient(ItemID.Glass, 6)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe()
			.AddRecipeGroup("IronBar", 3)
			.AddIngredient(ItemID.Glass, 6)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
