using Avalon.Items.Consumables.Critters;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.Gem;

public class PeridotSquirrelCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SapphireSquirrelCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.PeridotSquirrelCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<PeridotSquirrel>())
            .Register();
    }
}
public class TourmalineSquirrelCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.AmethystSquirrelCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.TourmalineSquirrelCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<TourmalineSquirrel>())
            .Register();
    }
}
public class ZirconSquirrelCage : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.RubySquirrelCage);
        Item.createTile = ModContent.TileType<Tiles.Furniture.Gem.ZirconSquirrelCage>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Terrarium)
            .AddIngredient(ModContent.ItemType<ZirconSquirrel>())
            .Register();
    }
}
