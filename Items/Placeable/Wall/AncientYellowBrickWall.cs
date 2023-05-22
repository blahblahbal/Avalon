using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

class AncientYellowBrickWall : ModItem
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
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.AncientYellowBrickWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.Ancient.AncientYellowBrick>()).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ModContent.ItemType<Tile.Ancient.AncientYellowBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
    }
}
