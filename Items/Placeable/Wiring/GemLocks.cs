using Avalon.Tiles.Furniture.Gem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wiring;

public class PeridotGemLock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<GemLocks>(), 0);
		Item.width = 22;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>(), 5)
			.AddIngredient(ItemID.StoneBlock, 10)
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}

public class TourmalineGemLock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<GemLocks>(), 1);
		Item.width = 22;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>(), 5)
			.AddIngredient(ItemID.StoneBlock, 10)
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}


public class ZirconGemLock : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<GemLocks>(), 2);
		Item.width = 22;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 5)
			.AddIngredient(ItemID.StoneBlock, 10)
			.AddTile(TileID.HeavyWorkBench)
			.Register();
	}
}
