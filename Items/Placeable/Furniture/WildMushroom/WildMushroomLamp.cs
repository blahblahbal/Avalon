using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.WildMushroom;

public class WildMushroomLamp : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.WildMushroom.WildMushroomLamp>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 500;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
            .AddIngredient(ItemID.VileMushroom)
            .AddTile(TileID.WorkBenches)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
            .AddIngredient(ItemID.ViciousMushroom)
            .AddTile(TileID.WorkBenches)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ItemID.Torch)
            .AddIngredient(ModContent.ItemType<Material.WildMushroom>(), 3)
            .AddIngredient(ModContent.ItemType<VirulentMushroom>())
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
