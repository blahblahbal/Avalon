using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture.ResistantWood;

public class ResistantWoodSofa : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodSofa>();
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
            .AddIngredient(ModContent.ItemType<Tile.ResistantWood>(), 5)
            .AddIngredient(ItemID.Silk, 2)
            .AddTile(TileID.Sawmill).Register();
    }
}
