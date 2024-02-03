using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomPiano : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomPiano>();
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
           .AddIngredient(ItemID.Bone, 4)
           .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
           .AddIngredient(ItemID.VileMushroom)
           .AddIngredient(ItemID.Book)
           .AddTile(TileID.Sawmill)
           .Register();

        CreateRecipe(1)
           .AddIngredient(ItemID.Bone, 4)
           .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
           .AddIngredient(ItemID.ViciousMushroom)
           .AddIngredient(ItemID.Book)
           .AddTile(TileID.Sawmill)
           .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Bone, 4)
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 15)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddIngredient(ItemID.Book)
            .AddTile(TileID.Sawmill)
            .Register();
    }
}
