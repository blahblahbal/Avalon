using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Wall;

class CoughwoodWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.createWall = ModContent.WallType<Avalon.Walls.CoughwoodWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.Coughwood>()).AddTile(TileID.WorkBenches).Register();
        Terraria.Recipe.Create(ModContent.ItemType<Tile.Coughwood>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
    }
}
