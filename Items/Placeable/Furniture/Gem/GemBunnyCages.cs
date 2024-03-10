using Avalon.Items.Consumables.Critters;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Gem;

public class PeridotBunnyCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SapphireBunnyCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.PeridotBunnyCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<PeridotBunny>())
            .Register();
    }
}
public class TourmalineBunnyCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.AmethystBunnyCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.TourmalineBunnyCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<TourmalineBunny>())
            .Register();
    }
}
public class ZirconBunnyCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.RubyBunnyCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.ZirconBunnyCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<ZirconBunny>())
            .Register();
    }
}
