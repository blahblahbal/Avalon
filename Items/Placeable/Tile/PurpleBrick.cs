using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class PurpleBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PurpleBrick>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    /* public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.PurpleBrickWall>(), 4).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.PurpleSlabWall>(), 4).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Wall.PurpleTiledWall>(), 4).AddTile(TileID.WorkBenches).Register();
    } */
}
