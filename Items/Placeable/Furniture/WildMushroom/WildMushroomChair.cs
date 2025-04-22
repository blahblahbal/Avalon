using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomChair : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomChair>());
		Item.width = 12;
		Item.height = 30;
		Item.value = Item.sellPrice(copper: 30);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 4)
			.AddIngredient(ItemID.VileMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 4)
			.AddIngredient(ItemID.ViciousMushroom)
			.AddTile(TileID.WorkBenches)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 4)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
