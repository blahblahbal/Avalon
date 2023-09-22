using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomBed : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomBed>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 2000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 8)
            .AddIngredient(ItemID.GlowingMushroom, 7)
            .AddIngredient(ItemID.VileMushroom)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 8)
            .AddIngredient(ItemID.GlowingMushroom, 7)
            .AddIngredient(ItemID.ViciousMushroom)
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 8)
            .AddIngredient(ItemID.GlowingMushroom, 7)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddIngredient(ItemID.Silk, 5)
            .AddTile(TileID.Sawmill)
            .Register();
    }
}
