using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomClock : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomClock>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 300;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup("IronBar", 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ItemID.Mushroom, 5)
            .AddIngredient(ItemID.GlowingMushroom, 5)
            .AddIngredient(ItemID.VileMushroom)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe()
            .AddRecipeGroup("IronBar", 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ItemID.Mushroom, 5)
            .AddIngredient(ItemID.GlowingMushroom, 5)
            .AddIngredient(ItemID.ViciousMushroom)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe()
            .AddRecipeGroup("IronBar", 3)
            .AddIngredient(ItemID.Glass, 6)
            .AddIngredient(ItemID.Mushroom, 5)
            .AddIngredient(ItemID.GlowingMushroom, 5)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddTile(TileID.Sawmill)
            .Register();
    }
}
