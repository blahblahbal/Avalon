using Avalon.Items.Material.Herbs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class HangingBarfbush : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.HangingPots>(), 0);
		Item.value = Item.buyPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PotSuspended)
			.AddIngredient(ModContent.ItemType<Barfbush>())
			.SortAfterFirstRecipesOf(ItemID.PotSuspendedDeathweedCorrupt)
			.Register();
	}
}

public class HangingSweetstem : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.HangingPots>(), 1);
		Item.value = Item.buyPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PotSuspended)
			.AddIngredient(ModContent.ItemType<Sweetstem>())
			.SortAfterFirstRecipesOf(ItemID.PotSuspendedMoonglow)
			.Register();
	}
}
public class HangingBloodberry : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.HangingPots>(), 2);
		Item.value = Item.buyPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PotSuspended)
			.AddIngredient(ModContent.ItemType<Bloodberry>())
			.SortAfterFirstRecipesOf(ModContent.ItemType<HangingBarfbush>())
			.Register();
	}
}
public class HangingHolybird : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.HangingPots>(), 3);
		Item.value = Item.buyPrice(0, 0, 25);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.PotSuspended)
			.AddIngredient(ModContent.ItemType<Holybird>())
			.SortAfterFirstRecipesOf(ItemID.PotSuspendedFireblossom)
			.Register();
	}
}

