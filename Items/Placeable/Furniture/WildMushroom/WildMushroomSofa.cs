using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomSofa : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomSofa>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 5)
			.AddIngredient(ItemID.VileMushroom)
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 5)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 5)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddIngredient(ItemID.Silk, 2)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
