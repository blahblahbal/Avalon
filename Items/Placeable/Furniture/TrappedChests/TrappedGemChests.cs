using Avalon.Items.Placeable.Furniture.Gem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.TrappedChests;

public class TrappedAmberChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 11);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<AmberChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedAmethystChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 12);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<AmethystChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedDiamondChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 13);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<DiamondChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedEmeraldChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 14);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<EmeraldChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedPeridotChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 15);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<PeridotChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedRubyChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 16);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<RubyChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedSapphireChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 17);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<SapphireChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedTopazChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 18);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TopazChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedTourmalineChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 19);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TourmalineChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}

public class TrappedZirconChest : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.TrapSigned[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.TrappedChests>(), 20);
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ZirconChest>())
			.AddIngredient(ItemID.Wire, 10)
			.AddTile(TileID.HeavyWorkBench)
			.SortAfterFirstRecipesOf(ItemID.Fake_AshWoodChest)
			.Register();
	}
}
