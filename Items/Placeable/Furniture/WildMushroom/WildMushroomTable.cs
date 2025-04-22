using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomTable : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomTable>());
		Item.width = 26;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 8)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 8)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 8)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
