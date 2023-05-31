using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

class OrangeSlabWall : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.Size = new Vector2(12);
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.OrangeSlabWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4).AddIngredient(ModContent.ItemType<Tile.OrangeBrick>()).AddTile(TileID.HeavyWorkBench).Register();
        Recipe.Create(ModContent.ItemType<Tile.OrangeBrick>()).AddIngredient(this, 4).AddTile(TileID.HeavyWorkBench).Register();
    }
}
