using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

public class BacciliteBrickWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DemoniteBrickWall);
        Item.createWall = ModContent.WallType<Walls.BacciliteBrickWall>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.BacciliteBrick>()).AddTile(TileID.WorkBenches).Register();
        Recipe.Create(ModContent.ItemType<Tile.BacciliteBrick>()).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
    }
}
