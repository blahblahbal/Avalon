using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomWorkBench : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomWorkBench>());
		Item.width = 28;
		Item.height = 14;
		Item.value = Item.sellPrice(copper: 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ItemID.VileMushroom)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ItemID.ViciousMushroom)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.Register();
	}
}
