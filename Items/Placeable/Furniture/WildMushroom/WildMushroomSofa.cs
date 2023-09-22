using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomSofa : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomSofa>();
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
        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 2)
            .AddIngredient(ItemID.VileMushroom)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 2)
            .AddIngredient(ItemID.ViciousMushroom)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Mushroom, 3)
            .AddIngredient(ItemID.GlowingMushroom, 2)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill)
            .Register();
    }
}
