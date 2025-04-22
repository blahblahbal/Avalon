using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomBed : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomBed>());
		Item.width = 28;
		Item.height = 20;
		Item.value = Item.sellPrice(silver: 4);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
			.AddIngredient(ItemID.VileMushroom)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddIngredient(ItemID.Silk, 5)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
