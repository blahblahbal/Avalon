using Avalon.Items.Material.Herbs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class HangingBarfbush : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 36;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.HangingPots>();
        Item.placeStyle = 0;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.buyPrice(0, 0, 25, 0);
        Item.useAnimation = 15;
        Item.rare = ItemRarityID.White;
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
        Item.width = 26;
        Item.height = 36;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.HangingPots>();
        Item.placeStyle = 1;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.buyPrice(0, 0, 25, 0);
        Item.useAnimation = 15;
        Item.rare = ItemRarityID.White;
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
        Item.width = 26;
        Item.height = 36;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.HangingPots>();
        Item.placeStyle = 2;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.buyPrice(0, 0, 25, 0);
        Item.useAnimation = 15;
        Item.rare = ItemRarityID.White;
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
        Item.width = 26;
        Item.height = 36;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.HangingPots>();
        Item.placeStyle = 3;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.buyPrice(0, 0, 25, 0);
        Item.useAnimation = 15;
        Item.rare = ItemRarityID.White;
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

