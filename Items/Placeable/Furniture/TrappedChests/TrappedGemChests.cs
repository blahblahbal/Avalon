using Avalon.Items.Placeable.Furniture.Gem;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 11;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<AmberChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 12;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<AmethystChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 13;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<DiamondChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 14;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<EmeraldChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 15;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<PeridotChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 16;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<RubyChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 17;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<SapphireChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 18;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<TopazChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 19;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<TourmalineChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
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
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.TrappedChests>();
        Item.placeStyle = 20;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<ZirconChest>())
            .AddIngredient(ItemID.Wire, 10)
            .AddTile(TileID.HeavyWorkBench)
			.Register();
    }
}
