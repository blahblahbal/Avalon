using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Placeable.Walls;

class OsmiumBrickWallItem : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.Size = new Vector2(16);
        Item.useTurn = true;
        Item.useTime = 10;
        Item.createWall = ModContent.WallType<Wall.OsmiumBrickWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<OsmiumBrick>()).AddTile(TileID.WorkBenches).Register();
        CreateRecipe(1).AddIngredient(this, 4).AddTile(TileID.WorkBenches).Register();
    }
}
