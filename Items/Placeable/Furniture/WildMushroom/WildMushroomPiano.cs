using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomPiano : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomPiano>());
		Item.width = 20;
		Item.height = 20;
		Item.value = Item.sellPrice(copper: 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
		   .AddIngredient(ItemID.Bone, 4)
		   .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
		   .AddIngredient(ItemID.VileMushroom)
		   .AddIngredient(ItemID.Book)
		   .AddTile(TileID.Sawmill)
		   .Register();

		CreateRecipe(1)
		   .AddIngredient(ItemID.Bone, 4)
		   .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
		   .AddIngredient(ItemID.ViciousMushroom)
		   .AddIngredient(ItemID.Book)
		   .AddTile(TileID.Sawmill)
		   .Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.Bone, 4)
			.AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
			.AddIngredient(ModContent.ItemType<VirulentMushroom>())
			.AddIngredient(ItemID.Book)
			.AddTile(TileID.Sawmill)
			.Register();
	}
}
