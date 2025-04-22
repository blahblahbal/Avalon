using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Gem;

public class AmberChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.AmberChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.AmberGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class AmethystChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.AmethystChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.AmethystGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class DiamondChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.DiamondChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.DiamondGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class EmeraldChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.EmeraldChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.EmeraldGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class PeridotChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.PeridotChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<PeridotGemsparkBlock>(), 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class RubyChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.RubyChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.RubyGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class SapphireChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.SapphireChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.SapphireGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class TopazChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.TopazChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.TopazGemsparkBlock, 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class TourmalineChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.TourmalineChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<TourmalineGemsparkBlock>(), 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}

public class ZirconChest : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Gem.ZirconChest>());
		Item.width = 26;
		Item.height = 22;
		Item.value = Item.sellPrice(silver: 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<ZirconGemsparkBlock>(), 8)
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.WorkBenches).Register();
	}
}
