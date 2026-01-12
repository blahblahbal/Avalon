using Avalon.Items.Placeable.Furniture.Tuhrtl;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.TrappedChests;

public class TrappedTuhrtlChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Functional.TrappedChests>(), 8);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TuhrtlChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}
