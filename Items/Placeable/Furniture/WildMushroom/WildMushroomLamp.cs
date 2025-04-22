using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomLamp : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomLamp>());
		Item.width = 10;
		Item.height = 24;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Torch)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
