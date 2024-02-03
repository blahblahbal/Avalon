using Microsoft.Xna.Framework;
using Avalon.Items.Material;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

class WildMushroomWorkBench : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomWorkBench>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 150;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
            .AddIngredient(ItemID.VileMushroom)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
            .AddIngredient(ItemID.ViciousMushroom)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 10)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .Register();
    }
}
