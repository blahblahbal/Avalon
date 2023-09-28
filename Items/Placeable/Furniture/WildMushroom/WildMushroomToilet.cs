using Microsoft.Xna.Framework;
using Avalon.Items.Material;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomToilet : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomToilet>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 3)
            .AddIngredient(ItemID.VileMushroom)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 3)
            .AddIngredient(ItemID.ViciousMushroom)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 3)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddTile(TileID.Sawmill)
            .Register();
    }
}