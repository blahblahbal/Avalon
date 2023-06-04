using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wiring;

public class PeridotGemLock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.createTile = ModContent.TileType<Tiles.GemLocks>();
        Item.placeStyle = 0;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
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
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        //Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.createTile = ModContent.TileType<Tiles.GemLocks>();
        Item.placeStyle = 1;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
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
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        //Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.createTile = ModContent.TileType<Tiles.GemLocks>();
        Item.placeStyle = 2;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
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
