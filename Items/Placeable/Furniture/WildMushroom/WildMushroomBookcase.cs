using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomBookcase : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomBookcase>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 20)
			.AddIngredient(ItemID.VileMushroom)
			.AddIngredient(ItemID.Book, 10)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 20)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddIngredient(ItemID.Book, 10)
			.AddTile(TileID.Sawmill)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 20)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddIngredient(ItemID.Book, 10)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
